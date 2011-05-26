using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Drawing;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TrainSimulator.Model
{
    public abstract class Track : ITrack
    {
        public int id { get; set; }
        public Track nextTrack { get; set; }
        public Track prevTrack { get; set; }
        public Track switchTrack { get; set; }
        public Signal signal { get; set; }
        public Sensor sensor { get; set; }
        public Texture2D gfx { get; set; }
        public Vector2 position { get; set; }
        public float rotation { get; set; }
        public Vector2 moveVector { get; set; }
        //True = Right, False = Left
        public bool direction { get; set; }
        public bool turn { get; set; }

        //public Track(int id, Track nextTrack, Track prevTrack, List<Signal> signals, List<Sensor> sensors, Bitmap gfx, Point position, bool direction)
        //{
        //    this.id = id;
        //    this.nextTrack = nextTrack;
        //    this.prevTrack = prevTrack;
        //    this.sensors = sensors;
        //    this.signals = signals;
        //    this.gfx = gfx;
        //    this.position = position;
        //    this.direction = direction;
        //}

        public Track()
        {
            //signals = new List<Signal>();
            //sensors = new List<Sensor>();
        }

        public Track getNextTrack(Track prevT)
        {
            if (this is SwitchLeft || this is SwitchRight)
            {

            }
            else
            {
                if (this.prevTrack.id == prevT.id)
                {
                    return this.nextTrack;
                }
                else if (this.nextTrack.id == prevT.id)
                {
                    return this.prevTrack;
                }
            }

            return null;
        }
        
        public abstract Vector2 calculatCartPosition(TrainCart cart);
    }
}
