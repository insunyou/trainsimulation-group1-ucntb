using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TrainSimulator.Model
{
    public class LocoDriver
    {
        public enum DriverState
        {
            Accelerate,
            Decelerate,
            Cruise,
            Stopped,
            Off
        }

        public string name { get; set; }
        public TrainSet trainSet { get; set; }
        public RailRoad railRoad { get; set; }
        public DriverState driverState { get; set; }

        public LocoDriver(string name, TrainSet myTrain, RailRoad railroad)
        {
            this.name = name;
            this.trainSet = myTrain;
            this.railRoad = railroad;
            driverState = DriverState.Off;
        }

        public void StartDriving()
        {
            driverState = DriverState.Stopped;
        }

        public void update(GameTime gameTime)
        {
            switch (driverState)
            {
                case DriverState.Accelerate: Accelerate(gameTime); break;
                case DriverState.Decelerate: Decelerate(gameTime); break;
                case DriverState.Cruise: Cruise(); break;
                case DriverState.Stopped: Stopped(); break;
            }

            //trainSet.engine.updateSpeed(gameTime);
            trainSet.updatePosition(gameTime);
        }

        private void Accelerate(GameTime gameTime)
        {
            trainSet.engine.accelerate(gameTime, trainSet.calculateMaxSpeed());

            TrainSet trainInFront = getTrainInFront();
            Signal signal = getNextSignal();
            if (trainInFront != null)
            {
                if (trainInFront.engine.currentSpeed < trainSet.engine.currentSpeed)
                {
                    driverState = DriverState.Decelerate;
                }
                else if (trainInFront.engine.currentSpeed == trainSet.engine.currentSpeed)
                {
                    driverState = DriverState.Cruise;
                }
            }
            else if (signal != null)
            {
                if (signal.state == Signal.State.Stop)
                    driverState = DriverState.Decelerate;
            }
            else
            {
                if (trainSet.engine.currentSpeed == trainSet.calculateMaxSpeed())
                    driverState = DriverState.Cruise;
            }

        }

        private void Decelerate(GameTime gameTime)
        {
            trainSet.engine.decelerate(gameTime);

            TrainSet trainInFront = getTrainInFront();
            Signal signal = getNextSignal();

            if (trainInFront != null)
            {
                if (trainInFront.locoDriver.driverState == DriverState.Stopped && trainSet.engine.currentSpeed == 0)
                {
                    driverState = DriverState.Stopped;
                }
                else
                {
                    if (trainSet.engine.currentSpeed < trainInFront.engine.currentSpeed)
                        driverState = DriverState.Accelerate;
                    else if (trainSet.engine.currentSpeed > trainInFront.engine.currentSpeed)
                    {
                        driverState = DriverState.Decelerate;
                    }
                }
            }
            else if (signal != null)
            {
                if (signal.state == Signal.State.Stop)
                    if (trainSet.engine.currentSpeed > 0)
                        driverState = DriverState.Decelerate;
                    else
                        driverState = DriverState.Stopped;
            }
            else
            {
                if (trainSet.engine.currentSpeed < trainSet.calculateMaxSpeed())
                    driverState = DriverState.Accelerate;
            }
        }

        private void Cruise()
        {
            TrainSet trainInFront = getTrainInFront();
            Signal signal = getNextSignal();

            if (trainInFront != null)
            {
                if (trainSet.engine.currentSpeed < trainInFront.engine.currentSpeed)
                    driverState = DriverState.Accelerate;
                else if (trainSet.engine.currentSpeed > trainInFront.engine.currentSpeed)
                {
                    driverState = DriverState.Decelerate;
                }
            }
            else if (signal != null)
            {
                if (signal.state == Signal.State.Stop)
                    driverState = DriverState.Decelerate;
            }
            else
            {
                if (trainSet.engine.currentSpeed < trainSet.calculateMaxSpeed())
                    driverState = DriverState.Accelerate;
            }
        }

        private void Stopped()
        {
            TrainSet trainInFront = getTrainInFront();
            Signal signal = getNextSignal();

            if (signal != null && trainInFront == null)
            {
                if (signal.state == Signal.State.Go)
                    driverState = DriverState.Accelerate;
            }
            if (signal == null && trainInFront == null)
                driverState = DriverState.Accelerate;

        }

        private TrainSet getTrainInFront()
        {
            Track t = trainSet.cartList[0].currentTrack;
            Track nextTrack = t.getNextTrack(trainSet.cartList[0].previousTrack);
            return railRoad.nextTrackStatus(nextTrack);
        }

        private Signal getNextSignal()
        {
            Track t = trainSet.cartList[0].currentTrack;
            Track nextTrack = t.getNextTrack(trainSet.cartList[0].previousTrack);
            Signal s = railRoad.getNextSignal(nextTrack);
            return s;
        }
    }
}
