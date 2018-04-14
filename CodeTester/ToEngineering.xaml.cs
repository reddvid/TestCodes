using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class ToEngineering : Page
    {
        long myValue;
        string myString;
        decimal myResult;

        static decimal exa = 1000000000000000000.0m;
        static decimal peta = 1000000000000000.0m;
        static decimal tera = 1000000000000.0m;
        static decimal giga = 1000000000.0m;
        static decimal mega = 1000000.0m;
        static decimal kilo = 1000.0m;
        static decimal hecto = 100.0m;
        static decimal deka = 10.0m;
        static decimal unit = 1.0m;
        static decimal deci = 0.1m;
        static decimal centi = 0.01m;
        static decimal milli = 0.001m;
        static decimal micro = 0.000001m;
        static decimal nano = 0.000000001m;
        static decimal pico = 0.000000000001m;
        static decimal femto = 0.000000000000001m;
        decimal[] units = { exa, peta, tera, giga, mega, kilo, hecto, deka, unit, deci, centi, milli, micro, nano, pico, femto };
        char[] symbols = { 'E', 'P', 'T', 'G', 'M', 'k', 'h', 'D', ' ', 'd', 'c', 'm', 'u', 'n', 'p', 'f' };
        public ToEngineering()
        {
            this.InitializeComponent();
        }

        private void btn_convert_Click(object sender, RoutedEventArgs e)
        {
            bool capable = long.TryParse(tbx_input.Text, out myValue);

            if (capable)
            {
                tb_output.Text = ConvertToEngineering(myValue);
            }
        }

        private string ConvertToEngineering(long myValue)
        {
            for (int i = 0; i < units.Length - 1; i++)
            {
                myResult = myValue / units[i];

                if (Math.Round(myResult, 2) < 1000 && Math.Round(myResult, 2) > 0)
                {
                    myString = String.Empty;
                    myString += String.Format("{0} {1}",  Math.Round(myResult, 2).ToString(), symbols[i]);
                }
                else if (myResult < 0)
                {
                    myString = String.Empty;
                    myString += String.Format("{0} {1}",myResult, symbols[i]);
                }
            }

            return myString;
        }
    }
}
