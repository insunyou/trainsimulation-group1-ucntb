﻿using System;
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

            int offset = 47;
            int radius = 38;

            int angle = Convert.ToInt32((90 * cart.position / 100) + MathHelper.ToDegrees(rotation));
            int moveAngle = angle - 90;

            Vector2 result = new Vector2();

            if (turn)
            {
                if (MathHelper.ToDegrees(rotation) == 90)
                    result = new Vector2(position.X - offset + Convert.ToInt32(radius * Math.Cos(Math.PI / 180 * moveAngle)), position.Y + Convert.ToInt32(radius * Math.Sin(Math.PI / 180 * moveAngle)));
                else if (MathHelper.ToDegrees(rotation) == 180)
                    result = new Vector2(position.X + Convert.ToInt32(radius * Math.Cos(Math.PI / 180 * moveAngle)), position.Y - offset + Convert.ToInt32(radius * Math.Sin(Math.PI / 180 * moveAngle)));
                else if (MathHelper.ToDegrees(rotation) == 270)
                    result = new Vector2(position.X + offset + Convert.ToInt32(radius * Math.Cos(Math.PI / 180 * moveAngle)), position.Y + Convert.ToInt32(radius * Math.Sin(Math.PI / 180 * moveAngle)));
                else
                    result = new Vector2(position.X + Convert.ToInt32(radius * Math.Cos(Math.PI / 180 * moveAngle)), position.Y + offset + Convert.ToInt32(radius * Math.Sin(Math.PI / 180 * moveAngle)));

                cart.rotation = (float)(angle * Math.PI / 180);
                return result;
            }
            else
            {
                double cartPos = cart.position;

                if (!direction && rotation == 0)
                {
                    result = new Vector2(position.X - 9, Convert.ToInt32(position.Y + (50 * cartPos / 100)));
                }


                if (rotation == 0)
                {
                    result = new Vector2(Convert.ToInt32(position.X + (50 * cartPos / 100)), position.Y + 9);
                }
                else
                {
                    result = new Vector2(position.X - 9, Convert.ToInt32(position.Y + (50 * cartPos / 100)));
                }
            }

            return result;
        }
    }
}