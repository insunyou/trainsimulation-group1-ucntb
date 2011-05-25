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
        public int length { get; set; }
        public double maxSpeed { get; set; }
        public Track previousTrack;
        public Track currentTrack;
        public double position { get; set; }
        public Vector2 currentPos { get; set; }
        //public Point moveVector { get; set; }
        //private float currentRot;
        public Texture2D gfx { get; set; }
        public float rotation { get; set; }
        public Matrix transform { get; set; }
        public Vector2 origin { get; set; }

        public TrainCart(ContentManager content)
        {
            //System.IO.Stream file = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("TrainSimulator.Graphics.trainCar.png");
            //gfx = Image.FromStream(file);
            gfx = content.Load<Texture2D>("trainCar");
            origin = new Vector2(gfx.Width / 2, gfx.Height / 2);
            //transform = new Matrix();
            //currentRot = 10.5f;
        }

        public void setCart()
        {
            //transform.Rotate(currentRot);
            currentPos = currentTrack.calculatCartPosition(this);
            //transform.Translate(currentPos.X, currentPos.Y);
        }

        public void moveCart(double amount)
        {

            System.Diagnostics.Debug.WriteLine("Position: " + position);

            //position = (position + amount);

            //if (position > 100)
            //{
            //position = position % 100;

            if (currentTrack is SwitchLeft || currentTrack is SwitchRight)
            {
                if (currentTrack.prevTrack == previousTrack)
                {                  
                    position += amount;

                    if (position > 100)
                    {
                        position = position % 100;
                        previousTrack = currentTrack;

                        if (currentTrack.turn)
                        {
                            currentTrack = currentTrack.switchTrack;
                        }
                        else
                        {
                            currentTrack = currentTrack.nextTrack;
                        }
                    }
                }
                else if (currentTrack.nextTrack == previousTrack)
                {
                    if (currentTrack is SwitchLeft)
                    {
                        position += amount;
                        if (position > 100)
                        {
                            position = position % 100;
                            previousTrack = currentTrack;
                            currentTrack = currentTrack.prevTrack;
                        }
                    }
                    else
                    {
                        position -= amount;
                        if (position < 0)
                        {
                            position = 100 - position;
                            previousTrack = currentTrack;
                            currentTrack = currentTrack.prevTrack;
                        }
                    }
                }
                else if (currentTrack.switchTrack == previousTrack)
                {
                    position += amount;
                    if (position > 100)
                    {
                        position = position % 100;
                        previousTrack = currentTrack;
                        currentTrack = currentTrack.prevTrack;
                    }
                }

            }
            else
            {
                if (currentTrack.prevTrack == previousTrack)
                {
                    position += amount;
                    if (position > 100)
                    {
                        position = position % 100;
                        previousTrack = currentTrack;
                        currentTrack = currentTrack.nextTrack;
                    }
                }
                else
                {
                    position -= amount;
                    if (position < 0)
                    {
                        position = 100 - position;
                        previousTrack = currentTrack;
                        currentTrack = currentTrack.prevTrack;
                        
                    }
                }

            }
            currentPos = currentTrack.calculatCartPosition(this);

        }

        public void createRotation(float total)
        {
            rotation = total;
            //currentRot = currentRot - total;
            //Bitmap rotatedImg = new Bitmap(gfx.Width, gfx.Width);
            //using (Graphics g = Graphics.FromImage(rotatedImg))
            //{
            //    //transform.Rotate(total);
            //    g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //    g.RotateTransform(total);
            //    g.DrawImage(gfx, new Point(0, 0));
            //}
            //gfx = rotatedImg;
        }

    }
}
