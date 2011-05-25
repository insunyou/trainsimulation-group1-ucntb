﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TrainSimulator.Model
{
    public class Signal
    {
        public int id { get; set; }
        public enum State { Go, Stop, Off }
        public State state { get; set; }

        public Vector2 position { get; set; }

        public Signal(int id)
        {
            this.id = id;
            this.state = State.Off;
        }
    }
}
