using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MapApiApp.Helper
{
    public class OpenWeatherMap<T>
    {
        public OpenWeatherMap()
        {

        }
        HttpClient _httpClient = new HttpClient();
        public async Task<T> GetAllWeathers(string zip, string countryCode)
        {
            string url = MainUtils.OpenWeatherApi + "zip=" + zip + "," + countryCode + "&APPID=" + MainUtils.Key;
            var json = await _httpClient.GetStringAsync(url);

            var getWeatherModels = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
            return getWeatherModels;
        }
    }
}
