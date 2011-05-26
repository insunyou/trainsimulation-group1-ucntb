using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace TrainSimulator.Model
{
    public class Signal
    {
        public int id { get; set; }
        public enum State { Go, Stop, Off }
        public State state { get; set; }
        public Texture2D goTexture { get; set; }
        public Texture2D stopTexture { get; set; }

        public Vector2 position { get; set; }

        public Signal(int id, ContentManager content)
        {
            this.id = id;
            this.state = State.Go;
            goTexture = content.Load<Texture2D>("SignalGo");
            stopTexture = content.Load<Texture2D>("SignalStop");
        }

        public Texture2D getTexture()
        {
            switch (state)
            {
                case State.Go: return goTexture;
                case State.Stop: return stopTexture;
            }
            return null;
        }

        public override string ToString()
        {
            string status = "";
            if (state == Signal.State.Go)
                status = "      Go";
            else
                status = "      Stop";

            return "Signal #" + id + status;
        }
    }
}
