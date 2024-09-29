using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace VremenskaPrognoza.Model
{
    public class Condition
    {
        [XmlElement("text")]
        public String Text { get; set; }

        [XmlElement("icon")]
        public String Icon { get; set; }

        [XmlElement("code")]
        public int Code { get; set; }
    }
}
