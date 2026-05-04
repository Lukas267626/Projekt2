using WeatherApp.Models;

namespace WeatherApp.Services
{
    public interface IWeatherProvider
    {
        // Task značí, že operace bude probíhat asynchronně
        Task<WeatherData> GetWeatherAsync(string city, double lat, double lon);
    }
}