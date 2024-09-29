using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace VremenskaPrognoza.Model
{
    public class CurrentWeather
    {
        [XmlElement("last_updated_epoch")]
        public int LastUpdatedEpoch { get; set; }

        [XmlElement("last_updated")]
        public String LastUpdated {  get; set; }

        [XmlElement("temp_c")]
        public double CelsiusTemperature { get; set; }

        [XmlElement("temp_f")]
        public double FahrenheitTemperature { get; set; }

        [XmlElement("is_day")]
        public bool IsDay {  get; set; }

        [XmlElement("condition")]
        public Condition CurrentCondition { get; set; }

        [XmlElement("wind_mph")]
        public double WindSpeedMiles { get; set; }

        [XmlElement("wind_kph")]
        public double WindSpeedKilometer { get; set; }

        [XmlElement("wind_degree")]
        public double WindDegree { get; set; }

        [XmlElement("wind_dir")]
        public String WindDirection { get; set; }

        [XmlElement("pressure_mb")]
        public double PressureMilibar {  get; set; }

        [XmlElement("pressure_in")]
        public double PressureIn {  get; set; }

        [XmlElement("precip_mm")]
        public double PrecipMM {  get; set; }

        [XmlElement("precip_in")]
        public double PrecipIn { get; set; }

        [XmlElement("humidity")]
        public double Humidity { get; set; }

        [XmlElement("cloud")]
        public double Cloud {  get; set; }

        [XmlElement("feelslike_c")]
        public double FeelsLikeCelsius { get; set; }

        [XmlElement("feelslike_f")]
        public double FeelsLikeFahrenheit {  get; set; }

        [XmlElement("windchill_c")]
        public double WindChillCelsius { get; set; }

        [XmlElement("windchill_f")]
        public double WindChillFahrenheit { get; set; }

        [XmlElement("heatindex_c")]
        public double HeatIndexCelsius { get; set; }

        [XmlElement("heatindex_f")]
        public double HeatIndexFahrenheit { get; set; }

        [XmlElement("dewpoint_c")]
        public double DewPointCelsius { get; set; }

        [XmlElement("dewpoint_f")]
        public double DewPointFahrenheit { get; set; }

        [XmlElement("vis_km")]
        public double VisKm {  get; set; }

        [XmlElement("vis_miles")]
        public double VisMiles { get; set; }

        [XmlElement("uv")]
        public double UvIndex { get; set; }

        [XmlElement("gust_mph")]
        public double GustMph { get; set; }

        [XmlElement("gust_kph")]
        public double GustKph { get; set; }

    }
}
