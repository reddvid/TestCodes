using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace AppTester
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void cbx_drives_Loaded(object sender, RoutedEventArgs e)
        {
            // List drives
            var diskList = new List<string>();
            foreach (var drive in DriveInfo.GetDrives())
            {
                diskList.Add(String.Format("{0} - {1}/{2}", drive.Name, drive.TotalFreeSpace, drive.TotalSize));
            }
            cbx_drives.ItemsSource = diskList;
        }
    }
}
