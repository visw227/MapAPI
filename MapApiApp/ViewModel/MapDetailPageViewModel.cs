using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MapApiApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Linq;
namespace MapApiApp.ViewModel
{
    public class MapDetailPageViewModel : BaseViewModel
    {
        public MapDetailPageViewModel(INavigation navigation) : base(navigation)
        {
            AddToFavoriteCommand = new Command(async () =>
            {
                var list = await App.Database.GetItemsAsync();
                bool containsItem = list.Any(item => item.Name == MapCords.Name);
                if (!containsItem)
                {
                    await App.Database.SaveItemAsync(MapCords);
                    ErrMsg = "Location added succesfully";
                    ShowErrorMsg = true;
                }
                else
                {
                    ErrMsg = "Location already added";
                    ShowErrorMsg = true;
                }
            });
            ShowFavoriteListCommand = new Command(async () =>
             {
                 var list = await App.Database.GetItemsAsync();
                 FavoriteList = new ObservableCollection<MapCords>(list);
                 if (IsShowFavorite)
                 {
                     IsShowFavorite = false;

                 }
                 else
                 {
                     IsShowFavorite = true;
                 }
             });

            CloseErrorWindow = new Command(() =>
          {
              ShowErrorMsg = false;

          });
        }

        private ObservableCollection<MapCords> mFavoriteList;
        public ObservableCollection<MapCords> FavoriteList
        {
            get { return mFavoriteList; }
            set
            {
                if (value != null || value != mFavoriteList) mFavoriteList = value;
                OnPropertyChanged("FavoriteList");
            }
        }
        private MapCords mMapCords;
        public MapCords MapCords
        {
            get { return mMapCords; }
            set
            {
                if (value != null || value != mMapCords) mMapCords = value;
                OnPropertyChanged("MapCords");
            }
        }

        private MapCords mSelectedCord;
        public MapCords SelectedCord
        {
            get { return mSelectedCord; }
            set
            {

                mSelectedCord = value;
                var latLon = 360 / (Math.Pow(2, 18));
                map.MoveToRegion(new MapSpan(new Position(mSelectedCord.Lat, mSelectedCord.Long), latLon, latLon));
                PinMap(map, SelectedCord);
                IsShowFavorite = false;
                OnPropertyChanged("SelectedCord");
            }
        }
        private bool mIsShowFavorite;
        public bool IsShowFavorite
        {
            get { return mIsShowFavorite; }
            set
            {
                mIsShowFavorite = value;
                OnPropertyChanged("IsShowFavorite");
            }
        }
        public Map InitialiseCords(RootObject mapModel)
        {
            MapCords = new MapCords();
            MapCords.Lat = mapModel.coord.lat;
            MapCords.Long = mapModel.coord.lon;
            MapCords.Name = mapModel.name;
            return MapInitilise();
        }
        Map map = null;
        public Map MapInitilise()
        {
            map = new Map
            {
                //  IsShowingUser = true,
                HeightRequest = 100,
                WidthRequest = 960,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            var latLon = 360 / (Math.Pow(2, 18));
            map.MoveToRegion(new MapSpan(new Position(MapCords.Lat, MapCords.Long), latLon, latLon));
            PinMap(map, MapCords);

            return map;
        }
        public void PinMap(Map map, MapCords mapCord)
        {
            var position = new Position(mapCord.Lat, mapCord.Long);
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = position,
                Label = mapCord.Name
            };
            map.Pins.Add(pin);
        }
        private bool mShowErrorMsg;
        public bool ShowErrorMsg
        {
            get { return mShowErrorMsg; }
            set
            {
                mShowErrorMsg = value;
                OnPropertyChanged("ShowErrorMsg");
            }
        }
        private string mErrMsg;
        public string ErrMsg
        {
            get { return mErrMsg; }
            set
            {
                if (value != null || value != mErrMsg) mErrMsg = value;
                OnPropertyChanged("ErrMsg");
            }
        }
        private ICommand mAddToFavoriteCommand;
        public ICommand AddToFavoriteCommand
        {
            get { return mAddToFavoriteCommand; }
            set
            {
                if (value != null || value != mAddToFavoriteCommand) mAddToFavoriteCommand = value;
                OnPropertyChanged("AddToFavoriteCommand");
            }
        }
        private ICommand mShowFavoriteListCommand;
        public ICommand ShowFavoriteListCommand
        {
            get { return mShowFavoriteListCommand; }
            set
            {
                if (value != null || value != mShowFavoriteListCommand) mShowFavoriteListCommand = value;
                OnPropertyChanged("ShowFavoriteListCommand");
            }
        }
        private ICommand mCloseErrorWindow;
        public ICommand CloseErrorWindow
        {
            get { return mCloseErrorWindow; }
            set
            {
                if (value != null || value != mCloseErrorWindow) mCloseErrorWindow = value;
                OnPropertyChanged("CloseErrorWindow");
            }
        }
    }
}
