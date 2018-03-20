using Microsoft.Toolkit.Uwp.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CodeTester
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        RoamingObjectStorageHelper helper = new RoamingObjectStorageHelper();

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
           // fr_content.Navigate(typeof(RadioMobile));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var cplPath = System.IO.Path.Combine(Environment.SystemDirectory, "control.exe");
            System.Diagnostics.Process.Start(cplPath, "/name Microsoft.ProgramsAndFeatures");
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);           
        }
       
        private void hl_advanced_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AdvancedBrightness));
        }

        private void lv_items_Loaded(object sender, RoutedEventArgs e)
        {
            ListView listView = sender as ListView;

            if (listView.Items.Count > 0)
            {
                listView.SelectedIndex = 0;
            }
        }

        private void lv_items_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            var item = listView.SelectedItem as ListViewItem;

            switch (item.Tag)
            {
                case "display":
                    fr_view.Navigate(typeof(AdvancedDisplay));
                    break;

                case "sdk":
                    fr_view.Navigate(typeof(GetItems));
                    break;

                case "smithchart":
                    fr_view.Navigate(typeof(SmithChartTemplate));
                    break;

                case "convert":
                    fr_view.Navigate(typeof(ToEngineering));
                    break;

            }
        }
    }
}
