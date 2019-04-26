using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExeMgrLib.Model
{
    public class UpdatePriceModel
    {
        public string Vendor { get; set; }

        public string DateStr { get; set; }

        public string ShopNumber { get; set; }

        public string UnitPrice { get; set; }
    }
}
