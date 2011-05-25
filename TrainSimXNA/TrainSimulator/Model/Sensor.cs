using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrainSimulator.Model
{
    public class Sensor
    {
        public int id { get; set; }
        public enum State {Off}
        public State state { get; set; }

        public Sensor(int id)
        {
            this.id = id;
            this.state = State.Off;
        }
    }
}
