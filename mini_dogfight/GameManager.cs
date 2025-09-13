using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using static mini_dogfight.client;

namespace mini_dogfight
{
    public class GameManager
    {

        private Canvas _map;
        private List<GameObject> objectList = new List<GameObject>();
        private DispatcherTimer _runTimer;
        private DispatcherTimer _dataSendTimer;
        public static Events GameEvents { get; private set; } = new Events();
        public client localClient = new client("10.0.0.14",1111,1111);
        Triangle other_player;
        
        public GameManager(Canvas map)
        {

            GameEvents.OnDataRecieve += DataProccess;
            
            _runTimer = new DispatcherTimer();
            _runTimer.Interval = TimeSpan.FromMilliseconds(1);
            _runTimer.Start();
            _runTimer.Tick += _runTimer_Tick;


            _dataSendTimer = new DispatcherTimer();
            _dataSendTimer.Interval = TimeSpan.FromMilliseconds(25);
            _dataSendTimer.Start();
            _dataSendTimer.Tick += SendData;
            _map = map;

            if (localClient.player == client.Player.PlayerA)
            {
                objectList.Add(new Triangle(100, 100, "images/player_test.png", _map, 120, localClient.player, true));//the local player's char
                other_player = new Triangle(_map.ActualWidth-100, 100, "images/player_test.png", _map, 120, localClient.remotePlayer, false);//that way we have direct access to it
                objectList.Add(other_player);
            }
            else
            {
                objectList.Add(new Triangle(_map.ActualWidth - 100, 100, "images/player_test.png", _map, 120, localClient.player, true));//the local player's char
                other_player = new Triangle(100, 100, "images/player_test.png", _map, 120, localClient.remotePlayer, false);//that way we have direct access to it
                objectList.Add(other_player);
            }

            
            
            
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;

        }

        private void SendData(object sender, object e)
        {
            localClient.SendData(other_player.GetData());
        }

        public void DataProccess(DataObj data)
        {
            other_player.SetNewData(data);
        }

        private void CoreWindow_KeyUp(CoreWindow sender, KeyEventArgs args)
        {
            if (GameEvents.OnKeyLeave != null)
            {
               
                
                 GameEvents.OnKeyLeave(args.VirtualKey);//האירוע שהגדרנו
                
            }
        }

        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            if (GameEvents.OnKeyPress != null)
            {
                GameEvents.OnKeyPress(args.VirtualKey);//האירוע שהגדרנו
            }

        }

        private void _runTimer_Tick(object sender, object e)
        {
            foreach (GameObject obj in objectList)
            {
                if (obj is GameMovingObject moveObj)
                    moveObj.Render();
            }
        }
    }
}
