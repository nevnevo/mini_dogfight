using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace mini_dogfight
{
    public abstract class GameMovingObject : GameObject
    {
        protected double _speedX;
        protected double _speedY;
        protected double _accelerationX;
        protected double _accelerationY;
        protected bool _canDrive { get; set; } = true;
        protected bool _canReverse { get; set; } = true;


        protected GameMovingObject(double x, double y, string fileName, Canvas field, double size) : base(x, y, fileName, field, size)
        {
            Stop();

        }
        public override void Render()//needs to run endlessly
        {
            _x += _speedX;
            _y += _speedY;
            _speedX += _accelerationX;
            _speedY += _accelerationY;


            base.Render();             //draws it in its new location
        }
        private void Stop() // stops the object
        {
            _speedX = 0;
            _speedY = 0;
            _accelerationX = 0;
            _accelerationX = 0;
        }

    }
}
