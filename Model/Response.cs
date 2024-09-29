using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace VremenskaPrognoza.Model
{
    [XmlRoot("root")]
    public class Response
    {
        [XmlElement("location")]
        public Location Location { get; set; }

        [XmlElement("current")]
        public CurrentWeather CurrentWeather { get; set; }
    }
}
