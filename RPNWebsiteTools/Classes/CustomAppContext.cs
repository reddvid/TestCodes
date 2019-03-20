using Microsoft.Win32;
using RPNWebsiteTools.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPNWebsiteTools.Classes
{
    class CustomAppContext : ApplicationContext
    {
        private static readonly string StartupKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
        private static readonly string StartupValue = "RPN Website Tools";
        private NotifyIcon trayIcon;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem tsmItemAddNewPost = new ToolStripMenuItem();
        private ToolStripMenuItem tsmItemTextCleaner = new ToolStripMenuItem();
        private ToolStripMenuItem tsmItemOpenNewsFolder = new ToolStripMenuItem();
        private ToolStripMenuItem tsmItemStatsChecker = new ToolStripMenuItem();
        private ToolStripSeparator tsmSep = new ToolStripSeparator();
        private ToolStripMenuItem tsmItemCloseApp = new ToolStripMenuItem();


        public CustomAppContext()
        {
            
            AddRunOnStartupRegistry();

            //Initialize items         
            tsmItemAddNewPost.Text = "Add new post";
            tsmItemAddNewPost.Click += TsmItemAddNewPost_Click;

            tsmItemOpenNewsFolder.Text = "Open news folder";
            tsmItemOpenNewsFolder.Click += TsmItemOpenNewsFolder_Click;

            tsmItemTextCleaner.Text = "Remove diacritics";
            tsmItemTextCleaner.Click += TsmItemTextCleaner_Click;

            tsmItemStatsChecker.Text = "Social media stats";
            tsmItemStatsChecker.Click += TsmItemStatsChecker_Click;

            tsmItemCloseApp.Text = "Quit";
            tsmItemCloseApp.Click += TsmItemCloseApp_Click;

            //Initialize context strips
            contextMenuStrip = new ContextMenuStrip()
            {
                DropShadowEnabled = false,
                ShowCheckMargin = true,
                ShowImageMargin = false,
                Size = new Size(265, 170)
            };

            contextMenuStrip.Items.AddRange(new ToolStripItem[]
            {
                tsmItemAddNewPost,
                tsmItemOpenNewsFolder,
                tsmItemTextCleaner,
                tsmItemStatsChecker,
                tsmSep,
                tsmItemCloseApp
            });

            //set first item to bold - default double click
            this.contextMenuStrip.Items[1].Font = new Font(this.contextMenuStrip.Items[1].Font, FontStyle.Bold);                                 
            this.contextMenuStrip.BackColor = Color.Transparent;
            this.contextMenuStrip.Renderer = new CustomRenderer();

            //Initialize tray
            trayIcon = new NotifyIcon()
            {
                ContextMenuStrip = contextMenuStrip,
                Icon = Resources.rwt,
                Visible = true,
                Text = "RPN Website Tools"
            };
            trayIcon.DoubleClick += TrayIcon_DoubleClick;

            //new Form1().Show();
        }

        private void TsmItemTextCleaner_Click(object sender, EventArgs e)
        {
            var frm = new TextConvert();

            if (!CheckOpened("TextConvert"))
                frm.Show();
        }

        private bool CheckOpened(string name)
        {
            FormCollection fc = Application.OpenForms;

            foreach (Form frm in fc)
            {
                if (frm.Text == name)
                {
                    return true;
                }
            }
            return false;
        }

        private void TsmItemStatsChecker_Click(object sender, EventArgs e)
        {
            var frm = new Form1();

            if (!CheckOpened("Form1"))
                frm.Show();
        }

        private void TrayIcon_DoubleClick(object sender, EventArgs e)
        {
            Process.Start(@"D:\OneDrive\Work (CNN PH)\C.RPN Website\News");
            //Process.Start("http://rpnradio.com/wp-admin/post-new.php");
        }

        private void TsmItemCloseApp_Click(object sender, EventArgs e)
        {
            //Hide tray icon, otherwise it will remain shown until user mouse hovers it
            trayIcon.Visible = false;
            //Close app
            Application.Exit();
        }

        private void TsmItemOpenNewsFolder_Click(object sender, EventArgs e)
        {
            Process.Start(@"D:\OneDrive\Work (CNN PH)\C.RPN Website\News");
        }

        private void TsmItemAddNewPost_Click(object sender, EventArgs e)
        {
            Process.Start("http://rpnradio.com/wp-admin/post-new.php");
        }

        private void AddRunOnStartupRegistry()
        {
            //Set the application to run at startup
            var key = Registry.CurrentUser.OpenSubKey(StartupKey, true);
            key.SetValue(StartupValue, Application.ExecutablePath.ToString());

            Debug.WriteLine("Added to Startup");
        }
    }
}
