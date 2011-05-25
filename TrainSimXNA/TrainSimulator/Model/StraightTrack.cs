using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TrainSimulator.Model
{
    public class StraightTrack : Track
    {

        //public StraightTrack(int id, Track nextTrack, Track prevTrack, List<Signal> signals, List<Sensor> sensors, Bitmap gfx, Point position)
        //    : base(id, nextTrack, prevTrack, signals, sensors, gfx, position, false)
        //{
        //}

        public StraightTrack()
            : base()
        {
        }

        public override Vector2 calculatCartPosition(TrainCart cart)
        {
            Vector2 result = new Vector2(0, 0);
            double cartPos = cart.position;
            if (direction == false)
                cartPos = 100 - cartPos;
            if (rotation == 0)
            {
                result = new Vector2(Convert.ToInt32(position.X + (65 * cartPos / 100)), position.Y + 9);
            }
            else
            {
                result = new Vector2(position.X - 9, Convert.ToInt32(position.Y + (65 * cartPos / 100)));
            }

            return result;
        }
    }
}
