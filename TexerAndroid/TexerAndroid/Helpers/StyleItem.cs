using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TexerAndroid.Helpers
{
    class StyleItem : INotifyPropertyChanged
    {
        public string StyleType { get; set; }
        public string TextResult { get; set; }

        public StyleItem(string t, string r)
        {
            StyleType = t;
            TextResult = r;
        }

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
