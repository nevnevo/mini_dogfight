using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml.Controls;
using static System.Net.Mime.MediaTypeNames;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml;
using System.Windows;
using Windows.Foundation;

using Windows.UI;
using Windows.UI.Composition.Scenes;
using static mini_dogfight.client;

namespace mini_dogfight
{
    public class Triangle:GameMovingObject
    {
        private double _x;
        private double _y;
        private client.Player _player;
        private bool isLocalPlayer;
        Canvas _field;
        public Triangle(double x, double y, string fileName, Canvas field, double size, client.Player player,bool isLocalPlayer) : base(x, y, fileName, field, size)
        {
            GameManager.GameEvents.OnKeyPress += Move;
            _field = field;
            _player = player;

            
            this.isLocalPlayer = isLocalPlayer;
            
        }
        
        public override void Render()//הפעולה צריכה להתבצע ללא הפסקה עבור כל הדמויות הנעות
        {
            _x += 3;///שינוי מיקום
            _y += 3;
            Canvas.SetLeft(_objectImage, _x);
            Canvas.SetTop(_objectImage, _y);


            base.Render();              //מציירת את הדמות במיקום החדש
        }
        protected void Move(VirtualKey key)
        {
            if(!isLocalPlayer) return;//רק השחקן המקומי יכול להזיז את הדמות שלו
            if (key == Controls.up)
                Vertical(-1);
            if (key == Controls.down)
                Vertical(1);
            if (key == Controls.right)
                Drive(1);
            if (key == Controls.left)
                Drive(-1);

            Render();
        }
        public DataObj GetData()
        {
            return new DataObj( _speedX, _speedY, _x, _y);
        }
        public void SetNewData(DataObj data)
        {
            _x = data.getX();
            _y = data.getY();
            _speedX = data.getSpeedX();
            _speedY = data.getSpeedY();
            Render();
        }

        private void Drive(int v)
        {
            _speedX += v * 5;
        }

        private void Vertical(int v)
        {
            _speedY += v * 5;
        }
    }
}
