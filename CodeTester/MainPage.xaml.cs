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
            var item = args.SelectedItem as NavigationViewItem;

            switch (item.Tag)
            {
                case "gui":
                    fr_view.Navigate(typeof(GUI));
                    break;

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

                case "tiles":
                    fr_view.Navigate(typeof(TilePinner));
                    break;

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        private void hl_advanced_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AdvancedBrightness));
        }
          
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void NavigationView_Loaded(object sender, RoutedEventArgs e)
        {
            // set the initial SelectedItem 
            foreach (NavigationViewItem item in (sender as NavigationView).MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "gui")
                {
                    (sender as NavigationView).SelectedItem = item;
                    break;
                }
            }
        }
    }
}
