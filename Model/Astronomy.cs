using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace VremenskaPrognoza.Model
{
    public class Astronomy
    {
        [XmlElement("astro")]
        public Astro Astro { get; set; }
    }
}
