using Microsoft.Toolkit.Uwp.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
    public sealed partial class GetItems : Page
    {
        List<string> _items = new List<string>();
        RoamingObjectStorageHelper helper = new RoamingObjectStorageHelper();

        public GetItems()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            tb_code.Text = await File.ReadAllTextAsync(@"codes.txt");
        }
      
        private async void btn_start_Click(object sender, RoutedEventArgs e)
        {                     
            _items.Clear();

            string[] readTxt = await File.ReadAllLinesAsync(@"codes.txt");
            foreach (var line in readTxt)
            {
                if (line.Contains("namespace") && line.Contains(@"{"))
                {
                    _items.Add(GetBetween(line));
                }
                lv_items.ItemsSource = _items;
            }
        }

        private string GetBetween(string line)
        {
            return line.Replace("namespace", "").Replace(@"{", "").Trim();
        }
    }
}
