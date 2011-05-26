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

            lbSensors.Items.Clear();
            lbSensors.Items.Add("ID:                     Status");
            lbSensors.Items.Add("");

            Dictionary<Track, TrainSet> tracks = railroad.getTrackStatus();
            foreach(Track t in tracks.Keys)
            {
                string status = "Track #" + t.id;
                if (tracks[t] != null)
                    status += "     " + tracks[t].name + "    ";
                else
                    status += "                      ";
  
                if(t is SwitchLeft || t is SwitchRight)
                {
                    if (t.turn)
                        status += "Switched";
                    else
                        status += "Through";

                }
                lbTracks.Items.Add(status);

                if (t.signal != null)
                {
                    lbSignals.Items.Add(t.signal);
                }

                if (t.sensor != null)
                {
                    status = "";
                    if (t.sensor.state == Sensor.State.On)
                        status = "          On";
                    else
                        status = "          Off";
                    lbSensors.Items.Add("Sensor #" + t.sensor.id + status);
                }              
            }

            foreach (TrainSet train in railroad.trains)
            {
                lbTrains.Items.Add(train);
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

        private void lbTrains_MouseDown(object sender, MouseEventArgs e)
        {
            if (lbTrains.SelectedIndex > 1)
            {
                TrainSet t = (TrainSet)lbTrains.SelectedItem;
                if (t.locoDriver.driverState == LocoDriver.DriverState.Off)
                    t.locoDriver.StartDriving();
            }
        }

        private void lbTracks_MouseDown(object sender, MouseEventArgs e)
        {
            if (lbTracks.SelectedIndex > 1)
            {
                game.railroad.tracks[lbTracks.SelectedIndex - 2].turn = !game.railroad.tracks[lbTracks.SelectedIndex - 2].turn;
            }
        }
    }
}
