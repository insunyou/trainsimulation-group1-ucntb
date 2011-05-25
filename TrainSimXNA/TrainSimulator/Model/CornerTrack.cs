using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TrainSimulator.Model
{
    public class CornerTrack : Track
    {
        //public CornerTrack(int id, Track nextTrack, Track prevTrack, List<Signal> signals, List<Sensor> sensors, Bitmap gfx, Point position, bool direction)
        //    : base(id, nextTrack, prevTrack, signals, sensors, gfx, position, direction)
        //{
        //}        

        public CornerTrack()
            : base()
        {

        }

        public override Vector2 calculatCartPosition(TrainCart cart)
        {
            int offset = 47;
            int radius = 38;

            int angle = Convert.ToInt32((90 * cart.position / 100) + MathHelper.ToDegrees(rotation));
            int moveAngle = angle - 90;
            Vector2 result = new Vector2();
            if  (MathHelper.ToDegrees(rotation) == 90)
                result = new Vector2(position.X - offset + Convert.ToInt32(radius * Math.Cos(Math.PI / 180 * moveAngle)), position.Y + Convert.ToInt32(radius * Math.Sin(Math.PI / 180 * moveAngle)));
            else if (MathHelper.ToDegrees(rotation) == 180)
                result = new Vector2(position.X + Convert.ToInt32(radius * Math.Cos(Math.PI / 180 * moveAngle)), position.Y - offset + Convert.ToInt32(radius * Math.Sin(Math.PI / 180 * moveAngle)));
            else if (MathHelper.ToDegrees(rotation) == 270)
                result = new Vector2(position.X + offset + Convert.ToInt32(radius * Math.Cos(Math.PI / 180 * moveAngle)), position.Y + Convert.ToInt32(radius * Math.Sin(Math.PI / 180 * moveAngle)));
            else
                result = new Vector2(position.X + Convert.ToInt32(radius * Math.Cos(Math.PI / 180 * moveAngle)), position.Y + offset  + Convert.ToInt32(radius * Math.Sin(Math.PI / 180 * moveAngle)));
            
            cart.rotation = (float)(angle * Math.PI / 180);
            return result;
        }
    }
}
