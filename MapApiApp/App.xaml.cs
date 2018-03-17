using MapApiApp.Data;
using MapApiApp.Views;
using Xamarin.Forms;

namespace MapApiApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = GetMainPage();
        }
        public static Page GetMainPage()
        {
            return new NavigationPage(new MainPage());
        }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        static MapData database;
        public static MapData Database
        {
            get
            {
                if (database == null)
                {
                    database = new MapData(DependencyService.Get<IFileHelper>().GetLocalFilePath("MapCords.db3"));
                }
                return database;
            }
        }
    }
}
