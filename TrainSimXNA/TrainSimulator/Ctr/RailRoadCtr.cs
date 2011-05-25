using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrainSimulator.DB;
using TrainSimulator.Model;
using Microsoft.Xna.Framework.Content;

namespace TrainSimulator.Ctr
{
    public class RailRoadCtr
    {
        RailRoadDB rrDB = new RailRoadDB();

        public RailRoad loadRailRoad(string path, ContentManager content)
        {
            return rrDB.loadXML(path, content);
        }
    }
}
