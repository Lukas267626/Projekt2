using WeatherApp.Services;

Console.WriteLine("=== WEATHER DASHBOARD 2026 ===");

// 1. Inicializace (vytvoření objektů)
IWeatherProvider provider = new OpenMeteoProvider();
WeatherAggregator aggregator = new WeatherAggregator(provider);

try
{
    Console.WriteLine("Stahuji a analyzuji data z internetu...");
    
    // 2. Volání asynchronní logiky
    string report = await aggregator.CompareCitiesAsync();
    
    // 3. Výpis výsledku
    Console.WriteLine(report);
}
catch (Exception ex)
{
    // Základní ošetření chyb, aby aplikace nespadla[cite: 1]
    Console.WriteLine($"Nastala chyba: {ex.Message}");
    if (ex.InnerException != null)
    {
        Console.WriteLine($"Detail: {ex.InnerException.Message}");
    }
}

Console.WriteLine("\nStiskněte libovolnou klávesu pro ukončení...");
Console.ReadKey();