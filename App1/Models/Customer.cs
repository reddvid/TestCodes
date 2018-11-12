using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace App1.Models
{
    public class Customer : INotifyPropertyChanged
    {
        private string _firstName;
        private string _lastName;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string member = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(member));
        }

        public string FirstName
        {
            get
            {
                return this._firstName;
            }
            set
            {
                if (value != _firstName)
                {
                    this._firstName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string LastName
        {
            get
            {
                return this._lastName;
            }
            set
            {
                if (value != _lastName)
                {
                    this._lastName = value;
                    OnPropertyChanged();
                }
            }
        }


    }
}
