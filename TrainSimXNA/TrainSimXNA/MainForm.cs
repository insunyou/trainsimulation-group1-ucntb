using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TrainSimulator.Model;

namespace TrainSimXNA
{
    public delegate void UpdatePanels(RailRoad railroad);

    public partial class MainForm : Form
    {
        public Game1 game { get; set; }

        public MainForm()
        {
            InitializeComponent();
        }

        public void updatePanels(RailRoad railroad)
        {
            lbTracks.Items.Clear();
            lbTrains.Items.Clear();
            foreach(Track t in railroad.tracks)
                lbTracks.Items.Add("Track #" + t.id);

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
            game.startSim();
        }
    }
}
