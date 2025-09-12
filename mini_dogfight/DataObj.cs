namespace mini_dogfight
{
    public class DataObj
    {
        private double speedX;
        private double speedY;
        private double xPos;
        private double yPos;
        public DataObj(double speedX, double speedY, double xPos, double yPos)
        {
            this.speedX = speedX;
            this.speedY = speedY;
            this.xPos = xPos;
            this.yPos = yPos;
        }
        public double getX() { return xPos;  }
        public double getY() { return yPos; }
        public double getSpeedX() { return speedX; }
        public double getSpeedY() { return speedY; }
    }
}