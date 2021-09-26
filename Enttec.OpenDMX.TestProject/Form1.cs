using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Enttec.OpenDMX.TestProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            opendmx = new Interface(0x0403, 0x6001);
            fixture = new Fixture(opendmx, 1);
            fixture.Channels.Add("Pan Left/Right Coarse", 1, 0, 255);
            fixture.Channels.Add("Pan Left/Right Fine", 2, 0, 255);
            fixture.Channels.Add("Tilt Up/Down Coarse", 3, 0, 255);
            fixture.Channels.Add("Tilt Up/Down Fine", 4, 0, 255);
            fixture.Channels.Add("Pan/Tilt Speed", 5, 0, 255);
            fixture.Channels.Add("Color Wheel", 6, 0, 255);
            fixture.Channels.Add("Gobo Wheel", 7, 0, 255);
            fixture.Channels.Add("Gobo Rotation", 8, 0, 255);
            fixture.Channels.Add("Prism", 9, 0, 255);
            fixture.Channels.Add("Dimmer", 10, 0, 255);
            fixture.Channels.Add("Shutter", 11, 0, 255);

            fixture.Reset();
        }

        private Interface opendmx = null;
        private Fixture fixture = null;

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            fixture.Channels[0].Value = (byte)trackBar1.Value;
        }
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            fixture.Channels[2].Value = (byte)trackBar2.Value;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            fixture.Reset();
        }

        private void chkLightOn_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLightOn.Checked)
            {
                fixture.Channels[9].Value = 255;
                fixture.Channels[10].Value = 255;
            }
            else
            {
                fixture.Channels[9].Value = 0;
                fixture.Channels[10].Value = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fixture.Channels[5].Value = 28;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fixture.Channels[5].Value = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            fixture.Channels[5].Value = 35;
        }
    }
}
