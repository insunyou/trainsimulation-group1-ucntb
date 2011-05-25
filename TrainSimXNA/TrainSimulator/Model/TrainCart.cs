using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TrainSimulator.Model
{
    public class TrainCart
    {
        public double maxSpeed { get; set; }
        public Track previousTrack;
        public Track currentTrack;
        public double position { get; set; }
        public Vector2 currentPos { get; set; }
        public Texture2D gfx { get; set; }
        public float rotation { get; set; }
        public Vector2 origin { get; set; }

        public TrainCart(ContentManager content)
        {
            gfx = content.Load<Texture2D>("trainCar");
            origin = new Vector2(gfx.Width / 2, gfx.Height / 2);
        }

        public void setCart()
        {
            currentPos = currentTrack.calculatCartPosition(this);
        }

        public void moveCart(double amount)
        {
            if (currentTrack is StraightTrack || currentTrack is CornerTrack)
            {
                position += amount;
                if (position > 100)
                {
                    position = position % 100;
                    if (previousTrack == currentTrack.prevTrack)
                    {
                        previousTrack = currentTrack;
                        currentTrack = currentTrack.nextTrack;
                    }
                    else
                    {
                        previousTrack = currentTrack;
                        currentTrack = currentTrack.prevTrack;
                    }
                }
            }
            else if (currentTrack is SwitchLeft || currentTrack is SwitchRight)
            {
                position += amount;
                if (position > 100)
                {
                    position = position % 100;
                    if (previousTrack == currentTrack.prevTrack)
                    {
                        if (currentTrack.turn)
                        {
                            previousTrack = currentTrack;
                            currentTrack = currentTrack.switchTrack;
                        }
                        else
                        {
                            previousTrack = currentTrack;
                            currentTrack = currentTrack.nextTrack;
                        }
                    }
                    else
                    {
                        previousTrack = currentTrack;
                        currentTrack = currentTrack.prevTrack;
                    }

                }
            }
            //else if (currentTrack is SwitchRight)
            //{
            //    position += amount;
            //    if (position > 100)
            //    {
            //        position = position % 100;
            //        if (previousTrack == currentTrack.prevTrack)
            //        {
            //            if (currentTrack.turn)
            //            {
            //                previousTrack = currentTrack;
            //                currentTrack = currentTrack.switchTrack;
            //            }
            //            else
            //            {
            //                previousTrack = currentTrack;
            //                currentTrack = currentTrack.nextTrack;
            //            }
            //        }
            //        else
            //        {
            //            previousTrack = currentTrack;
            //            currentTrack = currentTrack.prevTrack;
            //        }
            //    }
            //}

            currentPos = currentTrack.calculatCartPosition(this);
        }
    }
}
