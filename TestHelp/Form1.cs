using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestHelp
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            People = new List<Person>()
            {
                
            };

        }

        private List<Person> People { get; }

        class Person
        {
            public string Name { get; set; }
            public int Id { get; set; }
        }
    }
}
