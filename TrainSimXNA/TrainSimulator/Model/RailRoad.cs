using System.Collections.Generic;

namespace TrainSimulator.Model
{
    public class RailRoad
    {
        public List<Track> tracks { get; set; }
        public List<TrainSet> trains { get; set; }

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

        public Dictionary<Track, TrainSet> getTrackStatus()
        {
            Dictionary<Track, TrainSet> result = new Dictionary<Track, TrainSet>();
            foreach (Track t in tracks)
            {
                result.Add(t, null);
                foreach (TrainSet train in trains)
                {
                    foreach (TrainCart cart in train.cartList)
                        if (cart.currentTrack.id == t.id)
                            result[t] = train;
                }
            }

            return result;
        }

        public bool isNextTrackFree(Track nextTrack)
        {
            if (getTrackStatus()[nextTrack] == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
