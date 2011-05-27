using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TrainSimulator.Model
{
    public class SwitchRight : Track
    {
        public SwitchRight()
            : base()
        {

        }


        public override Vector2 calculatCartPosition(TrainCart cart)
        {
            Vector2 result = new Vector2();
            int offset = 59;
            int radius = 50;

            double cartPos = cart.position;

            if (MathHelper.ToDegrees(rotation) == 0 || MathHelper.ToDegrees(rotation) == 180)
            {
                if (cart.currentPos.X <= cart.previousTrack.position.X)
                    cartPos = 100 - cartPos;
            }
            else if (MathHelper.ToDegrees(rotation) == 90 || MathHelper.ToDegrees(rotation) == 270)
            {
                if (cart.currentPos.Y <= cart.previousTrack.position.Y)
                    cartPos = 100 - cartPos;
            }

            int angle = Convert.ToInt32((90 * cartPos / 100)); // + MathHelper.ToDegrees(rotation));
            int moveAngle = angle - 90;


            if (cart.previousTrack == nextTrack || (cart.previousTrack == prevTrack && !turn))
            {
                if (MathHelper.ToDegrees(rotation) == 0)
                    result = new Vector2(Convert.ToInt32(position.X + (50 * cartPos / 100)), position.Y + 9);
                else if (MathHelper.ToDegrees(rotation) == 90)
                    result = new Vector2(position.X - 9, Convert.ToInt32(position.Y + (50 * cartPos / 100)));
                else if (MathHelper.ToDegrees(rotation) == 180)
                    result = new Vector2(Convert.ToInt32(position.X -50 + (50 * cartPos / 100)), position.Y - 9);
                else if (MathHelper.ToDegrees(rotation) == 270)
                    result = new Vector2(position.X + 9, Convert.ToInt32(position.Y - 50 + (50 * cartPos / 100)));

            }
            else
            {
                if (MathHelper.ToDegrees(rotation) == 90)
                {
                    angle = 90 + angle;
                    moveAngle = 90 + moveAngle;
                    result = new Vector2(position.X - offset + Convert.ToInt32(radius * Math.Cos(Math.PI / 180 * moveAngle)), position.Y + Convert.ToInt32(radius * Math.Sin(Math.PI / 180 * moveAngle)));
                }
                else if (MathHelper.ToDegrees(rotation) == 180)
                {
                    angle = 90 - angle;
                    moveAngle = 90 - moveAngle;
                    result = new Vector2(position.X + Convert.ToInt32(radius * Math.Cos(Math.PI / 180 * moveAngle)), position.Y - offset + Convert.ToInt32(radius * Math.Sin(Math.PI / 180 * moveAngle)));
                }
                else if (MathHelper.ToDegrees(rotation) == 270)
                {
                    angle = -angle;
                    moveAngle = 180 - moveAngle;
                    result = new Vector2(position.X + offset + Convert.ToInt32(radius * Math.Cos(Math.PI / 180 * moveAngle)), position.Y + Convert.ToInt32(radius * Math.Sin(Math.PI / 180 * moveAngle)));
                }
                else
                    result = new Vector2(position.X + Convert.ToInt32(radius * Math.Cos(Math.PI / 180 * moveAngle)), position.Y + offset + Convert.ToInt32(radius * Math.Sin(Math.PI / 180 * moveAngle)));

                cart.rotation = (float)(angle * Math.PI / 180);
            }

            return result;
        }
    }
}
