using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VremenskaPrognoza.Model
{
    public class BasicInfo
    {
        public double Temperature {get; set;}
        public String Condition { get; set;}
        public String Icon { get; set;}

        public BasicInfo(double temperature, String condition, String icon)
        {
            Temperature = temperature;
            Condition = condition;
            Icon = icon;
        }
    }
}
