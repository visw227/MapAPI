using System;
using System.Threading.Tasks;
using System.Windows.Input;
using MapApiApp.Model;
using MapApiApp.Services;
using MapApiApp.Views;
using Xamarin.Forms;

namespace MapApiApp.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {
        MapService mapService = null;

        public MainPageViewModel(INavigation navigation) : base(navigation)
        {
            GetInfoCommand = new Command(async () =>
            {
                await GetWeatherApiCall(ZipCode);
            });
            NavigateMapPage = new Command(async () =>
            {
                await _navigation.PushAsync(new NavigationPage(new MapDetailPage(this.MainModel)));

            });
        }

        public async Task GetWeatherApiCall(string zipCode)
        {
            try
            {
                IsBusy = true;
                mapService = new MapService();
                MainModel = await mapService.GetMapDetails(zipCode);
                IsShowData = true;
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                var errormsg = ex.Message;
                IsErrorMsg = true;
                IsShowData = false;
            }
            finally
            {
                IsBusy = false;
            }
        }

        #region Properties
        private RootObject mMainModel;
        public RootObject MainModel
        {
            get { return mMainModel; }
            set
            {
                if (value != null || value != mMainModel) mMainModel = value;
                Icon = "http://openweathermap.org/img/w/" + mMainModel.weather[0].icon + ".png";
                OnPropertyChanged("MainModel");
            }
        }
        private string mIcon;
        public string Icon
        {
            get { return mIcon; }
            set
            {
                if (value != null || value != mIcon) mIcon = value;
                OnPropertyChanged("Icon");
            }
        }
        private bool mIsBusy;
        public bool IsBusy
        {
            get { return mIsBusy; }
            set
            {
                mIsBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }
        private string mZipCode;
        public string ZipCode
        {
            get { return mZipCode; }
            set
            {
                mZipCode = value;
                IsErrorMsg = false;
                //if (mZipCode.Length > 4)
                //{
                //    Task.Run(async () =>
                //    {
                //        await GetWeatherApiCall(mZipCode);
                //    });
                //}

                OnPropertyChanged("ZipCode");
            }
        }
        private bool mIsShowData;
        public bool IsShowData
        {
            get { return mIsShowData; }
            set
            {
                mIsShowData = value;
                OnPropertyChanged("IsShowData");
            }
        }
        private bool mIsErrorMsg;
        public bool IsErrorMsg
        {
            get { return mIsErrorMsg; }
            set
            {
                mIsErrorMsg = value;
                OnPropertyChanged("IsErrorMsg");
            }
        }
        private ICommand mGetInfoCommand;
        public ICommand GetInfoCommand
        {
            get
            {
                return mGetInfoCommand;
            }
            set
            {
                if (value != null || value != mGetInfoCommand) mGetInfoCommand = value;
                OnPropertyChanged("GetInfoCommand");
            }
        }
        private ICommand mNavigateMapPage;
        public ICommand NavigateMapPage
        {
            get { return mNavigateMapPage; }
            set
            {
                if (value != null || value != mNavigateMapPage) mNavigateMapPage = value;
                OnPropertyChanged("NavigateMapPage");
            }
        }
        #endregion
    }
}
