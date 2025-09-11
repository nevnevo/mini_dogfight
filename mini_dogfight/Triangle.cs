using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace mini_dogfight
{
    public class Triangle:GameMovingObject
    {
        private double _x;
        private double _y;
        Canvas _field;
        public Triangle(double x, double y, string fileName, Canvas field, double size) : base(x, y, fileName, field, size)
        {

            _field = field;

        }
        public override void Render()//הפעולה צריכה להתבצע ללא הפסקה עבור כל הדמויות הנעות
        {
            _x += 3;///שינוי מיקום
            _y += 3;
            Canvas.SetLeft(_objectImage, _x);
            Canvas.SetTop(_objectImage, _y);


            Render();              //מציירת את הדמות במיקום החדש
        }
        protected void Move(VirtualKey key)
        {
            if (key == Controls.up)
                Vertical(1);
            if (key == Controls.down)
                Vertical(-1);
            if (key == Controls.right)
                Drive(1);
            if (key == Controls.left)
                Drive(-1);
        }

        private void Vertical(int v)
        {
            throw new NotImplementedException();
        }
    }
}
