using System;
using System.Collections.Generic;
using MapApiApp.Model;
using Xamarin.Forms;

namespace MapApiApp.Views
{
    public partial class MapDetailPage : ContentPage
    {
        public MapDetailPage()
        {
            InitializeComponent();
        }
        public MapDetailPage(RootObject mapModel)
        {
            InitializeComponent();
            var vm = new ViewModel.MapDetailPageViewModel(this.Navigation);
            this.BindingContext = vm;
            
            var map = vm.InitialiseCords(mapModel);
            this.MapPanel.Children.Add(map);
        }
    }
}
