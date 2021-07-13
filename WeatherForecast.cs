using System;

namespace sample_application
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public WeatherSummary Summary { get; set; }
    }

    public class WeatherSummary
    {
        public string FaSummaryClass { get; set; }

        public string Summary { get; set; }
    }
}
