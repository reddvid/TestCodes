using Microsoft.Toolkit.Uwp.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CodeTester
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AdvancedBrightness : Page
    {
        RoamingObjectStorageHelper helper = new RoamingObjectStorageHelper();

        public AdvancedBrightness()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var frame = Window.Current.Content as Frame;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = frame.CanGoBack ?
                          AppViewBackButtonVisibility.Visible :
                          AppViewBackButtonVisibility.Collapsed;

        }

        private void cb_custom_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as ComboBox).SelectedIndex = 2;
        }

        private void cb_custom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                sp_sliders.Children.Clear();
            }
            catch
            {

            }

            helper.Save("levels", (sender as ComboBox).SelectedIndex);

            int x = 0;
            int y = 0;
            int level = 0;
            
            if (helper.Read<double[]>("sliderValues") != null)
            {
                do
                {
                    y++;
                    Slider s = new Slider();
                    s.Header = "Level " + y;
                    s.Maximum = 100;
                    s.MinHeight = 0;
                    s.Value = 10;
                    sp_sliders.Children.Add(s);
                    x++;
                }
                while (x < (sender as ComboBox).SelectedIndex + 3);
            }
            else
            {
                do
                {
                    y++;
                    Slider s = new Slider();
                    s.Header = "Level " + y;
                    s.Maximum = 100;
                    s.MinHeight = 0;
                    s.Value = level;
                    sp_sliders.Children.Add(s);
                    level = level + 15;
                    x++;
                }
                while (x < (sender as ComboBox).SelectedIndex + 3);
            }
        }

        private void rb_default_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;

            if (rb != null && g_custom != null)
            {
                string tag = rb.Tag.ToString();
                switch (tag)
                {
                    case "default":
                        g_custom.Opacity = 0.5;
                        g_custom.IsHitTestVisible = false;
                        helper.Save("radio", "default");
                        break;

                    case "custom":
                        g_custom.Opacity = 1.0;
                        g_custom.IsHitTestVisible = true;
                        helper.Save("radio", "custom");
                        break;
                }
            }
        }

        private void g_custom_Loaded(object sender, RoutedEventArgs e)
        {
            if (helper.KeyExists("radio"))
            {
                switch (helper.Read<string>("radio"))
                {
                    case "default":
                        rb_default.IsChecked = true;
                        break;

                    case "custom":
                        rb_custom.IsChecked = true;
                        break;
                }
            }
            else
            {
                rb_default.IsChecked = true;
            }
        }

    }
}
