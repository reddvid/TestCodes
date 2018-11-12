using App1.ViewModel;
using App1.Models;
using Windows.UI.Xaml.Controls;
using System;
using System.Collections.Generic;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public List<String> sampleList = new List<String>();


        public MainPage()
        {
            this.InitializeComponent();

            this.DataContext = new MainPageViewModel();

            sampleList = new DataSource().SampleList;
        }
    }
}
