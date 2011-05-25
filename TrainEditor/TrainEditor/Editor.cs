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
namespace TrainEditor
{
    public partial class Editor : Form
    {
        public Game1 game { get; set; }
        public Vector2 mousePos { get; set; }
        public ContentManager content { get; set; }
        public float straightFlip { get; set; }
        public float cornerFlip { get; set; }
        public Editor()
        {
            InitializeComponent();

            System.IO.Stream file = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("TrainEditor.butGfx.StraightNB.png");
            Image butImg = (Image)Image.FromStream(file);
            pbStraight.Image = butImg;
            file = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("TrainEditor.butGfx.rightTurnNB.png");
            butImg = (Image)Image.FromStream(file);
            pbCorner.Image = butImg;
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

            game.setCont();
            straightFlip = 0;

            //game.setStartPoint(place);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            game.rr.tracks = new List<Track>();
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
                    game.addTrack(t);
                }
                else
                {
                    Track oldT = game.rr.tracks[game.rr.tracks.Count() - 1];
                    Track t = new StraightTrack();
                    t.gfx = content.Load<Texture2D>("StraightNB");
                    t.rotation = MathHelper.ToRadians(straightFlip);
                    if (straightFlip == 90)
                    {
                        if (isCorner)
                        {
                            if (cornerFlip == 0)
                                t.position = new Vector2(oldT.position.X + oldT.gfx.Width, oldT.position.Y + oldT.gfx.Height);
                            else if (cornerFlip == 180)
                                t.position = new Vector2(oldT.position.X - oldT.gfx.Width + t.gfx.Height, oldT.position.Y - t.gfx.Width - oldT.gfx.Width);
                        }
                        else
                        {                            
                            t.position = new Vector2(oldT.position.X, oldT.position.Y + 65);
                        }
                    }
                    else
                    {
                        if (isCorner)
                        {
                            if (cornerFlip == 90)
                                t.position = new Vector2(oldT.position.X - oldT.gfx.Width - t.gfx.Width, oldT.position.Y + oldT.gfx.Height - t.gfx.Height);                            
                        }
                        else
                        {
                            t.position = new Vector2(oldT.position.X + 65, oldT.position.Y);
                        }
                    }
                    t.prevTrack = game.rr.tracks[game.rr.tracks.Count() - 1];
                    t.id = oldT.id + 1;
                    game.rr.tracks[game.rr.tracks.Count() - 1].nextTrack = t;
                    game.addTrack(t);
                    isCorner = false;
                }
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                pbStraight.Image.RotateFlip(RotateFlipType.Rotate90FlipY);
                pbStraight.Refresh();
                straightFlip = straightFlip + 90;
                if (straightFlip > 90)
                {
                    straightFlip = 0;
                }
            }
        }
        bool isCorner = false;
        private void pbCorner_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                isCorner = true;
                Track t = new CornerTrack();
                Track oldT = game.rr.tracks[game.rr.tracks.Count() - 1];
                t.gfx = content.Load<Texture2D>("rightTurnNB");
                t.rotation = MathHelper.ToRadians(cornerFlip);                
                if (cornerFlip == 0)
                    t.position = new Vector2(oldT.position.X + 65, oldT.position.Y);
                else if (cornerFlip == 90)
                    t.position = new Vector2(oldT.position.X, oldT.position.Y + oldT.gfx.Width);
                else if (cornerFlip == 180)
                    t.position = new Vector2(oldT.position.X - 65, oldT.position.Y + oldT.gfx.Width);
                else if (cornerFlip == 270)
                    t.position = new Vector2(oldT.position.X - oldT.gfx.Height, oldT.position.Y);
                game.addTrack(t);
                t.prevTrack = game.rr.tracks[game.rr.tracks.Count() - 1];
                t.id = oldT.id + 1;
                game.rr.tracks[game.rr.tracks.Count() - 1].nextTrack = t;
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

    }
}
