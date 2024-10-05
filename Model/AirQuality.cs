using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace VremenskaPrognoza.Model
{
    public class AirQuality
    {
        private String[] UsEpaNames =
        {
            "Good", "Moderate", "Unhealthy for sensitive groups",
            "Unhealthy", "Very unhealthy", "Hazardous"
        };

        [XmlElement("co")]
        public double CO {  get; set; }

        [XmlElement("no2")]
        public double NO2 { get; set; }

        [XmlElement("o3")]
        public double O3 { get; set; }

        [XmlElement("so2")]
        public double SO2 { get; set; }

        [XmlElement("pm2_5")]
        public double Pm2_5 { get; set; }

        [XmlElement("pm10")]
        public double Pm10 { get; set; }

        [XmlElement("us-epa-index")]
        public int UsEpaIndex { get; set; }

        public String UsEpaName
        {
            get
            {
                if (UsEpaIndex >= 0 && UsEpaIndex < UsEpaNames.Length)
                {
                    return UsEpaNames[UsEpaIndex];
                } else
                {
                    return "";
                }
            }
        }

        [XmlElement("gb-defra-index")]
        public int GbDefraIndex {  get; set; }
    }
}
