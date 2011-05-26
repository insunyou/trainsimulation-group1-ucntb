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
            lbTrains.Items.Add("ID:               Speed");
            lbTrains.Items.Add("");


           
            lbSignals.Items.Clear();
            lbSignals.Items.Add("ID:                Status");
            lbSignals.Items.Add("");
            
            Dictionary<Track, TrainSet> tracks = railroad.getTrackStatus();
            foreach(Track t in tracks.Keys)
            {
                string status = "";
                if (tracks[t] != null)
                    status = "     " + tracks[t].name;
                lbTracks.Items.Add("Track #" + t.id + status);

                if (t.signal != null)
                {
                    lbSignals.Items.Add(t.signal);
                }
                
            }

            foreach (TrainSet train in railroad.trains)
            {
                string status = "";
                if (train.locoDriver.driverState == LocoDriver.DriverState.Accelerate)
                    status = "   Accelerating";
                else if (train.locoDriver.driverState == LocoDriver.DriverState.Decelerate)
                    status = "   Decelerating";
                else if (train.locoDriver.driverState == LocoDriver.DriverState.Cruise)
                    status = "   Cruise";
                else if (train.locoDriver.driverState == LocoDriver.DriverState.Stopped)
                    status = "   Stopped";
                lbTrains.Items.Add(train.name + "       " + String.Format("{0:0.00}", train.engine.currentSpeed) + status);
            }
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

        private void lbSignals_MouseDown(object sender, MouseEventArgs e)
        {
            if (lbSignals.SelectedIndex > 1)
            {
                Signal s = (Signal)lbSignals.SelectedItem;
                if (s.state == Signal.State.Go)
                    s.state = Signal.State.Stop;
                else
                    s.state = Signal.State.Go;
            }
        }
    }
}
