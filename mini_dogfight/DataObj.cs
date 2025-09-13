namespace mini_dogfight
{
    public class DataObj
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double SpeedX { get; set; }
        public double SpeedY { get; set; }

        // Parameterless constructor is required for Json.NET
        public DataObj() { }

        public DataObj(double speedX, double speedY, double xPos, double yPos)
        {
            this.SpeedX = speedX;
            this.SpeedY = speedY;
            this.X = xPos;
            this.Y = yPos;
        }
    }
}
