using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Html;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LBCExTrackerApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void TrackBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string receivedText;
                var client = new HttpClient();
                var response = await client.GetAsync("https://www.lbcexpress.com/track/?tracking_no=" + TrackingNumber.Text);
                var responseContent = response.Content;

                using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
                {
                    receivedText = await reader.ReadToEndAsync();
                }

                var s = HtmlUtilities.ConvertToText(receivedText);

                ResultTxt.Text = s;

                client.Dispose();

            }
            catch (Exception ex)
            {

            }
        }
    }
}
