using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExeMgrLib.Model
{
    public class WeeklyCellInfo
    {
        public string Vendor { get; set; }

        public string ShiZaiPri { get; set; }

        public string ShiFengPri { get; set; }

        public string GoodsName { get; set; }

        public string Weight { get; set; }
    }

    public class WeeklyCellCollectionInfo
    {
        public string Vendor { get; set; }

        public string ShiZaiPri { get; set; }

        public string ShiFengPri { get; set; }

        public Dictionary<string,string> GoodsWeight { get; set; }
    }
}
