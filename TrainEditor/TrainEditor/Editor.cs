using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using TrainSimulator.Model;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Linq;
using System.Threading;
using System.Xml;

namespace TrainEditor
{
    public partial class Editor : Form
    {
        public Game1 game { get; set; }
        public Vector2 mousePos { get; set; }
        public ContentManager content { get; set; }
        public float straightFlip { get; set; }
        public float cornerFlip { get; set; }
        public enum Direction { Up, Down, Right, Left }
        public Direction direction;
        public Editor()
        {
            InitializeComponent();

            System.IO.Stream file = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("TrainEditor.butGfx.StraightNB.png");
            Image butImg = (Image)Image.FromStream(file);
            pbStraight.Image = butImg;
            file = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("TrainEditor.butGfx.rightTurnNB.png");
            butImg = (Image)Image.FromStream(file);
            pbCorner.Image = butImg;
            MessageBox.Show("Click in the 'track area' to set start point");
        }

        public IntPtr getDrawSurface()
        {
            return pbEditor.Handle;
        }

        private void Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void pbEditor_MouseClick(object sender, MouseEventArgs e)
        {
            mousePos = new Vector2(e.X, e.Y);
            game.ed = this;
            cornerFlip = 0;
            isCorner = false;
            game.setCont();
            direction = Direction.Right;

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                RailRoad r = game.rr;
                foreach (Track t in r.tracks)
                {
                    int xxx = t.gfx.Width + Convert.ToInt32(t.position.X);
                    int xxy = Convert.ToInt32(t.position.X);
                    int yyy = t.gfx.Height + Convert.ToInt32(t.position.Y);
                    int yyx = Convert.ToInt32(t.position.Y);
                    if (xxy < e.X && e.X < xxx)
                    {
                        if (yyx < e.Y && e.Y < yyy)
                        {
                            Signal s = new Signal(1, game.Content);
                            if (MathHelper.ToDegrees(t.rotation) == 90)
                            {
                                s.position = new Vector2(t.position.X - 20, t.position.Y + (t.position.Y / 2) );
                            }
                            else
                            {
                                s.position = new Vector2(t.position.X + (t.gfx.Width / 2) - 20, t.position.Y + t.gfx.Height + 2);
                            }
                            t.signal = s;
                        }
                    }
                }
            }

