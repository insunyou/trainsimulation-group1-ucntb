using Microsoft.Xna.Framework;

namespace TrainSimulator.Model
{
    public class Engine
    {
        private double accelerationSpeed = 0.00277;
        private double brakingSpeed = 0.017;

        public double currentSpeed { get; set; }
        public double maxSpeed { get; set; }
        public bool reverse { get; set; }
        public double lastUpdateSpeed { get; set; }

        public Engine(double maxSpeed)
        {
            this.maxSpeed = maxSpeed;
            currentSpeed = 0;
            lastUpdateSpeed = 0;
        }

        public void updateSpeed(GameTime gameTime)
        {
                //currentSpeed = (lastUpdateSpeed + currentSpeed) / 2;

                //this.lastUpdateSpeed = currentSpeed;
        }

        public void accelerate(GameTime gameTime, double maxSpeed)
        {
            currentSpeed = currentSpeed + (gameTime.ElapsedGameTime.Milliseconds * accelerationSpeed);
            if (currentSpeed > maxSpeed)
                currentSpeed = maxSpeed;
        }

        public void decelerate(GameTime gameTime)
        {
            currentSpeed = currentSpeed - (gameTime.ElapsedGameTime.Milliseconds * brakingSpeed);
            if (currentSpeed < 0)
                currentSpeed = 0;
        }
    }
}
