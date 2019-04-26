using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExeMgrLib.Model
{
    public class CustomerDailyModel
    {
        public string Vendor { get; set; }

        public double Weight05 { get; set; }

        public int CarCount05 { get; set; }

        public double Weight12 { get; set; }

        public int CarCount12 { get; set; }

        public double Weight13 { get; set; }

        public int CarCount13 { get; set; }

        public double Weight24 { get; set; }

        public int CarCount24 { get; set; }

        public double WeightShifeng { get; set; }

        public int CarCountShifeng { get; set; }

        public double WeightTouposhi { get; set; }

        public int CarCountTouposhi { get; set; }

        public string Notes { get; set; }
    }
}
