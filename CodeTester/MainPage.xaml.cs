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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var frame = Window.Current.Content as Frame;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = frame.CanGoBack ?
                          AppViewBackButtonVisibility.Visible :
                          AppViewBackButtonVisibility.Collapsed;

            if (helper.KeyExists("radio"))
            {
                switch (helper.Read<string>("radio"))
                {
                    case "default":
                        tb_notice.Visibility = Visibility.Collapsed;
                        slder_def.Opacity = 1.0;
                        slder_def.IsHitTestVisible = true;
                        break;

                    case "custom":
                        tb_notice.Visibility = Visibility.Visible;
                        slder_def.Opacity = 0.5;
                        slder_def.IsHitTestVisible = false;
                        break;
                }
            }
            else
            {
                tb_notice.Visibility = Visibility.Collapsed;
                slder_def.Opacity = 1.0;
                slder_def.IsHitTestVisible = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var cplPath = System.IO.Path.Combine(Environment.SystemDirectory, "control.exe");
            System.Diagnostics.Process.Start(cplPath, "/name Microsoft.ProgramsAndFeatures");
        }

        private void hl_advanced_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AdvancedBrightness));
        }
    }
}
