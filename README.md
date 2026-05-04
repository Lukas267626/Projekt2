# Projekt 2: Weather Aggregator (Dashboard)

Tato aplikace slouží k získávání a porovnávání dat o počasí z externího API (Open-Meteo). Byla vytvořena jako semestrální projekt v jazyce C# (.NET 8).

## Architektura a principy SOLID

Aplikace je navržena s důrazem na čistý kód a oddělení odpovědnosti do logických vrstev:

*   **Models**: Obsahuje třídu `WeatherData`, která slouží jako jednoduchý přepravník dat (DTO).
*   **Services**: 
    *   `IWeatherProvider`: Rozhraní (interface), které definuje kontrakt pro získávání dat. Toto řešení umožňuje snadnou rozšiřitelnost (princip Dependency Inversion).
    *   `OpenMeteoProvider`: Konkrétní implementace rozhraní komunikující s externím API pomocí HttpClient.
    *   `WeatherAggregator`: Aplikační logika, která data agreguje a provádí nad nimi výpočty.
*   **Exceptions**: Vlastní třída `WeatherApiException` pro specifické ošetření chyb v datové vrstvě.
*   **UI (Program.cs)**: Zajišťuje interakci s uživatelem, validaci vstupů a řízení běhu aplikace v cyklu.

## Asynchronní zpracování

V aplikaci je důsledně využíván asynchronní přístup pomocí klíčových slov `async` a `await`:
*   **I/O operace**: Veškerá komunikace s API probíhá asynchronně, aby nedocházelo k blokování hlavního vlákna.
*   **Paralelní zpracování**: V metodě `CompareCitiesAsync` jsou požadavky na obě města spouštěny současně pomocí `Task.WhenAll`, což výrazně zrychluje odezvu aplikace.

## Zpracování dat a uživatelský vstup

*   **Validace**: Aplikace kontroluje uživatelské vstupy a reaguje na prázdné nebo nevalidní názvy měst, čímž předchází pádům.
*   **Interaktivita**: Program běží v cyklu, který uživateli umožňuje provádět opakovaná srovnání bez nutnosti restartu aplikace.
*   **Výpočty**: Agregátor vypočítává absolutní rozdíl teplot mezi dvěma zvolenými lokalitami.

## Poznámka k implementaci (Geolokace)

V aktuální verzi aplikace jsou názvy měst zadávány uživatelem, ale k nim přiřazené zeměpisné souřadnice jsou v kódu nastaveny fixně na dvě odlišné lokality (např. Londýn a Brno). 

Toto rozhodnutí bylo učiněno z následujících důvodů:
1. **Zaměření na zadání**: Hlavním cílem bylo demonstrovat asynchronní získávání dat z JSON zdroje a jejich matematické zpracování, což aplikace splňuje.
2. **Absence Geocoding API**: Implementace další služby pro převod názvu na souřadnice by přesahovala rámec a zadání tohoto projektu.
3. **Konzistence**: Fixní souřadnice zaručují, že uživatel vždy uvidí reálné srovnání dat ze dvou odlišných geografických pásem.

## Použité technologie
*   C# / .NET 8
*   System.Text.Json (parsování dat)
*   HttpClient (HTTP komunikace)