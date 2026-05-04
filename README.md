# Projekt 2: Weather Aggregator (Dashboard)

Tato aplikace slouží k získávání a porovnávání dat o počasí z externího API (Open-Meteo). Byla vytvořena jako semestrální projekt v jazyce C# (.NET 8).

## Architektura a principy SOLID

Aplikace je navržena s důrazem na čistý kód a oddělení odpovědnosti do logických vrstev[cite: 1]:

*   **Models**: Obsahuje třídu `WeatherData`, která slouží jako jednoduchý přepravník dat (DTO)[cite: 1].
*   **Services**: 
    *   `IWeatherProvider`: Rozhraní (interface), které definuje kontrakt pro získávání dat. Toto řešení umožňuje snadnou rozšiřitelnost – pokud bychom chtěli změnit poskytovatele počasí, stačí vytvořit novou implementaci rozhraní bez nutnosti měnit zbytek aplikace (princip Dependency Inversion)[cite: 1].
    *   `OpenMeteoProvider`: Konkrétní implementace rozhraní komunikující s externím API[cite: 1].
    *   `WeatherAggregator`: Aplikační logika, která data agreguje a provádí nad nimi výpočty[cite: 1].
*   **UI (Program.cs)**: Zajišťuje pouze interakci s uživatelem a inicializaci komponent[cite: 1].

## Asynchronní zpracování

V aplikaci je důsledně využíván asynchronní přístup pomocí klíčových slov `async` a `await`[cite: 1]:
*   **I/O operace**: Veškerá komunikace s externím API probíhá asynchronně přes `HttpClient`, aby nedocházelo k blokování hlavního vlákna[cite: 1].
*   **Paralelní zpracování**: V metodě `CompareCitiesAsync` jsou požadavky na více měst spouštěny současně pomocí `Task.WhenAll`. To výrazně zrychluje odezvu aplikace, protože nečekáme na každé město zvlášť, ale na všechna najednou[cite: 1].

## Zpracování dat

Aplikace splňuje požadavek na smysluplné zpracování stažených dat[cite: 1]:
*   **Agregace a kombinace**: Data jsou stahována z více zdrojů (požadavků) naráz[cite: 1].
*   **Výpočty**: Aplikace vypočítává absolutní rozdíl teplot mezi dvěma městy a zaokrouhluje výsledek pro lepší čitelnost[cite: 1].

## Výjimky a chybové stavy

Robustnost aplikace je zajištěna správným handlingem výjimek[cite: 1]:
*   **Vlastní výjimka**: Byla vytvořena třída `WeatherApiException`, která slouží k jasné identifikaci chyb vzniklých v datové vrstvě[cite: 1].
*   **Propagace**: Výjimky jsou zachytávány v místě vzniku (např. chyby sítě nebo chybný formát JSON), zabaleny do vlastní výjimky a propagovány do UI vrstvy[cite: 1].
*   **Uživatelské info**: UI vrstva zachycuje tyto stavy a vypisuje uživateli srozumitelné hlášení namísto pádu aplikace[cite: 1].

## Použité technologie
*   C# / .NET 8
*   System.Text.Json (parsování dat)
*   HttpClient (HTTP komunikace)