using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.StartScreen;
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
    public sealed partial class TilePinner : Page
    {
        static string logoSecondaryTileId;
        public static string dynamicTileId = "SecondaryTile.LiveTile";
        public static string appbarTileId = "SecondaryTile.AppBar";

        public TilePinner()
        {
            this.InitializeComponent();
        }

        private void btnVisualStudio_Click(object sender, RoutedEventArgs e)
        {
            string imgName = "StoreLogo";
            string tileId = "VisualStudio";
            string tileName = (sender as Button).Content as string;
            PinTile(imgName, tileId, tileName);
        }

        private async void PinTile(string imgName, string tileId, string tileName)
        {
            Uri logo = new Uri("ms-appx:///Assets/" + imgName + ".png");
            Uri smallLogo = new Uri("ms-appx:///Assets/" + imgName + ".png");

            // During creation of secondary tile, an application may set additional arguments on the tile that will be passed in during activation.
            // These arguments should be meaningful to the application. In this sample, we'll pass in the date and time the secondary tile was pinned.
            // Create a 1x1 Secondary tile
            logoSecondaryTileId = "myStringID";
            SecondaryTile s = new SecondaryTile(logoSecondaryTileId, tileName, tileName, "textArgs", TileOptions.ShowNameOnLogo, logo);
            s.DisplayName = tileName;
            // Specify a foreground text value.
            s.ForegroundText = ForegroundText.Light;

            await s.RequestCreateAsync();
        }
    }
}
