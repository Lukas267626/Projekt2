using System.Globalization;
using System.Text.Json;
using WeatherApp.Models;
using WeatherApp.Exceptions;

namespace WeatherApp.Services
{
    public class OpenMeteoProvider : IWeatherProvider
    {
        // HttpClient je definován jako statický pro efektivnější správu připojení
        private static readonly HttpClient _httpClient = new HttpClient();

        public async Task<WeatherData> GetWeatherAsync(string city, double lat, double lon)
        {
            // CultureInfo.InvariantCulture zajistí, že lat/lon budou mít v URL tečku místo čárky[cite: 1]
            string url = string.Create(CultureInfo.InvariantCulture, 
                $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&current_weather=true");

            try
            {
                // Asynchronní volání externího HTTP API[cite: 1]
                var response = await _httpClient.GetAsync(url);
                
                // Kontrola, zda API vrátilo úspěšný kód (např. 200 OK)[cite: 1]
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                
                // Zpracování JSON dat[cite: 1]
                using var document = JsonDocument.Parse(json);
                var root = document.RootElement;

                // Kontrola existence dat v JSONu před jejich čtením
                if (!root.TryGetProperty("current_weather", out var current))
                {
                    throw new WeatherApiException("API odpověď neobsahuje sekci 'current_weather'.");
                }

                // Mapování dat na objekt WeatherData[cite: 1]
                return new WeatherData
                {
                    City = city,
                    Temperature = current.GetProperty("temperature").GetDouble(),
                    WindSpeed = current.GetProperty("windspeed").GetDouble(),
                    FetchedAt = DateTime.Now
                };
            }
            // Zachytávání specifických výjimek a jejich transformace na WeatherApiException[cite: 1]
            catch (HttpRequestException ex)
            {
                throw new WeatherApiException($"Chyba sítě při stahování dat pro město {city}.", ex);
            }
            catch (JsonException ex)
            {
                throw new WeatherApiException($"Chyba při zpracování JSON dat pro město {city}.", ex);
            }
            // Zachycení ostatních neočekávaných chyb (pokud to už není naše WeatherApiException)[cite: 1]
            catch (Exception ex) when (ex is not WeatherApiException)
            {
                throw new WeatherApiException($"Neočekávaná chyba při získávání počasí: {city}", ex);
            }
        }
    }
}