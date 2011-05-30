namespace TrainSimXNA
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pctSurface = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbTracks = new System.Windows.Forms.ListBox();
            this.btnStartSim = new System.Windows.Forms.Button();
            this.lbTrains = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbSignals = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbSensors = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.pctSurface)).BeginInit();
            this.SuspendLayout();
            // 
            // pctSurface
            // 
            this.pctSurface.Location = new System.Drawing.Point(12, 12);
            this.pctSurface.Name = "pctSurface";
            this.pctSurface.Size = new System.Drawing.Size(648, 315);
            this.pctSurface.TabIndex = 0;
            this.pctSurface.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(682, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tracks";
            // 
            // lbTracks
            // 
            this.lbTracks.FormattingEnabled = true;
            this.lbTracks.Location = new System.Drawing.Point(685, 28);
            this.lbTracks.Name = "lbTracks";
            this.lbTracks.Size = new System.Drawing.Size(213, 381);
            this.lbTracks.TabIndex = 2;
            this.lbTracks.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbTracks_MouseDown);
            // 
            // btnStartSim
            // 
            this.btnStartSim.Location = new System.Drawing.Point(685, 423);
            this.btnStartSim.Name = "btnStartSim";
            this.btnStartSim.Size = new System.Drawing.Size(213, 23);
            this.btnStartSim.TabIndex = 3;
            this.btnStartSim.Text = "Start Simulator";
            this.btnStartSim.UseVisualStyleBackColor = true;
            this.btnStartSim.Click += new System.EventHandler(this.btnStartSim_Click);
            // 
            // lbTrains
            // 
            this.lbTrains.FormattingEnabled = true;
            this.lbTrains.Location = new System.Drawing.Point(12, 351);
            this.lbTrains.Name = "lbTrains";
            this.lbTrains.Size = new System.Drawing.Size(252, 95);
            this.lbTrains.TabIndex = 4;
            this.lbTrains.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbTrains_MouseDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 335);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Trains";
            // 
            // lbSignals
            // 
            this.lbSignals.FormattingEnabled = true;
            this.lbSignals.Location = new System.Drawing.Point(287, 351);
            this.lbSignals.Name = "lbSignals";
            this.lbSignals.Size = new System.Drawing.Size(178, 95);
            this.lbSignals.TabIndex = 6;
            this.lbSignals.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbSignals_MouseDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(284, 335);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Signals";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(483, 335);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Sensors";
            // 
            // lbSensors
            // 
            this.lbSensors.FormattingEnabled = true;
            this.lbSensors.Location = new System.Drawing.Point(486, 351);
            this.lbSensors.Name = "lbSensors";
            this.lbSensors.Size = new System.Drawing.Size(174, 95);
            this.lbSensors.TabIndex = 9;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 480);
            this.Controls.Add(this.lbSensors);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbSignals);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbTrains);
            this.Controls.Add(this.btnStartSim);
            this.Controls.Add(this.lbTracks);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pctSurface);
            this.Name = "MainForm";
            this.Text = "Train Simulator Ultimate Edition 2011";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pctSurface)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox pctSurface;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbTracks;
        private System.Windows.Forms.Button btnStartSim;
        private System.Windows.Forms.ListBox lbTrains;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lbSignals;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lbSensors;

    }
}