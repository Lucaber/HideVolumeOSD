namespace HideVolumeOSD
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.slShowOnChangeMs = new System.Windows.Forms.TrackBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbShowOnChangeMs = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.slShowOnChangeMs)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // slShowOnChangeMs
            // 
            this.slShowOnChangeMs.LargeChange = 50;
            this.slShowOnChangeMs.Location = new System.Drawing.Point(6, 19);
            this.slShowOnChangeMs.Maximum = 2000;
            this.slShowOnChangeMs.Name = "slShowOnChangeMs";
            this.slShowOnChangeMs.Size = new System.Drawing.Size(575, 45);
            this.slShowOnChangeMs.SmallChange = 10;
            this.slShowOnChangeMs.TabIndex = 0;
            this.slShowOnChangeMs.TickFrequency = 50;
            this.slShowOnChangeMs.ValueChanged += new System.EventHandler(this.slShowOnChangeMs_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbShowOnChangeMs);
            this.groupBox1.Controls.Add(this.slShowOnChangeMs);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(587, 85);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Show OSD on VolumeKeys";
            // 
            // lbShowOnChangeMs
            // 
            this.lbShowOnChangeMs.AutoSize = true;
            this.lbShowOnChangeMs.Location = new System.Drawing.Point(279, 51);
            this.lbShowOnChangeMs.Name = "lbShowOnChangeMs";
            this.lbShowOnChangeMs.Size = new System.Drawing.Size(29, 13);
            this.lbShowOnChangeMs.TabIndex = 1;
            this.lbShowOnChangeMs.Text = "0 ms";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 105);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.slShowOnChangeMs)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TrackBar slShowOnChangeMs;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbShowOnChangeMs;
    }
}