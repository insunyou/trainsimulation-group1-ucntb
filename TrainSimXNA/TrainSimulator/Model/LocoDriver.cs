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
            this.trainSet = trainSet;
            this.railRoad = railRoad;
        }

        public void Accelerate()
        {
            trainSet.engine.engineState = (Engine.EngineState)1;
        }

        public void Deaccelerate()
        {
            trainSet.engine.engineState = (Engine.EngineState)2;
        }
    }
}
