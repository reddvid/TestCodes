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
        List<string> _items2 = new List<string>();
        RoamingObjectStorageHelper helper = new RoamingObjectStorageHelper();

        public GetItems()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }
      
        private async void btn_start_Click(object sender, RoutedEventArgs e)
        {                     
            _items.Clear();
            _items2.Clear();

            string[] readTxt = File.ReadAllLines(@"APICodes/17120.txt");
            foreach (var line in readTxt)
            {
                if (line.Contains("namespace") && line.Contains(@"{"))
                {
                    _items.Add(GetBetween(line));
                }
                lv_items.ItemsSource = _items;
                lv_items.Header = "17120 " + _items.Count;
            }

            string[] readTxt2 = File.ReadAllLines(@"APICodes/17125.txt");
            foreach (var line in readTxt)
            {
                if (line.Contains("namespace") && line.Contains(@"{"))
                {
                    _items2.Add(GetBetween(line));
                }
                lv_items2.ItemsSource = _items2;
                lv_items2.Header = "17125 " + _items2.Count;
            }
        }

        private string GetBetween(string line)
        {
            return line.Replace("namespace", "").Replace(@"{", "").Trim();
        }
    }
}
