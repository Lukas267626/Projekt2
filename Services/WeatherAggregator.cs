using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class WeatherAggregator
    {
        private readonly IWeatherProvider _provider;

        public WeatherAggregator(IWeatherProvider provider)
        {
            _provider = provider;
        }

        public async Task<string> CompareCitiesAsync()
        {
            // Spustíme dvě asynchronní operace naráz
            // Praha (50.08, 14.43) a Brno (49.19, 16.60)
            var prahaTask = _provider.GetWeatherAsync("Praha", 50.08, 14.43);
            var brnoTask = _provider.GetWeatherAsync("Brno", 49.19, 16.60);

            // Počkáme, až se dokončí obě
            var results = await Task.WhenAll(prahaTask, brnoTask);

            var p = results[0];
            var b = results[1];

            double diff = Math.Round(Math.Abs(p.Temperature - b.Temperature), 2);
            
            return $"--- Výsledek porovnání ---\n" +
                   $"{p.City}: {p.Temperature}°C (vítr {p.WindSpeed} km/h)\n" +
                   $"{b.City}: {b.Temperature}°C (vítr {b.WindSpeed} km/h)\n" +
                   $"Rozdíl teplot je {diff}°C.";
        }
    }
}