using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TrainSimulator.Model
{
    public class LocoDriver
    {
        public string name { get; set; }
        public TrainSet trainSet { get; set; }
        public RailRoad railRoad { get; set; }
        
        public LocoDriver()
        {
            
        }

        public LocoDriver(string name, TrainSet myTrain, RailRoad railroad)
        {
            this.name = name;
            this.trainSet = myTrain;
            this.railRoad = railroad;
        }

        public void freeToRun()
        {
            Track t = trainSet.cartList[0].currentTrack;
            Track nextTrack = t.getNextTrack(t.prevTrack);
            bool status = railRoad.isNextTrackFree(nextTrack);

            if (status)
            {
                Accelerate();
            }
            else
            {
                Deaccelerate();
            }
        }

        public void shutDownEngine()
        {
            trainSet.engine.shutDown();
        }

        public void Accelerate()
        {
            trainSet.engine.engineState = Engine.EngineState.Accelerate;
        }

        public void Deaccelerate()
        {
            trainSet.engine.engineState = Engine.EngineState.Deaccelerate;
        }
    }
}
