﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TrainSimulator.Model
{
    public class TrainSet
    {
        public String name { get; set; }
        public List<TrainCart> cartList;
        public Engine engine;
        public LocoDriver locoDriver;

        public TrainSet(String name, List<TrainCart> cartList, Engine engine)
        {
            this.name = name;
            this.cartList = cartList;
            this.engine = engine;
        }

        public TrainSet()
        {
            cartList = new List<TrainCart>();
        }

        public void updatePosition(GameTime gameTime)
        {
            double trackLength = 480;
            double distance = (gameTime.ElapsedGameTime.Milliseconds * engine.currentSpeed) / trackLength;
            
            foreach (TrainCart tc in cartList)
            {
                tc.moveCart(distance);
            }
        }

        public double calculateMaxSpeed()
        {
            double maxSpeed = double.MaxValue;

            foreach (TrainCart tc in cartList)
            {
                if (maxSpeed > tc.maxSpeed)
                {
                    maxSpeed = tc.maxSpeed;
                }
            }

            if (maxSpeed > engine.maxSpeed)
                maxSpeed = engine.maxSpeed;

            return maxSpeed;
        }

        public override string ToString()
        {
            string status = "";
            if (locoDriver.driverState == LocoDriver.DriverState.Accelerate)
                status = "   Accelerating";
            else if (locoDriver.driverState == LocoDriver.DriverState.Decelerate)
                status = "   Decelerating";
            else if (locoDriver.driverState == LocoDriver.DriverState.Cruise)
                status = "   Cruise";
            else if (locoDriver.driverState == LocoDriver.DriverState.Stopped)
                status = "   Stopped";
            else if (locoDriver.driverState == LocoDriver.DriverState.Off)
                status = "   Off";
            return name + "       " + String.Format("{0:0.00}", engine.currentSpeed) + status;
        }
    }
}
