using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace mini_dogfight
{
    public class GameManager
    {

        private Canvas _map;
        private List<GameObject> objectList = new List<GameObject>();
        private DispatcherTimer _runTimer;
        public static Events GameEvents { get; private set; } = new Events();
        public GameManager(Canvas map)
        {
            _runTimer = new DispatcherTimer();
            _runTimer.Interval = TimeSpan.FromMilliseconds(1);
            _runTimer.Start();
            _runTimer.Tick += _runTimer_Tick;
            _map = map;
            objectList.Add(new Triangle(100, 180, "player_test.png", _map, 120));
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;
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
