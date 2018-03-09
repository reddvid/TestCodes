using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppTester
{
    /// <summary>
    /// Interaction logic for RadioMobile.xaml
    /// </summary>
    public partial class RadioMobile : Page
    {
        public string folderName;
        public string launcherPath;
        public string pathString;

        public RadioMobile()
        {
            InitializeComponent();

            btn_install.IsEnabled = false;
        }

        private void cbx_drives_Loaded(object sender, RoutedEventArgs e)
        {
            // List drives
            var diskList = new List<string>();
            foreach (var drive in DriveInfo.GetDrives())
            {
                diskList.Add(String.Format("{0} - {1}GB free of {2}GB", drive.Name, ConvertToGigs(drive.TotalFreeSpace), ConvertToGigs(drive.TotalSize)));
            }

            if (diskList.Count > 0)
            {
                cbx_drives.ItemsSource = diskList;
                cbx_drives.SelectedIndex = 0;
            }
        }

        private object ConvertToGigs(long diskValue)
        {
            return (diskValue / (1024 * 1024 * 1024));
        }

        private void cbx_drives_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DriveInfo[] drives = DriveInfo.GetDrives();

                if (drives[cbx_drives.SelectedIndex] != null)
                {
                    // Specify a name for your top-level folder.
                    folderName = drives[cbx_drives.SelectedIndex].Name + @"\Radio_Mobile";
                    launcherPath = folderName + @"\rmweng.exe";
                    pathString = System.IO.Path.Combine(folderName, "Geodata");

                    // Check if launcher exists
                    if (File.Exists(launcherPath))
                    {
                        (this.Tag as MainWindow).PassData("Looks like Radio Mobile is already installed on " + launcherPath);
                        // Remove btn
                        btn_install.Visibility = Visibility.Collapsed;

                        Button runButton = new Button();
                        runButton.Content = "Launch Radio Mobile";
                        runButton.Margin = new Thickness(0, 48, 0, 0);
                        runButton.Click += RunButton_Click;
                        sp_contents.Children.Add(runButton);
                    }
                    else
                    { 
                    (this.Tag as MainWindow).PassData("Ready to create folders in drive " + drives[cbx_drives.SelectedIndex].Name);

                    btn_install.IsEnabled = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btn_createfolders_Click(object sender, RoutedEventArgs e)
        {
            string[] subFolders = new string[] { "srtm3", "srtm1", "srtmthird", "Landcover", "OpenStreetMap", "Terraserver", "Toporama" };

            DriveInfo[] drives = DriveInfo.GetDrives();
            if (drives[cbx_drives.SelectedIndex] != null)
            {
                // Specify a name for your top-level folder.
                folderName = drives[cbx_drives.SelectedIndex].Name + @"\Radio_Mobile";
                launcherPath = folderName + @"\rmweng.exe";
                pathString = System.IO.Path.Combine(folderName, "Geodata");
                System.IO.Directory.CreateDirectory(pathString);

                // Now create subfolders for Geodata
                foreach (var sf in subFolders)
                {
                    pathString = System.IO.Path.Combine(pathString, sf);
                    System.IO.Directory.CreateDirectory(pathString);
                    // Reset
                    pathString = System.IO.Path.Combine(folderName, "Geodata");
                }

                 (this.Tag as MainWindow).PassData("Folders created successfully!");
            }
        }

        private void btn_install_Click(object sender, RoutedEventArgs e)
        {
            string[] subFolders = new string[] { "srtm3", "srtm1", "srtmthird", "Landcover", "OpenStreetMap", "Terraserver", "Toporama" };

            DriveInfo[] drives = DriveInfo.GetDrives();
            if (drives[cbx_drives.SelectedIndex] != null)
            {
                // Specify a name for your top-level folder.
                folderName = drives[cbx_drives.SelectedIndex].Name + @"\Radio_Mobile";
                launcherPath = folderName + @"\rmweng.exe";
                pathString = System.IO.Path.Combine(folderName, "Geodata");

                // Check if launcher exists
                if (File.Exists(launcherPath))
                {
                    // Remove btn
                    btn_install.Visibility = Visibility.Collapsed;

                    Button runButton = new Button();
                    runButton.Content = "Launch Radio Mobile";
                    runButton.Margin = new Thickness(0, 48, 0, 0);
                    runButton.Click += RunButton_Click;
                    sp_contents.Children.Add(runButton);
                }
                else
                {
                    (this.Tag as MainWindow).PassData("Creating folders...");

                    System.IO.Directory.CreateDirectory(pathString);

                    // Now create subfolders for Geodata
                    foreach (var sf in subFolders)
                    {
                        pathString = System.IO.Path.Combine(pathString, sf);
                        System.IO.Directory.CreateDirectory(pathString);
                        // Reset
                        pathString = System.IO.Path.Combine(folderName, "Geodata");
                    }

                (this.Tag as MainWindow).PassData("Extracting files...");

                    // Extract first zip - core
                    var zipFileName = Environment.CurrentDirectory + @"\ZipFiles\rmwcore.zip";
                    var targetDir = folderName;
                    FastZip fastZip = new FastZip();
                    string fileFilter = null;

                    // Will always overwrite if target filenames already exist
                    fastZip.ExtractZip(zipFileName, targetDir, fileFilter);

                    // Extract second zip
                    zipFileName = Environment.CurrentDirectory + @"\ZipFiles\rmw1166eng.zip";
                    targetDir = folderName;

                    // Will always overwrite if target filenames already exist
                    fastZip.ExtractZip(zipFileName, targetDir, fileFilter);

                    (this.Tag as MainWindow).PassData("Files extracted successfully!");

                    // Check if launcher exists
                    if (File.Exists(launcherPath))
                    {
                        // Remove btn
                        btn_install.Visibility = Visibility.Collapsed;

                        Button runButton = new Button();
                        runButton.Content = "Launch Radio Mobile";
                        runButton.Margin = new Thickness(0, 48, 0, 0);
                        runButton.Click += RunButton_Click;
                        sp_contents.Children.Add(runButton);
                    }
                }
            }
        }

        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if file exists

            // Launch
            Process.Start(folderName + @"\rmweng.exe");
        }
    }
}
