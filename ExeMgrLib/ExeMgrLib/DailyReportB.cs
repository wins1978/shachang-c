using ExeMgrLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExeMgrLib
{
    public class DailyReportB
    {
        public string Run(string fileFullPath, List<InputInfo> items)
        {
            string status = "";

            List<string> datestrArr = items.Select(o => o.Date).Distinct().ToList();
            foreach(string datestr in datestrArr)
            {
                List<InputInfo> itemsA = items.Where(o => o.Date == datestr).ToList();
                DailyReport rt = new DailyReport();
                string rst = rt.Run(fileFullPath, itemsA);
                if(rst != "")
                {
                    status += rst;
                }
            }
            if(status == "")
            {
                foreach (string datestr in datestrArr)
                {
                    DailyReportForCustomer rt = new DailyReportForCustomer();
                    string rst = rt.Run(fileFullPath, datestr);
                    if (rst != "")
                    {
                        status += rst;
                    }
                }
            }

            if (status == "") status = "导出报表成功";

            return status;
        }
    }
}
