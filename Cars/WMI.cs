using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cars
{
    public class Auto
    {
        public string Country { get; set; }
        public string CreatedOn { get; set; }
        public string DateAvailableToPublic { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string UpdatedOn { get; set; }
        public string WMI { get; set; }
        public string VehicleType { get; set; }
    }

    public class AutoFilter
    {
        public string SearchText { get; set; }
        public string Country { get; set; }
    }
}
