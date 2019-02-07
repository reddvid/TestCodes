using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexerAndroid.Helpers;
using Xamarin.Forms;

namespace TexerAndroid
{
    public partial class MainPage : ContentPage
    {
        ObservableCollection<StyleItem> results = null;
        char[] alpha = null;
        string[,] charArray = null;

        public MainPage()
        {
            InitializeComponent();

            alpha = new Styles().alpha;
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

            foreach (char c in InputBox.Text)
            {
                int alphaIndex = Array.IndexOf(alpha, c);

                Debug.WriteLine(alphaIndex);

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

            ResultsGrid.ItemsSource = results;
        }
    }
}
