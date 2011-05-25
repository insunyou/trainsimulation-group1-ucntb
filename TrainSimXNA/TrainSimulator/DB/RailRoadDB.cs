using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using TrainSimulator.Model;
using System.Drawing;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace TrainSimulator.DB
{
    public class RailRoadDB
    {
        public RailRoad loadXML(string path, ContentManager content)
        {
            XmlDocument doc = new XmlDocument();

            //doc.Load(@"C:\Users\LasseSoerensen\Documents\Visual Studio 2010\Projects\RailRoadXML\RailRoadXML\RailRoad.xml");
            doc.Load(path);

            XmlNodeList nodeList = doc.SelectNodes("RailRoad/Track");

            RailRoad rr = new RailRoad();

            foreach (XmlNode n in nodeList)
            {
                string type = n.Attributes.GetNamedItem("Type").Value.ToString();

                Track t = new StraightTrack();
                t = new StraightTrack();
                t.gfx = content.Load<Texture2D>("StraightNB");
                t.direction = Convert.ToBoolean(n.Attributes.GetNamedItem("Direction").Value.ToString());
                if (type.Equals("CornerTrack"))
                {
                    t = new CornerTrack();
                    t.direction = Convert.ToBoolean(n.Attributes.GetNamedItem("Direction").Value.ToString());
                    if (t.direction)
                    {
                        t.gfx = content.Load<Texture2D>("rightTurnNB");
                    }
                    else
                    {
                        t.gfx = content.Load<Texture2D>("leftTurnNB");
                    }
                }

                if (type.Equals("SwitchTrack"))
                {
                    t = new SwitchTrack();
                    t.direction = Convert.ToBoolean(n.Attributes.GetNamedItem("Direction").Value.ToString());
                    t.turn = Convert.ToBoolean(n.Attributes.GetNamedItem("Switch").Value.ToString());
                    if (t.direction)
                    {
                        t.gfx = content.Load<Texture2D>("switchRight");
                    }
                    else
                    {
                        t.gfx = content.Load<Texture2D>("switchLeft");
                    }
                }

                t.rotation = MathHelper.ToRadians(Convert.ToInt32(n.Attributes.GetNamedItem("Rotation").Value));                

                Vector2 point = new Vector2(Convert.ToInt32(n.Attributes.GetNamedItem("X").Value), Convert.ToInt32(n.Attributes.GetNamedItem("Y").Value));
                t.position = point;
                t.id = Convert.ToInt32(n.Attributes.GetNamedItem("ID").Value);

                foreach (XmlNode nn in n.ChildNodes)
                {
                    foreach (XmlNode nnn in nn.ChildNodes)                        
                    {
                        if (nnn.Name == "Signal")
                        {
                            Signal s = new Signal(Convert.ToInt32(nnn.Attributes.GetNamedItem("ID").Value));
                            s.position = new Vector2(Convert.ToInt32(n.Attributes.GetNamedItem("X").Value), Convert.ToInt32(n.Attributes.GetNamedItem("Y").Value));
                            switch (nnn.Attributes.GetNamedItem("ID").Value)
                            {
                                case "Stop": s.state = Signal.State.Stop; break;
                                case "Go": s.state = Signal.State.Go; break;
                                case "Off": s.state = Signal.State.Off; break;
                            }

                            t.signals.Add(s);
                        }
                        else if (nnn.Name == "Sensor")
                        {
                            Sensor s = new Sensor(Convert.ToInt32(nnn.Attributes.GetNamedItem("ID").Value));
                            t.sensors.Add(s);
                        }
                    }
                }

                rr.tracks.Add(t);
            }
            foreach(XmlNode xml in nodeList)
            {
                int id = Convert.ToInt32(xml.Attributes.GetNamedItem("ID").Value);
                foreach (Track t in rr.tracks)
                {
                    if (t.id == id)
                    {
                        t.nextTrack = rr.findTrack(Convert.ToInt32(xml.Attributes.GetNamedItem("NextTrack").Value));
                        t.prevTrack = rr.findTrack(Convert.ToInt32(xml.Attributes.GetNamedItem("PreviousTrack").Value));
                    }
                }
            }

            return rr;
        }
    }
}
