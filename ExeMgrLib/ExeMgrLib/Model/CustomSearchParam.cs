using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExeMgrLib.Model
{
    public class CustomSearchParam
    {
        public List<string> VendorNameList { get; set; }

        public List<string> CarNumberList { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public List<string> ShopNumberList { get; set; }

        public int PageLimit { get; set; }

        public int PageOffset { get; set; }

        public string SearchType { get; set; }
    }
}
