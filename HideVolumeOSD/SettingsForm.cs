using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HideVolumeOSD
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            slShowOnChangeMs.Value = Settings.Default.ShowMs;
            lbShowOnChangeMs.Text = Settings.Default.ShowMs + " ms";
        }

        private void slShowOnChangeMs_ValueChanged(object sender, EventArgs e)
        {
            lbShowOnChangeMs.Text = slShowOnChangeMs.Value + " ms";
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.ShowMs = slShowOnChangeMs.Value;
            Settings.Default.Save();
        }
    }
}
