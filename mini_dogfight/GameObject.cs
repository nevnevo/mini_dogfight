using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace mini_dogfight
{
    public abstract class GameObject
    {
        protected double _x;
        protected double _y;
        protected Image _objectImage;
        protected Canvas _map;
        public bool isCollisional { get; set; } = true;
        public GameObject(double x, double y, string fileName, Canvas scene, double size)
        {
            _x = x;
            _y = y;
            _objectImage = new Image();
            _objectImage.Width = size; // image's width
            _map = scene;
            SetImage(fileName);
            _map.Children.Add(_objectImage);
            Render();
        }
        public GameObject() { }//this is a special constructor for Wall

        public virtual void Render()
        {
            Canvas.SetLeft(_objectImage, _x);
            Canvas.SetTop(_objectImage, _y);
        }

        protected void SetImage(string fileName)
        {
            _objectImage.Source = new BitmapImage(new Uri($"ms-appx:///Assets/{fileName}"));
        }
        public virtual Rect Rect(int angle)
        {

            return new Rect(_x, _y, _objectImage.Width - 15, _objectImage.Height - 15);

        }
        public virtual Rect Rect()
        {

            return new Rect(_x, _y, _objectImage.Width - 15, _objectImage.Height - 15);

        }
        public virtual void Collide(GameObject otherObject) { }

    }
}
