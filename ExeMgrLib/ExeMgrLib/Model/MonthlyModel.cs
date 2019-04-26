using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExeMgrLib.Model
{
    public class MonthlyRowMap
    {
        public int Index { get; set; }
        public string Vendor { get; set; }
        public List<MonthlyRow> MonthlyRowList { get; set; }

        public double TotalPrice { get; set; }

        public int GroupCount { get; set; }
    }

    public class MonthlyModelMap
    {
        public string Vendor { get; set; }
        public List<MonthlyModel> MonthlyModelList { get; set; }
    }

    public class MonthlyRow
    {
        public string Vendor { get; set; }

        public double ShiZaiWeight { get; set; }

        public int ShiZaiCarCount { get; set; }

        public double ShiZaiUnitPrice { get; set; }

        public double ShiFengWeight { get; set; }

        public int ShiFengCarCount { get; set; }

        public double ShiFengUnitPrice { get; set; }

        public double TouPoShiWeight { get; set; }

        public int TouPoShiCarCount { get; set; }

        public double TouPoShiUnitPrice { get; set; }

        public double TotalGroupPrice { get; set; }
    }

    public class MonthlyModel
    {
        public string Vendor { get; set; }

        public string GroupName { get; set; }

        public int CarCount { get; set; }

        public double UnitPrice { get; set; }

        public double TotalNetWeight { get; set; }

        public double TotalPrice { get; set; }
    }
}
