using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Texer.UWP.Helpers;
using Windows.ApplicationModel.DataTransfer;
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

namespace Texer.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        char[] alpha = null;
        string[] alphaShit = null;

        string[,] charArray = null;

        ObservableCollection<StyleItem> results = null;

        public MainPage()
        {
            this.InitializeComponent();

            alpha = new Styles().alpha;
            alphaShit = new Styles().alphaShit;
            charArray = new Styles().charArray;

            results = new ObservableCollection<StyleItem>();
        }

        private void InputBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            results.Clear();

            string bold = "";
            string italic = "";
            string em = "";
            string score = "";
            string script = "";
            string super = "";
            string small = "";
            string dark = "";
            string circle = "";
            string thin = "";
            string utf = "";

            foreach (char c in InputBox.Text)
            {
                int alphaIndex = Array.IndexOf(alpha, c);

                Debug.WriteLine(alphaIndex);
                Debug.WriteLine(c);

                if (alphaIndex != -1)
                {
                    bold += charArray[alphaIndex, 0];
                    italic += charArray[alphaIndex, 1];
                    em += charArray[alphaIndex, 2];
                    score += charArray[alphaIndex, 3];
                    script += charArray[alphaIndex, 4];
                    super += charArray[alphaIndex, 5];
                    small += charArray[alphaIndex, 6];
                    dark += charArray[alphaIndex, 7];
                    circle += charArray[alphaIndex, 8];
                    thin += charArray[alphaIndex, 9];
                    //utf += Convert(c.ToString());
                }
                else if (Array.IndexOf(alphaShit, c) > -1)
                {
                    int shitIndex = Array.IndexOf(alphaShit, c);
                    utf += alpha[shitIndex];
                }
                else
                {
                    bold += c;
                    italic += c;
                    em += c;
                    score += c;
                    script += c;
                    super += c;
                    small += c;
                    dark += c;
                    circle += c;
                    thin += c;
                    //utf += Convert(c.ToString());
                }
            }

            results.Add(new StyleItem("Bold", bold));
            results.Add(new StyleItem("Italic", italic));
            results.Add(new StyleItem("Emphasized", em));
            results.Add(new StyleItem("Dark Bubble", dark));
            results.Add(new StyleItem("Bubble", circle));
            results.Add(new StyleItem("Thin", thin));
            results.Add(new StyleItem("Scored", score));
            results.Add(new StyleItem("Script", script));
            results.Add(new StyleItem("Tiny", super));
            results.Add(new StyleItem("Small Caps", small));
            results.Add(new StyleItem("UTF-8", utf));
            ResultsGrid.ItemsSource = results;
        }

        private string Convert(string c)
        {
            string accentedStr;
            byte[] tempBytes;
            tempBytes = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(c);
            return System.Text.Encoding.ASCII.GetString(tempBytes);
        }

        private void BtnCopy_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as Button).DataContext as StyleItem;

            var dataPackage = new DataPackage();
            dataPackage.SetText(item.TextResult);
            Debug.WriteLine(item.TextResult);

            Clipboard.SetContent(dataPackage);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
#if DEBUG
            InputBox.Text = "Debugging...";
#endif
        }
    }
}
