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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPXAMLControls
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

        private void MyScroll_ViewChanging(object sender, ScrollViewerViewChangingEventArgs e)
        {
            ScrollValue.Text = "Vertical Offset: " + e.FinalView.VerticalOffset.ToString() + "\nHorizontal Offset: " + e.FinalView.HorizontalOffset.ToString() + "\nVerticalHeight: " + MyScroll.ScrollableHeight;

            double vOffset = e.FinalView.VerticalOffset;
            double scrollHeight = MyScroll.ScrollableHeight;

            // If 0 yung vOffset dapat max yung shape
            if (vOffset == 0)
            {
                MyShape.Height = MyShape.Width = 200;
            }
            else if (((scrollHeight - vOffset) / scrollHeight) >= 0.4)
            {
                MyShape.Height = MyShape.Width = ((scrollHeight - vOffset) / scrollHeight) * 200;
            }
            // Kapag umabot sa 40 percent yung scroll, unti-unting liliit upto 50%
        }
    }
}
