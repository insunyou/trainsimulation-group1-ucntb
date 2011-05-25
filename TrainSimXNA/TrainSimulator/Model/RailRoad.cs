using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrainSimulator.Model
{
    public class RailRoad
    {
        public List<Track> tracks { get; set; }

        public RailRoad(List<Track> tracks)
        {
            this.tracks = tracks;
        }

        public RailRoad()
        {
            this.tracks = new List<Track>();
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
