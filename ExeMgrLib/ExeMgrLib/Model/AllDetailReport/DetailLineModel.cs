using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExeMgrLib.Model
{
    public class DetailLineModel
    {
        public string Vendor { get; set; }

        public int CarCount { get; set; }

        public string SubVendor { get; set; }

        public string DateStr { get; set; }

        public double Weight05 { get; set; }

        public double Weight12 { get; set; }

        public double Weight13 { get; set; }

        public double Weight24 { get; set; }

        public string NumberFlag { get; set; }

        public double WeightShifeng { get; set; }

        public double WeightTouposhi { get; set; }

        public double UnitPrice { get; set; }

        public double TotalPrice { get; set; }

        public string CarNumber { get; set; }
    }
}
