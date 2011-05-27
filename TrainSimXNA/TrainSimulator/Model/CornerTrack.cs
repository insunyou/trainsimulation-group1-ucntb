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
            int offset = 57;
            int radius = 48;

            double cartPos = cart.position;

            int angle = Convert.ToInt32((90 * cartPos / 100) + MathHelper.ToDegrees(rotation));
            int moveAngle = angle - 90;
            Vector2 result = new Vector2();

            if (direction)
            {

                if (MathHelper.ToDegrees(rotation) == 90)
                {
                    angle =  90 - angle;
                    moveAngle =  90 - moveAngle;
                    result = new Vector2(position.X - offset + Convert.ToInt32(radius * Math.Cos(Math.PI / 180 * moveAngle)), position.Y + Convert.ToInt32(radius * Math.Sin(Math.PI / 180 * moveAngle)));
                }
                else if (MathHelper.ToDegrees(rotation) == 180)
                {
                    angle = 270 - angle;
                    moveAngle = 270 - moveAngle;
                    result = new Vector2(position.X + Convert.ToInt32(radius * Math.Cos(Math.PI / 180 * moveAngle)), position.Y - offset + Convert.ToInt32(radius * Math.Sin(Math.PI / 180 * moveAngle)));
                }
                else if (MathHelper.ToDegrees(rotation) == 270)
                {
                    angle = 90 - angle;
                    moveAngle = 90 - moveAngle;
                    result = new Vector2(position.X + offset + Convert.ToInt32(radius * Math.Cos(Math.PI / 180 * moveAngle)), position.Y + Convert.ToInt32(radius * Math.Sin(Math.PI / 180 * moveAngle)));
                }
                else
                {
                    angle = 270 - angle;
                    moveAngle = 270 - moveAngle;
                    result = new Vector2(position.X + Convert.ToInt32(radius * Math.Cos(Math.PI / 180 * moveAngle)), position.Y + offset + Convert.ToInt32(radius * Math.Sin(Math.PI / 180 * moveAngle)));
                }

            }
            else
            {
                //if (cart.previousTrack != prevTrack)
                //    cartPos = 100 - cartPos;

                if (MathHelper.ToDegrees(rotation) == 90)
                    result = new Vector2(position.X - offset + Convert.ToInt32(radius * Math.Cos(Math.PI / 180 * moveAngle)), position.Y + Convert.ToInt32(radius * Math.Sin(Math.PI / 180 * moveAngle)));
                else if (MathHelper.ToDegrees(rotation) == 180)
                    result = new Vector2(position.X + Convert.ToInt32(radius * Math.Cos(Math.PI / 180 * moveAngle)), position.Y - offset + Convert.ToInt32(radius * Math.Sin(Math.PI / 180 * moveAngle)));
                else if (MathHelper.ToDegrees(rotation) == 270)
                    result = new Vector2(position.X + offset + Convert.ToInt32(radius * Math.Cos(Math.PI / 180 * moveAngle)), position.Y + Convert.ToInt32(radius * Math.Sin(Math.PI / 180 * moveAngle)));
                else
                    result = new Vector2(position.X + Convert.ToInt32(radius * Math.Cos(Math.PI / 180 * moveAngle)), position.Y + offset + Convert.ToInt32(radius * Math.Sin(Math.PI / 180 * moveAngle)));
            }
            cart.rotation = (float)(angle * Math.PI / 180);
            return result;
        }
    }
}
