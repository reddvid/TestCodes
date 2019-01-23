using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Windows.System;

namespace AppLauncher.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Apps> appList = new List<Apps>();

        public MainWindow()
        {
            InitializeComponent();

            //Load AppList
            LoadAppList();
        }

        private async void LoadAppList()
        {
            //appList.Add(new Apps("Microsoft To-Do", "ms-todo", await CheckUri("ms-todo").Result));
            //appList.Add(new Apps("Evernote", "evernote", CheckUri("evernote").Result));
            //lvApps.ItemsSource = appList;
        }

        private async void TbTest_Loaded(object sender, RoutedEventArgs e)
        {
            var isUriFound = await Launcher.QueryUriSupportAsync(new Uri("evernote:"), LaunchQuerySupportType.Uri);

            if (isUriFound == LaunchQuerySupportStatus.Available)
            {
                tbTest.Text = "Found";
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }

    public class Apps
    {
        public string AppName { get; set; }
        public Uri UriScheme { get; set; }
        public string BtnContent { get; set; }

        public Apps(string name, string scheme, string content)
        {
            scheme = scheme + ":";
            Debug.WriteLine(scheme);

            AppName = name;
            UriScheme = new Uri(scheme);
            BtnContent = content;
        }

    }
}
