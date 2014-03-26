using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Enttec.OpenDMX.TestProject
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // FTD2XX.Device[] count = FTD2XX.Device.GetDevices();

            Application.Run(new Form1());
        }
    }
}
