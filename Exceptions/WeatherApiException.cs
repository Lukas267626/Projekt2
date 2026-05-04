namespace WeatherApp.Exceptions
{
    public class WeatherApiException : Exception
    {
        public WeatherApiException(string message) : base(message) { }
        public WeatherApiException(string message, Exception innerException) 
            : base(message, innerException) { }
    }
}