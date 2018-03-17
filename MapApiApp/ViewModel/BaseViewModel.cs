using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace MapApiApp.ViewModel
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public INavigation _navigation;
        public BaseViewModel(INavigation navigation)
        {
            _navigation = navigation; // AND HERE
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
