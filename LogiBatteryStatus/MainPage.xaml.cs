using Microsoft.Toolkit.Uwp.Connectivity;
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

namespace LogiBatteryStatus
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        BluetoothLEHelper bluetoothLEHelper = BluetoothLEHelper.Context;
        DispatcherTimer dispatcherTimer = new DispatcherTimer();

        public MainPage()
        {
            this.InitializeComponent();

            bluetoothLEHelper.EnumerationCompleted += BluetoothLEHelper_EnumerationCompleted;

            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Tick += DispatcherTimer_Tick;

        }

        private void DispatcherTimer_Tick(object sender, object e)
        {
            EnumerateDevices();

            dispatcherTimer.Stop();
        }

        private async void BluetoothLEHelper_EnumerationCompleted(object sender, EventArgs e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                bluetoothLEHelper.StopEnumeration();
            });
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // dispatcherTimer.Start();

            base.OnNavigatedTo(e);
        }

        private async void EnumerateDevices()
        {
            bluetoothLEHelper.StartEnumeration();

            foreach (var d in bluetoothLEHelper.BluetoothLeDevices)
            {
                batteryStatus.Text += d.Name;

                if (d.Name.ToString() == "M585/M590")
                {
                    batteryStatus.Text += d.Name;

                    bluetoothLEHelper.StopEnumeration();

                    await d.ConnectAsync();

                    var services = d.Services;

                    foreach (var s in services)
                    {
                        batteryStatus.Text += s.Name;
                    }

                    break;
                }
            }
        }

        private void TextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
           
        }

        private void StackPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!bluetoothLEHelper.IsEnumerating)
            {
                EnumerateDevices();
            }
            else
            {
                bluetoothLEHelper.StopEnumeration();
            }
        }
    }
}
