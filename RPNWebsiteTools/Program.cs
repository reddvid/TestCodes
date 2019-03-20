using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using RPNWebsiteTools.Classes;

namespace RPNWebsiteTools
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool createdNew = true;

            using (Mutex mutex = new Mutex(true, "RPNWebsiteTools", out createdNew))
            {
                if (createdNew)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    Application.Run(new CustomAppContext());
                }
                else
                {
                    MessageBox.Show("The app is already running and silently lives on the system tray.", "RPN Website Tools", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }


        }
    }
}
