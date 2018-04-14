using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GBrightness
{
    public partial class Form1 : Form
    {
        int iCount = 0; //global counter for hiding/closing form after certian period of inactivity
        byte[] bLevels; //array of valid level values
        string[] arguments;

        public Form1(string[] args)
        {
            arguments = args;
            InitializeComponent();
        }
       
        //In case of an incompatible system, the form has to be shown in order to close the app...as far as I know ^^
        private void Form1_Shown(object sender, EventArgs e)
        {
            bLevels = GetBrightnessLevels(); //get the level array for this system
            if (bLevels.Count() == 0) //"WmiMonitorBrightness" is not supported by the system
            {
                Application.Exit();
            }
            else
            {
                trackBar1.TickFrequency = bLevels.Count(); //adjust the trackbar ticks according the number of possible brightness levels
                trackBar1.Maximum = bLevels.Count() - 1;
                trackBar1.Update();
                trackBar1.Refresh();
                check_brightness();
                timer1.Enabled = true;  //timer for closing form
                //check the arguments
                if (Array.FindIndex(arguments, item => item.Contains("%")) > -1)
                    startup_brightness();
                if (arguments.Length == 0 || Array.IndexOf(arguments, "hide") > -1) //hide the trackbar initially if no arguments are passed
                    this.Hide();

            }
        }
        private void check_brightness()
        {
            int iBrightness = GetBrightness(); //get the actual value of brightness
            int i = Array.IndexOf(bLevels, (byte)iBrightness);
            if (i < 0) i = 1;
            change_icon(iBrightness);
            trackBar1.Value = i;
        }

        private void startup_brightness()
        {
            string sPercent = arguments[Array.FindIndex(arguments, item => item.Contains("%"))];
            if (sPercent.Length > 1)
            {
                int iPercent = Convert.ToInt16(sPercent.Split('%').ElementAt(0));
                if (iPercent >= 0 && iPercent <= bLevels[bLevels.Count() - 1])
                {
                    byte level = 100;
                    foreach (byte item in bLevels)
                    {
                        if (item >= iPercent)
                        {
                            level = item;
                            break;
                        }
                    }
                    SetBrightness(level);
                    check_brightness();
                }

            }
        }

        //change the icon according to brightness
        private void change_icon(int iBrightness)
        {
            if (iBrightness < 25) { }
            //pictureBox1.Image = ScreenBrightness.Properties.Resources.sonne_2;
            else
            {
                if (iBrightness < 75) { }
                //    pictureBox1.Image = ScreenBrightness.Properties.Resources.sonne_1;
                else { }
                //    pictureBox1.Image = ScreenBrightness.Properties.Resources.sonne;
            }
            label1.Text = iBrightness.ToString() + "%";
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            SetBrightness(bLevels[trackBar1.Value]);
            change_icon(bLevels[trackBar1.Value]);
            iCount = 0; //reset inactivity counter
        }

        //get the actual percentage of brightness
        static int GetBrightness()
        {
            //define scope (namespace)
            System.Management.ManagementScope s = new System.Management.ManagementScope("root\\WMI");

            //define query
            System.Management.SelectQuery q = new System.Management.SelectQuery("WmiMonitorBrightness");

            //output current brightness
            System.Management.ManagementObjectSearcher mos = new System.Management.ManagementObjectSearcher(s, q);

            System.Management.ManagementObjectCollection moc = mos.Get();

            //store result
            byte curBrightness = 0;
            foreach (System.Management.ManagementObject o in moc)
            {
                curBrightness = (byte)o.GetPropertyValue("CurrentBrightness");
                break; //only work on the first object
            }

            moc.Dispose();
            mos.Dispose();

            return (int)curBrightness;
        }

        //array of valid brightness values in percent
        static byte[] GetBrightnessLevels()
        {
            //define scope (namespace)
            System.Management.ManagementScope s = new System.Management.ManagementScope("root\\WMI");

            //define query
            System.Management.SelectQuery q = new System.Management.SelectQuery("WmiMonitorBrightness");

            //output current brightness
            System.Management.ManagementObjectSearcher mos = new System.Management.ManagementObjectSearcher(s, q);
            byte[] BrightnessLevels = new byte[0];

            try
            {
                System.Management.ManagementObjectCollection moc = mos.Get();

                //store result


                foreach (System.Management.ManagementObject o in moc)
                {
                    BrightnessLevels = (byte[])o.GetPropertyValue("Level");
                    break; //only work on the first object
                }

                moc.Dispose();
                mos.Dispose();

            }
            catch (Exception)
            {
                MessageBox.Show("Sorry, Your System does not support this brightness control...");

            }

            return BrightnessLevels;
        }

        static void SetBrightness(byte targetBrightness)
        {
            //define scope (namespace)
            System.Management.ManagementScope s = new System.Management.ManagementScope("root\\WMI");

            //define query
            System.Management.SelectQuery q = new System.Management.SelectQuery("WmiMonitorBrightnessMethods");

            //output current brightness
            System.Management.ManagementObjectSearcher mos = new System.Management.ManagementObjectSearcher(s, q);

            System.Management.ManagementObjectCollection moc = mos.Get();

            foreach (System.Management.ManagementObject o in moc)
            {
                o.InvokeMethod("WmiSetBrightness", new Object[] { UInt32.MaxValue, targetBrightness }); //note the reversed order - won't work otherwise!
                break; //only work on the first object
            }

            moc.Dispose();
            mos.Dispose();
        }

        //timer for hiding/closing form
        private void timer1_Tick(object sender, EventArgs e)
        {
            iCount++;
            if (iCount > 2)
            {
                if (Array.IndexOf(arguments, "quit") > -1)
                    this.Close();
                else
                {
                    this.Hide();
                    timer1.Stop();
                    iCount = 0;
                }

            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Point p = new Point(MousePosition.X, MousePosition.Y);
                Rectangle r = Screen.GetBounds(p);
                //find the right position next to the icon
                if (p.X > r.Width / 2)
                {
                    if (p.X + 140 > r.Width)
                        this.Left = r.Width - 275;
                    else
                        this.Left = p.X - 140;
                }
                else
                    this.Left = p.X;

                if (p.Y > r.Height / 2)
                    this.Top = p.Y - 60;
                else
                    this.Top = p.Y;
                check_brightness();
                this.Show();
                this.Activate();
                timer1.Start();
            }
            else
                contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void notifyIcon1_MouseMove(object sender, MouseEventArgs e)
        {
            notifyIcon1.Text = "screen brightness " + GetBrightness().ToString() + "%";
        }

    }
}
