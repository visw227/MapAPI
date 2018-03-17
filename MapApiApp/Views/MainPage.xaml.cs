using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MapApiApp.Views
{
    public partial class MainPage : ContentPage
    {
        
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = new ViewModel.MainPageViewModel(this.Navigation);
        }
        
    }
}
