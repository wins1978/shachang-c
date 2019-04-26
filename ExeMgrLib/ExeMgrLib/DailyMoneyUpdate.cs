using ExcelPro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tencent.OA.ATQ.DataAccess;

namespace ExeMgrLib
{
    public class DailyMoneyUpdate
    {
        Dictionary<string, decimal> DataMap = new Dictionary<string, decimal>();
        public void Run()
        {
            UpdateSource();
        }

        private void UpdateSource()
        {
            List<EXCEL_DAILY_DETAIL> datas = GetUncheckData();
            if (datas == null || datas.Count < 1) return;

            DeleteDailyMoney(datas);

            foreach (EXCEL_DAILY_DETAIL edd in datas)
            {
                UpdateDailyMoney(edd);
            }

            List<EXCEL_DAILY_MONEY> list = new List<EXCEL_DAILY_MONEY>();
            foreach (string key in DataMap.Keys)
            {
                string vendor = key.Split('|')[0];
                string datestr = key.Split('|')[1];
                decimal price = DataMap[key];
                string date2 = datestr.Substring(0, 4) + "-" + datestr.Substring(4, 2) + "-" + datestr.Substring(6, 2);
                EXCEL_DAILY_MONEY edm = new EXCEL_DAILY_MONEY();
                edm.DATE_STR = datestr;
                edm.REPORT_DATE = DateTime.Parse(date2);
                edm.TOTAL_PRICE = Math.Round(price, 2);
                edm.UPDATE_DATE = DateTime.Now;
                edm.VENDOR = vendor;
                list.Add(edm);
            }
            if (list.Count > 0)
            {
                using (var db = new OracleDataAccess())
                {
                    db.InsertItems(list);
                }
            }

            //update is_check
            UpdateCheck(datas);
        }

        private void UpdateCheck(List<EXCEL_DAILY_DETAIL> datas)
        {
            foreach(EXCEL_DAILY_DETAIL edd in datas)
            {
                edd.IS_CHECK = "Y";
            }
            using (var db = new OracleDataAccess())
            {
                db.UpdateItems(datas, new string[] { "IS_CHECK" });
            }
        }


        private void UpdateDailyMoney(EXCEL_DAILY_DETAIL data)
        {
            //sum
            string key = data.VENDOR + "|" + data.DATE_STR;
            decimal price = data.UNIT_PRICE.Value * data.WEIGHT.Value;
            if (!DataMap.ContainsKey(key))
            {
                DataMap.Add(key, price);
            }
            else
            {
                decimal pri1 = DataMap[key];
                pri1 += price;
                DataMap[key] = pri1;
            }
        }

        private void DeleteDailyMoney(List<EXCEL_DAILY_DETAIL> datas)
        {
            string[] datearr = datas.Select(o=>o.DATE_STR).Distinct().ToArray();
            

            using (var db =new OracleDataAccess())
            {
                
                var old = db.GetItems<EXCEL_DAILY_MONEY>(o => datearr.Contains(o.DATE_STR));
                    
                if (old != null && old.Count > 0)
                {
                    db.DeleteItems(old);
                }
            }
        }
        

        private List<EXCEL_DAILY_DETAIL> GetUncheckData()
        {
            List<EXCEL_DAILY_DETAIL> datas = new List<EXCEL_DAILY_DETAIL>();
            using (var db = new OracleDataAccess())
            {
                datas = db.GetItems<EXCEL_DAILY_DETAIL>(o => o.IS_CHECK == "N");
            }

            return datas;
        }
    }
}
