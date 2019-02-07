using Antexer.Helpers;
using Plugin.Clipboard;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AnTexer
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

            ResultsGrid.ItemSelected += ResultsGrid_ItemSelected;

        }

        protected override void OnAppearing()
        {
            LoadTheme();
            base.OnAppearing();
        }

        private void LoadTheme()
        {
            // Enable dark
            if (Application.Current.Properties.ContainsKey("theme"))
            {
                var isDark = (bool)Application.Current.Properties["theme"];

                Debug.WriteLine("Saved theme " + isDark);

                ThemeTggle.IsToggled = isDark;
            }
            else
            {
                ThemeTggle.IsToggled = true;
            }
        }

        private void ResultsGrid_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as StyleItem;
            Debug.WriteLine(item);
            if (item == null)
                return;

            // Copy Text
            CrossClipboard.Current.SetText(item.TextResult);

            // Show Toast
            DependencyService.Get<MsgToast>().ShortAlert("Copied " + item.StyleType);

            ResultsGrid.SelectedItem = null;
        }
        private void ThemeTggle_Toggled(object sender, ToggledEventArgs e)
        {
            Debug.WriteLine("Toggled " + ThemeTggle.IsToggled);

            if (ThemeTggle.IsToggled == true)
            {
                Debug.WriteLine("Dark mode");
                // Dark mode
                LblSwitch.TextColor = Color.FromRgba(255, 255, 255, 255);
                InputBox.BackgroundColor = Color.FromRgba(39, 39, 39, 255);
                InputBox.TextColor = Color.FromRgba(255, 255, 255, 255);
                InputBox.PlaceholderColor = Color.FromRgba(199, 199, 199, 255);
                RootLayout.BackgroundColor = Color.FromRgba(23, 23, 23, 255);

                ResultsGrid.ItemTemplate = (DataTemplate)Resources["DarkItem"];
            }
            else
            {
                Debug.WriteLine("Light mode");
                // Light mode
                LblSwitch.TextColor = Color.FromRgba(10, 10, 10, 255);
                InputBox.BackgroundColor = Color.FromRgba(238, 238, 238, 255);
                InputBox.TextColor = Color.FromRgba(5, 5, 5, 255);
                InputBox.PlaceholderColor = Color.FromRgba(39, 39, 39, 255);
                RootLayout.BackgroundColor = Color.FromRgba(255, 255, 255, 255);

                ResultsGrid.ItemTemplate = (DataTemplate)Resources["LightItem"];
            }

            // Save setting
            Application.Current.Properties["theme"] = ThemeTggle.IsToggled;
            Debug.WriteLine(RootLayout.BackgroundColor);
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

            ResultsGrid.IsVisible = !string.IsNullOrEmpty(InputBox.Text) ? true : false;
        }

    }
}
