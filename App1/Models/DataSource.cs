using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Models
{
    public class DataSource
    {
        private List<String> _myList = new List<String>(); 

        public List<String> SampleList
        {
            get
            {
                _myList.Add("Hello");
                _myList.Add("There");
                _myList.Add("You");
                _myList.Add("Are");
                _myList.Add("Beautiful");
                return _myList;
            }
            set
            {
                _myList = value;
            }
        }
    }
}
