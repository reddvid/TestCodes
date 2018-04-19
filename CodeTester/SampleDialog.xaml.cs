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

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CodeTester
{
    public sealed partial class SampleDialog : ContentDialog
    {
        bool isEscape;

        public SampleDialog()
        {
            isEscape = true;
            this.InitializeComponent();
            this.Closing += SampleDialog_Closing;
        }

        private void SampleDialog_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            if (isEscape)
                args.Cancel = true;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.Title = "Primary button clicked";
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            isEscape = false;
            this.Hide();
        }
    }
}
