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
            this.state = State.Off;
            goTexture = content.Load<Texture2D>("SignalOn");
            stopTexture = content.Load<Texture2D>("SignalOff");
        }

        public Texture2D getTexture()
        {
            switch (state)
            {
                case State.Go: return goTexture; break;
                case State.Stop: return stopTexture; break;
            }
            return null;
        }

    }
}
