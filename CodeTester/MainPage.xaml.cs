using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp.Helpers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Shapes;

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

            nvMain.Header = "Test";
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

                case "web":
                    Frame.Navigate(typeof(WebViewer), item.Tag);
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
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
            foreach (NavigationViewItemBase item in (sender as NavigationView).MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "gui")
                {
                    (sender as NavigationView).SelectedItem = item;
                    break;
                }
            }
        }

        private async void AddItem()
        {
            NavigationViewItem item = new NavigationViewItem()
            {
                Content = "Hello",
                Tag = "hello"
            };
            nvMain.MenuItems.Add(item);
            await Task.Delay(500);
            SelectAddedItem(item);
        }

        private void SelectAddedItem(NavigationViewItem item)
        {
            foreach (NavigationViewItemBase i in nvMain.MenuItems)
            {
                if (i is NavigationViewItem && i.Tag.ToString() == item.Tag.ToString())
                {
                    nvMain.SelectedItem = i;
                    break;
                }
            }
        }

        private void nvMain_DoubleTapped(object sender, Windows.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
        {
            AddItem();
        }

        private void Rectangle_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var id = (sender as Rectangle).Tag as string;
            Frame.Navigate(typeof(WebViewer), id);
        }
    }
}
