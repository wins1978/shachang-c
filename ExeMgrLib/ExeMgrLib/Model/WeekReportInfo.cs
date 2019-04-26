using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExeMgrLib.Model
{
    public class WeekReportInfo
    {
        public string DateTime { get; set; }

        public double ShiZaiWeight { get; set; }

        public double ShiFengWeight { get; set; }

        public double Cash { get; set; }

        public double Debt { get; set; }

        public double TotalIncome { get; set; }

        public string Nodes { get; set; }
    }
}
