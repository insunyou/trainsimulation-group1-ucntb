﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrainSimulator.Model
{
    public class RailRoad
    {
        public List<Track> tracks { get; set; }
        public List<TrainSet> trains { get; set; }

        //public RailRoad(List<Track> tracks)
        //{
        //    this.tracks = tracks;
        //}

        public RailRoad()
        {
            this.tracks = new List<Track>();
            this.trains = new List<TrainSet>();
        }

        public Track findTrack(int id)
        {
            foreach (Track t in tracks)
            {
                if (t.id == id)
                    return t;                    
            }

            return null;
        }
    }
}
