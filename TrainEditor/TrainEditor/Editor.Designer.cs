namespace TrainEditor
{
    partial class Editor
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
            this.pbEditor = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.pbStraight = new System.Windows.Forms.PictureBox();
            this.pbCorner = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbEditor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbStraight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCorner)).BeginInit();
            this.SuspendLayout();
            // 
            // pbEditor
            // 
            this.pbEditor.Location = new System.Drawing.Point(12, 12);
            this.pbEditor.Name = "pbEditor";
            this.pbEditor.Size = new System.Drawing.Size(648, 315);
            this.pbEditor.TabIndex = 0;
            this.pbEditor.TabStop = false;
            this.pbEditor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbEditor_MouseClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 351);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pbStraight
            // 
            this.pbStraight.Location = new System.Drawing.Point(106, 351);
            this.pbStraight.Name = "pbStraight";
            this.pbStraight.Size = new System.Drawing.Size(100, 50);
            this.pbStraight.TabIndex = 2;
            this.pbStraight.TabStop = false;
            this.pbStraight.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // pbCorner
            // 
            this.pbCorner.Location = new System.Drawing.Point(213, 351);
            this.pbCorner.Name = "pbCorner";
            this.pbCorner.Size = new System.Drawing.Size(100, 50);
            this.pbCorner.TabIndex = 3;
            this.pbCorner.TabStop = false;
            this.pbCorner.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbCorner_MouseClick);
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 434);
            this.Controls.Add(this.pbCorner);
            this.Controls.Add(this.pbStraight);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pbEditor);
            this.Name = "Editor";
            this.Text = "Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Editor_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pbEditor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbStraight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCorner)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox pbEditor;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pbStraight;
        private System.Windows.Forms.PictureBox pbCorner;
    }
}