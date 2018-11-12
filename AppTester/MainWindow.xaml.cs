using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppTester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void lv_applist_Loaded(object sender, RoutedEventArgs e)
        {
            lv_applist.SelectedIndex = 0;
        }

        private void lv_applist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            fr_content.Navigate(new System.Uri("RadioMobile.xaml",
             UriKind.RelativeOrAbsolute));
        }

        private void fr_content_LoadCompleted(object sender, NavigationEventArgs e)
        {
            (e.Content as RadioMobile).Tag = this;
        }

        public void PassData(string data)
        {
            tb_status.Text = data;
        }

        public void MultipleCursorInVisualStudio()
        {
            string sampleStringIAddedThis = "Sample stringI also added this!";
            string ulolTestsIAddedThis = "Dragon Ball";
            string viceGandaIAddedThis = "I also added this!";

            string jakePeralta = "Noice!";
        }
    }
}
