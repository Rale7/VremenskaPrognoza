using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace VremenskaPrognoza.Model
{
    public class Location
    {
        [XmlElement("name")]
        public String Name { get; set; }

        [XmlElement("region")]
        public String Region { get; set; }

        [XmlElement("country")]
        public String Country { get; set; }

        [XmlElement("lat")]
        public Double Latitude { get; set; }

        [XmlElement("lon")]
        public Double Longitude { get; set; }

        [XmlElement("tz_id")]
        public String CountryCode { get; set; }

        [XmlElement("localtime_epoch")]
        public int LocaltimeEpoch { get; set; }

        [XmlElement("localtime")]
        public String Localtime {  get; set; }

    }
}
