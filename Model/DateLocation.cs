using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VremenskaPrognoza.Model
{
    public class DateLocation
    {
        public String CurrentDateTime { get; set; }
        public String CurrentLocation {  get; set; }

        public DateLocation(DateTime currentDateTime, String currentLocation)
        { 
            this.CurrentDateTime = currentDateTime.ToShortDateString(); 
            this.CurrentLocation = currentLocation; 
        }

    }
}
