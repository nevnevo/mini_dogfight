using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Windows.ApplicationModel.Chat;
using System.Security.Cryptography;
using System.ServiceModel.Channels;
using System.ComponentModel;
using Newtonsoft.Json;
using System.IO;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml;
using System.Runtime.InteropServices;
namespace mini_dogfight
{
    public class client
    {
        private UdpClient _udpClient;
        private IPEndPoint _endPoint; 
        private int _localPort;
        private bool isInitialized = false;
        public enum Player
        {
            PlayerA,
            PlayerB
        }
        public Player player { get; set; }
        public client(string serverIP, int serverPort, int localPort)
        {
            _localPort = localPort;
            _udpClient = new UdpClient(_localPort);
            _endPoint = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);
            _udpClient.Client.ReceiveTimeout = 30000; // 10 seconds
            initialize_connection();
            if (isInitialized)
            {

                new Thread(Listen).Start();
                
            }
                
            else
            {
                throw new Exception("Failed to initialize connection with server.");
            }
        }



        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        private static extern bool FreeConsole();


        private void Listen()
        {
            byte[] data = _udpClient.Receive(ref _endPoint);
            string recievedData = Encoding.UTF8.GetString(data);
            Mutex mutex = new Mutex();
            mutex.WaitOne();
            Task.Run(() =>
            {
                DataObj recievedObject = Newtonsoft.Json.JsonConvert.DeserializeObject<DataObj>(recievedData);
                ProcessMessage(recievedObject);
            });
            mutex.ReleaseMutex();

        }
        public void SendData(DataObj dataObject)
        {
            string jsonData = JsonConvert.SerializeObject(dataObject);
            byte[] data = Encoding.UTF8.GetBytes(jsonData);
            _udpClient.Send(data, data.Length, _endPoint);
        }

        private void ProcessMessage(object message)
        {
            if(message is DataObj dataObject)//only if we its a DataObj we proccess it.
            {
                if (GameManager.GameEvents.OnDataRecieve != null)
                {
                    GameManager.GameEvents.OnDataRecieve(dataObject);
                }
            }
        }

        public void initialize_connection()
        {
            Random rndTime = new Random((int)DateTime.Now.Ticks);
            int posNumber = rndTime.Next(1, 500);

            // Step 1: handshake
            bool handshakeComplete = false;
            DateTime start = DateTime.Now;
            byte[] data;
            while (!handshakeComplete && (DateTime.Now - start).TotalSeconds < 30) // 10s timeout
            {
                try
                {
                    _udpClient.Send(Encoding.UTF8.GetBytes("AA"), 2, _endPoint);
                    byte[] received = _udpClient.Receive(ref _endPoint);
                    string response = Encoding.UTF8.GetString(received);
                    

                    if (response == "BB")
                    {
                        handshakeComplete = true;
                        isInitialized = true;
                    }
                    else if (response == "AA")
                    {
                        handshakeComplete = true;
                        isInitialized = true;
                        _udpClient.Send(Encoding.UTF8.GetBytes("BB"), 2, _endPoint);
                    }
                }
                catch (SocketException) { /* timeout, retry */ }
            }

            // Step 2: exchange numbers
            if (isInitialized)
            {
                AllocConsole();
                Console.WriteLine("Connection Initialized, moving to step 2");

            }
                
            data = Encoding.UTF8.GetBytes(posNumber.ToString());
            _udpClient.Send(data, data.Length, _endPoint);

            try
            {
                byte[] received = _udpClient.Receive(ref _endPoint);
                string response = Encoding.UTF8.GetString(received);
                if (int.TryParse(response, out int peerCode))
                {
                    
                    if(posNumber > peerCode)
                        player = Player.PlayerA;
                    else
                        player = Player.PlayerB;

                }
               
            }
            catch (SocketException)
            {
                AllocConsole();
                Console.WriteLine("Failed to receive peer code from server.");
            }
        }

       
    }
}
