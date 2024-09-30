using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace VremenskaPrognoza.Model
{
    public class Astro
    {

        [XmlElement("sunrise")]
        public String Sunrise { get; set; }

        [XmlElement("sunset")]
        public String Sunset { get; set; }

        [XmlElement("moonrise")]
        public String Moonrise { get; set; }

        [XmlElement("moonset")]
        public String Moonset { get; set; }

        [XmlElement("moon_phase")]
        public String Moonphase { get; set; }

        [XmlElement("moon_illumination")]
        public double MoonIllumination {  get; set; }

        [XmlElement("is_moon_up")]
        public bool IsMoonUp { get; set; }

        [XmlElement("is_sun_up")]
        public bool IsSunUp { get; set; }
    }
}
