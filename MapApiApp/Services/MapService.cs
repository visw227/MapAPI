using System;
using MapApiApp.Helper;
using System.Threading.Tasks;
using System.Globalization;

namespace MapApiApp.Services
{
    public class MapService
    {
        OpenWeatherMap<Model.RootObject> restApiCall = new OpenWeatherMap<Model.RootObject>();
        public async Task<Model.RootObject> GetMapDetails(string zip)
        {
            var deviceCountry = GetCurrentCountry();
            var getData = await restApiCall.GetAllWeathers(zip, "us");
            return getData;
        }
        public string GetCurrentCountry()
        {
            return CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
        }
    }
}
