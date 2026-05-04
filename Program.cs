using WeatherApp.Services;
using WeatherApp.Exceptions;

Console.WriteLine("=== WEATHER DASHBOARD 2026 ===");

// Inicializace služeb
IWeatherProvider provider = new OpenMeteoProvider();
WeatherAggregator aggregator = new WeatherAggregator(provider);

bool pokracovat = true;

while (pokracovat)
{
    try
    {
        Console.WriteLine("\n--- NOVÉ POROVNÁNÍ MĚST ---");
        
        // Načtení prvního města
        Console.Write("Zadejte název prvního města: ");
        string? city1 = Console.ReadLine();

        // Načtení druhého města
        Console.Write("Zadejte název druhého města: ");
        string? city2 = Console.ReadLine();

        // Validace vstupů - kontrola prázdných hodnot
        if (string.IsNullOrWhiteSpace(city1) || string.IsNullOrWhiteSpace(city2))
        {
            Console.WriteLine("Chyba: Musíte zadat názvy obou měst.");
        }
        else
        {
            Console.WriteLine($"\nStahuji a analyzuji data pro města: {city1} a {city2}...");
            
            // Volání asynchronní logiky s oběma městy
            string report = await aggregator.CompareCitiesAsync(city1, city2);
            Console.WriteLine(report);
        }
    }
    catch (WeatherApiException ex)
    {
        // Ošetření chyb API (např. výpadek sítě)[cite: 1]
        Console.WriteLine($"Chyba služby počasí: {ex.Message}");
    }
    catch (Exception ex)
    {
        // Ošetření neočekávaných chyb[cite: 1]
        Console.WriteLine($"Nastala neočekávaná chyba: {ex.Message}");
    }

    // Dotaz na další srovnání (interaktivita)[cite: 1]
    Console.Write("\nChcete provést další porovnání? (ano/ne): ");
    string? odpoved = Console.ReadLine()?.Trim().ToLower();
    
    if (odpoved != "ano" && odpoved != "a")
    {
        pokracovat = false;
    }
}

Console.WriteLine("\nDěkujeme za použití Weather Dashboard 2026. Nashledanou!");