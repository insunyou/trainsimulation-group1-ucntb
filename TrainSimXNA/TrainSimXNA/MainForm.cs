using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TrainSimulator.Model;

namespace TrainSimXNA
{
    public delegate void UpdatePanels(RailRoad railroad);

    public partial class MainForm : Form
    {
        public Game1 game { get; set; }
        private bool running;

        public MainForm()
        {
            InitializeComponent();
        }

        public void updatePanels(RailRoad railroad)
        {
            lbTracks.Items.Clear();
            lbTracks.Items.Add("ID:               Occupied");
            lbTracks.Items.Add("");
            lbTrains.Items.Clear();
            Dictionary<Track, TrainSet> tracks = railroad.getTrackStatus();
            foreach(Track t in tracks.Keys)
            {
                string status = "";
                if (tracks[t] != null)
                    status = "     " + tracks[t].name;
                lbTracks.Items.Add("Track #" + t.id + status);
            }

            foreach (TrainSet train in railroad.trains)
                lbTrains.Items.Add(train.name + " " + String.Format("{0:0.00}", train.engine.currentSpeed));
        }

        public IntPtr getDrawSurface()
	    {
	        return pctSurface.Handle;
	    }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnStartSim_Click(object sender, EventArgs e)
        {
            if (running)
            {
                btnStartSim.Text = "Start simulator";
                running = false;
                game.stopSim();
            }
            else
            {
                btnStartSim.Text = "Stop simulator";
                running = true;
                game.startSim();
            }
        }
    }
}
