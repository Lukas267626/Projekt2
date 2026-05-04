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

        /// <summary>
        /// Asynchronně získá data pro dvě města a provede jejich porovnání[cite: 1].
        /// </summary>
        public async Task<string> CompareCitiesAsync(string city1, string city2)
        {
            // Spuštění dvou nezávislých asynchronních požadavků paralelně[cite: 1]
            // Pro účely projektu používáme fixní souřadnice, ale s názvy od uživatele
            var task1 = _provider.GetWeatherAsync(city1, 50.08, 14.43); // Simulace souřadnic 1
            var task2 = _provider.GetWeatherAsync(city2, 49.19, 16.60); // Simulace souřadnic 2

            // Počkáme na dokončení obou úloh naráz pomocí Task.WhenAll[cite: 1]
            var results = await Task.WhenAll(task1, task2);

            var w1 = results[0];
            var w2 = results[1];

            // Výpočet rozdílu teplot (zpracování dat vyžadované zadáním)[cite: 1]
            double tempDiff = Math.Round(Math.Abs(w1.Temperature - w2.Temperature), 2);
            
            // Sestavení přehledného výstupu[cite: 1]
            return "------------------------------------------\n" +
                   "VÝSLEDEK ANALÝZY DAT\n" +
                   "------------------------------------------\n" +
                   $"{w1.City}: {w1.Temperature}°C (Vítr: {w1.WindSpeed} km/h)\n" +
                   $"{w2.City}: {w2.Temperature}°C (Vítr: {w2.WindSpeed} km/h)\n" +
                   "------------------------------------------\n" +
                   $"Absolutní rozdíl teplot mezi městy je {tempDiff}°C.\n" +
                   "------------------------------------------";
        }
    }
}