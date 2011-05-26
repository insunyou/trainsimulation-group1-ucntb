using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrainSimulator.Model
{
    public class Sensor
    {
        public int id { get; set; }
        public enum State {Off , On}
        public State state { get; set; }
        public Signal mySignal { get; set; }

        public Sensor(int id, State state, Signal signal)
        {
            this.id = id;
            this.state = state;
            this.mySignal = signal;
        }

        public Sensor()
        {
        }

        public void setSignal(State state)
        {
            this.state = state;
            if (state == State.On)
                mySignal.state = Signal.State.Stop;
            else
                mySignal.state = Signal.State.Go;
        }
    }
}
