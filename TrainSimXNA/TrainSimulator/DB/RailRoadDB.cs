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
                    //if (t.direction)
                    //{
                        t.gfx = content.Load<Texture2D>("rightTurnNB");
                    //}
                    //else
                    //{
                    //    t.gfx = content.Load<Texture2D>("leftTurnNB");
                    //}
                }

                if (type.Equals("SwitchRight"))
                {
                    t = new SwitchRight();
                    t.direction = Convert.ToBoolean(n.Attributes.GetNamedItem("Direction").Value.ToString());
                    t.turn = Convert.ToBoolean(n.Attributes.GetNamedItem("Switch").Value.ToString());
                   
                  
                    
                    

                    t.gfx = content.Load<Texture2D>("switchRight");
                }

                if (type.Equals("SwitchLeft"))
                {
                    t = new SwitchLeft();
                    t.direction = Convert.ToBoolean(n.Attributes.GetNamedItem("Direction").Value.ToString());
                    t.turn = Convert.ToBoolean(n.Attributes.GetNamedItem("Switch").Value.ToString());
                    
                  

                    t.gfx = content.Load<Texture2D>("switchLeft");
                }

                t.rotation = MathHelper.ToRadians(Convert.ToInt32(n.Attributes.GetNamedItem("Rotation").Value));

                t.position = new Vector2(Convert.ToInt32(n.Attributes.GetNamedItem("X").Value), Convert.ToInt32(n.Attributes.GetNamedItem("Y").Value));
                t.id = Convert.ToInt32(n.Attributes.GetNamedItem("ID").Value);

                foreach (XmlNode nn in n.ChildNodes)
                {
                        if (nn.Name == "Signal")
                        {
                            Signal s = new Signal(Convert.ToInt32(nn.Attributes.GetNamedItem("ID").Value), content);
                            s.position = new Vector2(Convert.ToInt32(n.Attributes.GetNamedItem("X").Value), Convert.ToInt32(n.Attributes.GetNamedItem("Y").Value));
                            switch (nn.Attributes.GetNamedItem("ID").Value)
                            {
                                case "Stop": s.state = Signal.State.Stop; break;
                                case "Go": s.state = Signal.State.Go; break;
                                case "Off": s.state = Signal.State.Off; break;
                            }

                            t.signal = s;
                            System.Diagnostics.Debug.WriteLine("Signal added");
                        }
                        //else if (nn.Name == "Sensor")
                        //{
                        //    Sensor s = new Sensor(Convert.ToInt32(nn.Attributes.GetNamedItem("ID").Value));
                        //    t.sensors.Add(s);
                        //}
                    
                }

                rr.tracks.Add(t);
            }
            foreach (XmlNode xml in nodeList)
            {
                int id = Convert.ToInt32(xml.Attributes.GetNamedItem("ID").Value);
                foreach (Track t in rr.tracks)
                {
                    if (t.id == id)
                    {
                        t.prevTrack = rr.findTrack(Convert.ToInt32(xml.Attributes.GetNamedItem("PreviousTrack").Value));
                        t.nextTrack = rr.findTrack(Convert.ToInt32(xml.Attributes.GetNamedItem("NextTrack").Value));

                        if (t is SwitchLeft || t is SwitchRight)
                        {
                            t.switchTrack = rr.findTrack(Convert.ToInt32(xml.Attributes.GetNamedItem("SwitchTrackID").Value));
                        }
                    }
                }
            }

            return rr;
        }
    }
}