            //game.setStartPoint(place);
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                
                if (game.rr.tracks.Count() == 0)
                {
                    Track t = new StraightTrack();
                    t.gfx = content.Load<Texture2D>("StraightNB");
                    t.position = mousePos;
                    t.rotation = 0;
                    t.id = 1;
                    t.direction = true;
                    game.addTrack(t);                    
                }
                else
                {
                    Track oldT = game.rr.tracks[game.rr.tracks.Count() - 1];
                    Track t = new StraightTrack();
                    t.gfx = content.Load<Texture2D>("StraightNB");
                    t.direction = true;
                    if (direction == Direction.Right)
                    {
                        if (isCorner)
                        {
                            if (MathHelper.ToDegrees(oldT.rotation) == 180)
                            {
                                t.position = new Vector2(oldT.position.X, oldT.position.Y - 18);
                            }
                            else if (MathHelper.ToDegrees(oldT.rotation) == 270)
                                t.position = new Vector2(oldT.position.X + 59, oldT.position.Y - 59);
                        }
                        else
                        {
                            t.position = new Vector2(oldT.position.X + 50, oldT.position.Y);
                        }
                    }
                    if (direction == Direction.Left)
                    {
                        t.direction = false;
                        if (isCorner)
                        {
                            if (MathHelper.ToDegrees(oldT.rotation) == 0)
                            {
                                t.position = new Vector2(oldT.position.X - 50, oldT.position.Y);
                            }
                            else if (MathHelper.ToDegrees(oldT.rotation) == 90)
                                t.position = new Vector2(oldT.position.X - 109, oldT.position.Y + 41);
                        }
                        else
                        {
                            t.position = new Vector2(oldT.position.X - 50, oldT.position.Y);
                        }
                    }

                    if (direction == Direction.Down)
                    {
                        t.rotation = MathHelper.ToRadians(90);
                        if (isCorner)
                        {
                            if (MathHelper.ToDegrees(oldT.rotation) == 0)
                            {
                                t.position = new Vector2(oldT.position.X + 59, oldT.position.Y + 59);
                            }
                            else if (MathHelper.ToDegrees(oldT.rotation) == 270)
                                t.position = new Vector2(oldT.position.X + 18, oldT.position.Y);
                        }
                        else
                        {
                            t.position = new Vector2(oldT.position.X, oldT.position.Y + 50);
                        }
                    }
                    if (direction == Direction.Up)
                    {
                        t.direction = false;
                        t.rotation = MathHelper.ToRadians(90);
                        if (isCorner)
                        {
                            if (MathHelper.ToDegrees(oldT.rotation) == 90)
                            {
                                t.position = new Vector2(oldT.position.X, oldT.position.Y - 50);
                            }
                            else if (MathHelper.ToDegrees(oldT.rotation) == 180)
                                t.position = new Vector2(oldT.position.X- 41, oldT.position.Y - 109);
                        }
                        else
                        {
                            t.position = new Vector2(oldT.position.X, oldT.position.Y - 50);
                        }
                    }
                    t.prevTrack = game.rr.tracks[game.rr.tracks.Count() - 1];
                    t.id = oldT.id + 1;
                    game.rr.tracks[game.rr.tracks.Count() - 1].nextTrack = t;
                    game.addTrack(t);
                    isCorner = false;
                }                
            }
            
        }
        bool isCorner = false;
        private void pbCorner_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Track t = new CornerTrack();
                Track oldT = game.rr.tracks[game.rr.tracks.Count() - 1];
                t.gfx = content.Load<Texture2D>("rightTurnNB");
                t.rotation = MathHelper.ToRadians(cornerFlip);
                if (direction == Direction.Right)
                {
                    if (isCorner)
                    {
                        if (cornerFlip == 0)
                        {
                            if (MathHelper.ToDegrees(oldT.rotation) == 180)
                            {
                                t.position = new Vector2(oldT.position.X, oldT.position.Y - 18);
                                direction = Direction.Down;
                                pbStraight.Image.RotateFlip(RotateFlipType.Rotate90FlipY);
                                pbStraight.Refresh();
                            }
                            else if (MathHelper.ToDegrees(oldT.rotation) == 270)
                            {
                                t.position = new Vector2(oldT.position.X + 59, oldT.position.Y - 59);
                                direction = Direction.Down;
                                pbStraight.Image.RotateFlip(RotateFlipType.Rotate90FlipY);
                                pbStraight.Refresh();
                            }
                        }
                        else if (cornerFlip == 90)
                        {
                            if (MathHelper.ToDegrees(oldT.rotation) == 180)
                            {
                                t.position = new Vector2(oldT.position.X + 59, oldT.position.Y - 59);
                                t.direction = true;
                                direction = Direction.Up;
                                pbStraight.Image.RotateFlip(RotateFlipType.Rotate90FlipY);
                                pbStraight.Refresh();
                            }
                            else if (MathHelper.ToDegrees(oldT.rotation) == 270)
                            {
                                t.position = new Vector2(oldT.position.X + 118, oldT.position.Y - 100);
                                t.direction = true;
                                direction = Direction.Up;
                                pbStraight.Image.RotateFlip(RotateFlipType.Rotate90FlipY);
                                pbStraight.Refresh();
                            }
                        }
                    }
                    else
                    {
                        if (cornerFlip == 0)
                        {
                            t.position = new Vector2(oldT.position.X + 50, oldT.position.Y);
                            direction = Direction.Down;
                            pbStraight.Image.RotateFlip(RotateFlipType.Rotate90FlipY);
                            pbStraight.Refresh();
                        }
                        else if (cornerFlip == 90)
                        {
                            t.position = new Vector2(oldT.position.X + 109, oldT.position.Y - 41);
                            t.direction = true;
                            direction = Direction.Up;
                            pbStraight.Image.RotateFlip(RotateFlipType.Rotate90FlipY);
                            pbStraight.Refresh();
                        }
                    }
                }
                if (direction == Direction.Down)
                {
                    if (isCorner)
                    {
                        if (cornerFlip == 90)
                        {
                            if (MathHelper.ToDegrees(oldT.rotation) == 0)
                            {
                                t.position = new Vector2(oldT.position.X + 50, oldT.position.Y + 50);
                                direction = Direction.Left;
                                pbStraight.Image.RotateFlip(RotateFlipType.Rotate90FlipY);
                                pbStraight.Refresh();
                            }
                            else if (MathHelper.ToDegrees(oldT.rotation) == 270)
                            {
                                t.position = new Vector2(oldT.position.X + 18, oldT.position.Y);
                                direction = Direction.Left;
                                pbStraight.Image.RotateFlip(RotateFlipType.Rotate90FlipY);
                                pbStraight.Refresh();
                            }
                        }
                        else if (cornerFlip == 180)
                        {
                            if (MathHelper.ToDegrees(oldT.rotation) == 0)
                            {
                                t.position = new Vector2(oldT.position.X + 100, oldT.position.Y + 118);
                                t.direction = true;
                                direction = Direction.Right;
                                pbStraight.Image.RotateFlip(RotateFlipType.Rotate90FlipY);
                                pbStraight.Refresh();
                            }
                            else if (MathHelper.ToDegrees(oldT.rotation) == 270)
                            {
                                t.position = new Vector2(oldT.position.X + 50, oldT.position.Y + 50);
                                t.direction = true;
                                direction = Direction.Right;
                                pbStraight.Image.RotateFlip(RotateFlipType.Rotate90FlipY);
                                pbStraight.Refresh();
                            }
                        }
                    }
                    else
                    {
                        if (cornerFlip == 90)
                        {
                            t.position = new Vector2(oldT.position.X, oldT.position.Y + 50);
                            direction = Direction.Left;
                            pbStraight.Image.RotateFlip(RotateFlipType.Rotate90FlipY);
                            pbStraight.Refresh();
                        }
                        else if (cornerFlip == 180)
                        {
                            t.position = new Vector2(oldT.position.X + 32, oldT.position.Y + 100);
                            t.direction = true;
                            direction = Direction.Right;
                            pbStraight.Image.RotateFlip(RotateFlipType.Rotate90FlipY);
                            pbStraight.Refresh();
                        }
                    }
                }
                if (direction == Direction.Left)
                {
                    if (isCorner)
                    {
                        if (cornerFlip == 180)
                        {
                            if (MathHelper.ToDegrees(oldT.rotation) == 90)
                            {
                                t.position = new Vector2(oldT.position.X - 50, oldT.position.Y + 50);
                                direction = Direction.Up;
                                pbStraight.Image.RotateFlip(RotateFlipType.Rotate90FlipY);
                                pbStraight.Refresh();
                            }
                            else if (MathHelper.ToDegrees(oldT.rotation) == 0)
                            {
                                t.position = new Vector2(oldT.position.X, oldT.position.Y + 18);
                                direction = Direction.Up;
                                pbStraight.Image.RotateFlip(RotateFlipType.Rotate90FlipY);
                                pbStraight.Refresh();
                            }
                        }
                        else if (cornerFlip == 270)
                        {
                            if (MathHelper.ToDegrees(oldT.rotation) == 90)
                            {
                                t.position = new Vector2(oldT.position.X - 100, oldT.position.Y + 82);
                                t.direction = true;
                                direction = Direction.Down;
                                pbStraight.Image.RotateFlip(RotateFlipType.Rotate90FlipY);
                                pbStraight.Refresh();
                            }
                            else if (MathHelper.ToDegrees(oldT.rotation) == 0)
                            {
                                t.position = new Vector2(oldT.position.X - 59 , oldT.position.Y + 59);
                                t.direction = true;
                                direction = Direction.Down;
                                pbStraight.Image.RotateFlip(RotateFlipType.Rotate90FlipY);
                                pbStraight.Refresh();
                            }
                        }
                    }
                    else
                    {
                        if (cornerFlip == 180)
                        {
                            t.position = new Vector2(oldT.position.X, oldT.position.Y + 18);
                            direction = Direction.Up;
                            pbStraight.Image.RotateFlip(RotateFlipType.Rotate90FlipY);
                            pbStraight.Refresh();
                        }
                        else if (cornerFlip == 270)
                        {
                            t.position = new Vector2(oldT.position.X - 59, oldT.position.Y + 59);
                            t.direction = true;
                            direction = Direction.Down;
                            pbStraight.Image.RotateFlip(RotateFlipType.Rotate90FlipY);
                            pbStraight.Refresh();
                        }
                    }
                }

                if (direction == Direction.Up)
                {
                    if (isCorner)
                    {
                        if (cornerFlip == 0)
                        {
                            if (MathHelper.ToDegrees(oldT.rotation) == 90)
                            {
                                t.position = new Vector2(oldT.position.X - 50, oldT.position.Y - 50);
                                t.direction = true;
                                direction = Direction.Left;
                                pbStraight.Image.RotateFlip(RotateFlipType.Rotate90FlipY);
                                pbStraight.Refresh();
                            }
                            else if (MathHelper.ToDegrees(oldT.rotation) == 180)
                            {
                                t.position = new Vector2(oldT.position.X-100, oldT.position.Y - 118);
                                t.direction = true;
                                direction = Direction.Left;
                                pbStraight.Image.RotateFlip(RotateFlipType.Rotate90FlipY);
                                pbStraight.Refresh();
                            }
                        }
                        else if (cornerFlip == 270)
                        {
                            if (MathHelper.ToDegrees(oldT.rotation) == 90)
                            {
                                t.position = new Vector2(oldT.position.X - 18, oldT.position.Y);
                                direction = Direction.Right;
                                pbStraight.Image.RotateFlip(RotateFlipType.Rotate90FlipY);
                                pbStraight.Refresh();
                            }
                            else if (MathHelper.ToDegrees(oldT.rotation) == 180)
                            {
                                t.position = new Vector2(oldT.position.X - 59, oldT.position.Y- 59);                                
                                direction = Direction.Right;
                                pbStraight.Image.RotateFlip(RotateFlipType.Rotate90FlipY);
                                pbStraight.Refresh();
                            }
                        }
                    }
                    else
                    {
                        if (cornerFlip == 0)
                        {
                            t.position = new Vector2(oldT.position.X - 50 , oldT.position.Y - 50);
                            t.direction = true;
                            direction = Direction.Left;
                            pbStraight.Image.RotateFlip(RotateFlipType.Rotate90FlipY);
                            pbStraight.Refresh();
                        }
                        else if (cornerFlip == 270)
                        {
                            t.position = new Vector2(oldT.position.X - 18, oldT.position.Y);
                            
                            direction = Direction.Right;
                            pbStraight.Image.RotateFlip(RotateFlipType.Rotate90FlipY);
                            pbStraight.Refresh();
                        }
                    }
                }                
                t.prevTrack = game.rr.tracks[game.rr.tracks.Count() - 1];
                t.id = oldT.id + 1;
                game.rr.tracks[game.rr.tracks.Count() - 1].nextTrack = t;
                game.addTrack(t);
                isCorner = true;
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                pbCorner.Image.RotateFlip(RotateFlipType.Rotate270FlipXY);
                pbCorner.Refresh();
                cornerFlip = cornerFlip + 90;
                if (cornerFlip > 270)
                {
                    cornerFlip = 0;
                }                
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            game.rr.tracks[0].prevTrack = game.rr.tracks[game.rr.tracks.Count() -1];
            game.rr.tracks[game.rr.tracks.Count() -1].nextTrack = game.rr.tracks[0];
            string fileName = txtFilename.Text.ToString();
            XDocument xDoc = new XDocument(new XElement("RailRoad", 
                from tracks in game.rr.tracks select new XElement("Track",
                    new XAttribute("ID", tracks.id), new XAttribute("PreviousTrack", tracks.prevTrack.id),
                        new XAttribute("NextTrack", tracks.nextTrack.id), new XAttribute("Type", tracks.GetType().ToString().Replace("TrainSimulator.Model.", "")),
                        new XAttribute("Direction", tracks.direction),
                            new XAttribute("X", tracks.position.X), new XAttribute("Y", tracks.position.Y), new XAttribute("Rotation", MathHelper.ToDegrees(tracks.rotation))
                            )));
            xDoc.Save(fileName.ToString() + ".xml");
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("TrainEditor.exe");
            this.Close();
        }
    }
}
