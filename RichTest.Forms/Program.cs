using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RichTest.Forms
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

            using (Mutex mutex = new Mutex(true, "RichTest", out createdNew))
            {
                if (createdNew)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    Application.Run(new MyCustomApplicationContext());
                }
                else
                {
                    MessageBox.Show("You don't have to run this app twice.", "Rich Person Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        public class MyCustomApplicationContext : ApplicationContext
        {
            private static readonly string StartupKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
            private static readonly string StartupValue = "My Phone Messages";
            private NotifyIcon trayIcon;
            private ContextMenuStrip contextMenuStrip;
            private ToolStripMenuItem toolStripOpen = new ToolStripMenuItem();
            private ToolStripSeparator toolStripSep = new ToolStripSeparator();
            private ToolStripMenuItem toolStripExit = new ToolStripMenuItem();

            public MyCustomApplicationContext()
            {
                toolStripOpen.Text = "Show messages";
                toolStripOpen.Click += ToolStripOpen_Click;

                toolStripExit.Text = "Quit";
                toolStripExit.Click += ToolStripExit_Click;

                AddRunOnStartupRegistry();

                contextMenuStrip = new ContextMenuStrip()
                {
                    DropShadowEnabled = false,
                    ShowCheckMargin = true,
                    ShowImageMargin = false,
                    Size = new System.Drawing.Size(205, 170)
                };

                contextMenuStrip.Items.AddRange(new ToolStripItem[]
                {
                    toolStripOpen,
                    toolStripSep,
                    toolStripExit
                });

                this.contextMenuStrip.Items[0].Font = new Font(this.contextMenuStrip.Items[0].Font, FontStyle.Bold);

                // Set             

                this.contextMenuStrip.Renderer = new MyCustomRenderer();

                // Initialize Tray Icon
                trayIcon = new NotifyIcon()
                {
                    ContextMenuStrip = contextMenuStrip,
                    Icon = Resources.PhoneMessagesColorized,
                    Visible = true,
                    Text = "View Your Phone messages"
                };
                trayIcon.MouseDoubleClick += TrayIcon_MouseDoubleClick;
                trayIcon.MouseClick += TrayIcon_MouseClick;
            }

            private void TrayIcon_MouseClick(object sender, MouseEventArgs e)
            {
                Process.Start("ms-phone://photos");
            }

            private void ToolStripExit_Click(object sender, EventArgs e)
            {
                // Hide tray icon, otherwise it will remain shown until user mouses over it
                trayIcon.Visible = false;

                Application.Exit();
            }

            private void ToolStripOpen_Click(object sender, EventArgs e)
            {
                Process.Start("ms-phone://messages");
            }

            private void TrayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
            {
                Process.Start("ms-phone://messages");
            }

            public bool IsProcessOpen(string name)
            {
                //here we're going to get a list of all running processes on
                //the computer
                foreach (Process clsProcess in Process.GetProcesses())
                {
                    //now we're going to see if any of the running processes
                    //match the currently running processes. Be sure to not
                    //add the .exe to the name you provide, i.e: NOTEPAD,
                    //not NOTEPAD.EXE or false is always returned even if
                    //notepad is running.
                    //Remember, if you have the process running more than once, 
                    //say IE open 4 times the loop thr way it is now will close all 4,
                    //if you want it to just close the first one it finds
                    //then add a return; after the Kill
                    if (clsProcess.ProcessName.Contains(name))
                    {
                        //if the process is found to be running then we
                        //return a true
                        return true;
                    }
                }
                //otherwise we return a false
                return false;
            }

            private void RemoveStartupObject()
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(StartupKey, true);
                key.DeleteValue(StartupValue, false);

                Debug.WriteLine("Removed from Startup");
            }

            private void AddRunOnStartupRegistry()
            {
                // Set the application to run at startup
                RegistryKey key = Registry.CurrentUser.OpenSubKey(StartupKey, true);
                key.SetValue(StartupValue, Application.ExecutablePath.ToString());

                Debug.WriteLine("Added to Startup");
            }

            void Exit(object sender, EventArgs e)
            {
                // Hide tray icon, otherwise it will remain shown until user mouses over it
                trayIcon.Visible = false;

                Application.Exit();
            }
        }

        private class MyCustomRenderer : ToolStripProfessionalRenderer
        {
            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs myMenu)
            {
                if (!myMenu.Item.Selected)
                    base.OnRenderMenuItemBackground(myMenu);
                else
                {
                    if (myMenu.Item.Enabled)
                    {
                        Rectangle menuRectangle = new Rectangle(Point.Empty, myMenu.Item.Size);
                        //Fill Color
                        myMenu.Graphics.FillRectangle(Brushes.LightSkyBlue, menuRectangle);
                        // Border Color
                        // myMenu.Graphics.DrawRectangle(Pens.Lime, 1, 0, menuRectangle.Width - 2, menuRectangle.Height - 1);
                    }
                    else
                    {
                        Rectangle menuRectangle = new Rectangle(Point.Empty, myMenu.Item.Size);
                        //Fill Color
                        myMenu.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(20, 128, 128, 128)), menuRectangle);
                    }

                }
            }

            protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var r = new Rectangle(e.ImageRectangle.Location, e.ImageRectangle.Size);
                r.Inflate(1, 1);
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(20, 128, 128, 128)), r);
                //r.Inflate(-4, -4);
                e.Graphics.DrawLines(Pens.Gray, new Point[]
                {
                    new Point(r.Left + 4, 10), //2
                    new Point(r.Left - 2 + r.Width / 2,  r.Height / 2 + 4), //3
                    new Point(r.Right - 4, r.Top + 4)
                });
            }
        }
    }
}
