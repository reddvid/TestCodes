using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPNWebsiteTools
{
    public partial class TextConvert : Form
    {
        public TextConvert()
        {
            InitializeComponent();

            //Navigate to website
            webView1.Navigate("http://www.unit-conversion.info/texttools/remove-letter-accents/");
        }
    }
}
