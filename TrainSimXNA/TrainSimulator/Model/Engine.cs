using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


namespace TrainSimulator.Model
{
    public class Engine
    {
        public bool isRunning { get; set; }
        public double accelerationSpeed { get; set; }
        public TrainSet trainset { get; set; }
        public double currentSpeed { get; set; }
        public double maxSpeed { get; set; }
        public bool forward { get; set; }
        public int lastUpdateTime { get; set; }
        public double lastUpdateSpeed { get; set; }
        public enum EngineState { Accelerate, Deaccelerate }
        public EngineState engineState { get; set; }
        public double brakingSpeed { get; set; }

        public Engine(TrainSet trainset, double maxSpeed)
        {
            this.trainset = trainset;
            this.maxSpeed = maxSpeed;
            double trainSetMaxSpeed = this.trainset.calculateMaxSpeed();
            if (trainSetMaxSpeed < this.maxSpeed)
            {
                this.maxSpeed = trainSetMaxSpeed;
            }
            
        }

        public void Start()
        {
            currentSpeed = 0;
            lastUpdateSpeed = 0;
            //lastUpdateTime = DateTime.Now.Millisecond * 1000;
            accelerationSpeed = 0.00277;
        }

        //Vi burde aldrig få en distance på over 100.
        public void updatePostion(GameTime gameTime)
        {
            System.Diagnostics.Debug.WriteLine("Speed: " + currentSpeed);

            if (currentSpeed < maxSpeed && engineState == EngineState.Accelerate)
                accelerate(gameTime);
            else if(currentSpeed > 0 && engineState == EngineState.Deaccelerate)
                Deaccelerate(gameTime);

            double speed = (lastUpdateSpeed + currentSpeed) / 2;
            double trackLength = 480;
            double distance = (gameTime.ElapsedGameTime.Milliseconds * speed) / trackLength;

            foreach(TrainCart tc in trainset.cartList)
            {
                tc.moveCart(distance);
            }

            this.lastUpdateSpeed = currentSpeed;
            //this.lastUpdateTime = gameTime.ElapsedGameTime.Milliseconds;
        }

        public void accelerate(GameTime gameTime)
        {
            currentSpeed = currentSpeed + (gameTime.ElapsedGameTime.Milliseconds) * accelerationSpeed;            
        }

        public void Deaccelerate(GameTime gameTime)
        {
            currentSpeed = currentSpeed - (gameTime.ElapsedGameTime.Milliseconds) * brakingSpeed;
        }
    }
}
