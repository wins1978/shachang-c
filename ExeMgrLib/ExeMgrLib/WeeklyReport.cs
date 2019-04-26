using ExcelPro;
using ExeMgrLib.Model;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tencent.OA.ATQ.Common.Json;
using Tencent.OA.ATQ.DataAccess;

namespace ExeMgrLib
{
    public class WeeklyReport: SheetBase
    {

        public string Run(string startDate, string endDate)
        {
            InitializeWorkbook();
  
            DateTime firstDay = GetWeekUpOfDate(DateTime.Now, DayOfWeek.Monday, -1);
            DateTime lastDay = GetWeekUpOfDate(DateTime.Now, DayOfWeek.Sunday, 0);

            if(!String.IsNullOrEmpty(startDate) && !String.IsNullOrEmpty(endDate))
            {
                firstDay = DateTime.Parse(startDate);
                lastDay = DateTime.Parse(endDate);
            }

            if (firstDay > lastDay)
            {
                return "错误，开始时间大于结束时间";
            }

            string title = CreeateFirstPage(firstDay, lastDay);
            CreateSecondPage(firstDay, lastDay);
            WriteToFile("销售周报"+title);

            return "";
        }

        #region FirstPage

        private string CreeateFirstPage(DateTime firstDay, DateTime lastDay)
        {
            string guid = Guid.NewGuid().ToString();

            ISheet sheet = hssfworkbook.CreateSheet("产量");
            CreateTitle(sheet, "石场销售周报",6);

            //sub title
            string firdayStr = firstDay.GetDateTimeFormats('D')[0].ToString();
            string lastDayStr = lastDay.GetDateTimeFormats('D')[0].ToString();
            string title = firdayStr + "-" + lastDayStr;
            CreateSubTitle(sheet, title, 6);

            //title2
            IRow row2 = sheet.CreateRow(2);
            IRow row3 = sheet.CreateRow(3);
            CreateMergeCellString("日期", sheet, row2, 2, 3, 0, 0);
            CreateCellString(row2, "石仔", 1);
            CreateCellString(row3, "吨数", 1);
            CreateCellString(row2, "石粉", 2);
            CreateCellString(row3, "吨数", 2);
            CreateMergeCellString("现金", sheet, row2, 2, 3, 3, 3);
            CreateMergeCellString("欠账", sheet, row2, 2, 3, 4, 4);
            CreateMergeCellString("总收入", sheet, row2, 2, 3, 5, 5);
            CreateMergeCellString("备注", sheet, row2, 2, 3, 6, 6);

            WeekReportInfo wc = new WeekReportInfo();

            TimeSpan d3 = lastDay.Subtract(firstDay);
            int days = d3.Days+1;

            for (int i = 0; i < days; i++)
            {
                WeekReportInfo wri = GetWeekReportInfo(firstDay,i);
                IRow rr = sheet.CreateRow(4+i);
                CreateCellString(rr, wri.DateTime, 0);

                CreateCellDouble(rr, wri.ShiZaiWeight, 1);
                wc.ShiZaiWeight += wri.ShiZaiWeight;

                CreateCellDouble(rr, wri.ShiFengWeight, 2);
                wc.ShiFengWeight += wri.ShiFengWeight;

                CreateCellDouble(rr, wri.Cash, 3);
                wc.Cash += wri.Cash;

                CreateCellDouble(rr, wri.Debt, 4);
                wc.Debt += wri.Debt;

                CreateCellDouble(rr, wri.TotalIncome, 5);
                wc.TotalIncome += wri.TotalIncome;

                CreateCellString(rr, wri.Nodes, 6);
            }

            IRow rrt = sheet.CreateRow(4+ days);
            CreateCellString(rrt, "合计：", 0);
           
            //CreateCellDouble(rrt, wc.ShiZaiWeight, 1);
            //CreateCellDouble(rrt, wc.ShiFengWeight, 2);
            //CreateCellDouble(rrt, wc.Cash, 3);
            //CreateCellDouble(rrt, wc.Debt, 4);
            //CreateCellDouble(rrt, wc.TotalIncome, 5);
            CreateCellString(rrt, wc.Nodes, 6);

            string col = (4 + days).ToString();
            CreateSumFormula(rrt, 1, "B5", "B"+ col);
            CreateSumFormula(rrt, 2, "C5", "C" + col);
            CreateSumFormula(rrt, 3, "D5", "D" + col);
            CreateSumFormula(rrt, 4, "E5", "E" + col);
            CreateSumFormula(rrt, 5, "F5", "F" + col);

            sheet.SetColumnWidth(0, 14*256);
            InsertWeeklySummary(title, guid, firstDay);

            return title;
        }

        private void InsertWeeklySummary(string title, string guid, DateTime firstDay)
        {
            EXCEL_WEEKLY_SUMMARY ws = new EXCEL_WEEKLY_SUMMARY();
            using(var db=new OracleDataAccess())
            {
                var old = db.GetSingleItem<EXCEL_WEEKLY_SUMMARY>(o => o.TITLE == title);
                if(old != null)
                {
                    db.DeleteItem(old);
                }
            }
            ws.FILE_NAME = "销售周报"+title+".xls";
            ws.GUID_KEY = guid;
            ws.REPORT_DATE = firstDay;
            ws.TITLE = title;
            using (var db = new OracleDataAccess())
            {
                db.InsertItem(ws);
            }
        }

        private WeekReportInfo GetWeekReportInfo(DateTime firstDay, int wd)
        {
            DateTime dt = firstDay.AddDays(wd);
            string dateStr = dt.ToString("yyyyMMdd");
            //DayOfWeek week = DayOfWeek.Sunday;
            //if (wd == 1) week = DayOfWeek.Monday;
            //if (wd == 2) week = DayOfWeek.Tuesday;
            //if (wd == 3) week = DayOfWeek.Wednesday;
            //if (wd == 4) week = DayOfWeek.Thursday;
            //if (wd == 5) week = DayOfWeek.Friday;
            //if (wd == 6) week = DayOfWeek.Saturday;
            //if (wd == 7) week = DayOfWeek.Sunday;

            WeekReportInfo wri = new WeekReportInfo();

            //DateTime day = DateTime.Now;
            //if (wd == 7)
            //{
            //    day = GetWeekUpOfDate(DateTime.Now, week, 0);
            //}
            //else
            //{
            //    day = GetWeekUpOfDate(DateTime.Now, week, -1);
            //}
            
            List<EXCEL_DAILY_DETAIL> shiZaiList = GetWeekDetailShiZai(dateStr);
            List<EXCEL_DAILY_DETAIL> shiFengList = GetWeekDetailShiFeng(dateStr);

            wri.DateTime = dt.GetDateTimeFormats('D')[0].ToString();
            foreach (EXCEL_DAILY_DETAIL dd in shiZaiList)
            {
                wri.ShiZaiWeight += (double)dd.WEIGHT.Value;
                if (dd.VENDOR == "零售")
                {
                    double d = (double)(dd.WEIGHT.Value * dd.UNIT_PRICE.Value);
                    d = GetMoneyRoundD(d);
                    wri.Cash += d;
                }
                else
                {
                    wri.Debt += (double)(dd.WEIGHT.Value * dd.UNIT_PRICE.Value);
                }
                
            }

            foreach (EXCEL_DAILY_DETAIL dd in shiFengList)
            {
                wri.ShiFengWeight += (double)dd.WEIGHT.Value;
                if (dd.VENDOR == "零售")
                {
                    double d = (double)(dd.WEIGHT.Value * dd.UNIT_PRICE.Value);
                    d = GetMoneyRoundD(d);
                    wri.Cash += d;
                }
                else
                {
                    wri.Debt += (double)(dd.WEIGHT.Value * dd.UNIT_PRICE.Value);
                }
                
            }
            wri.TotalIncome += wri.Cash + wri.Debt;
            wri.Cash = Math.Round(wri.Cash, 0);
            wri.ShiZaiWeight = Math.Round(wri.ShiZaiWeight, 2);
            wri.ShiFengWeight = Math.Round(wri.ShiFengWeight, 2);
            wri.Debt = Math.Round(wri.Debt, 2);
            wri.TotalIncome = Math.Round(wri.TotalIncome, 2);

            return wri;
        }

        List<EXCEL_DAILY_DETAIL> GetWeekDetailShiZai(string dateStr)
        {
            //DateTime firstDay = GetWeekUpOfDate(DateTime.Now, week, -1);
            //string dateStr = firstDay.ToString("yyyyMMdd");
            List<EXCEL_DAILY_DETAIL> ddList = new List<EXCEL_DAILY_DETAIL>();
            using (var db = new OracleDataAccess())
            {
                ddList = db.GetItems<EXCEL_DAILY_DETAIL>(o => o.GROUP_NAME == "石仔" && o.DATE_STR== dateStr);
            }

            return ddList;
        }

        List<EXCEL_DAILY_DETAIL> GetWeekDetailShiFeng(string dateStr)
        {
            //DateTime firstDay = GetWeekUpOfDate(DateTime.Now, week, -1);
            //string dateStr = firstDay.ToString("yyyyMMdd");
            List<EXCEL_DAILY_DETAIL> ddList = new List<EXCEL_DAILY_DETAIL>();
            using (var db = new OracleDataAccess())
            {
                ddList = db.GetItems<EXCEL_DAILY_DETAIL>(o => o.GROUP_NAME == "石粉" && o.DATE_STR == dateStr);
            }

            return ddList;
        }

        private DateTime GetWeekUpOfDate(DateTime dt, DayOfWeek weekday, int Number)
        {
            int wd1 = (int)weekday;
            int wd2 = (int)dt.DayOfWeek;
            return wd2 == wd1 ? dt.AddDays(7 * Number) : dt.AddDays(7 * Number - wd2 + wd1);
        }

        #endregion

        #region Secondpage

        private void CreateSecondPage(DateTime firstDay, DateTime lastDay)
        {
            //Get Goods List
            List<EXCEL_GOODS_LIST> goodsList = GetGoodsList();
            int goodsListCount = goodsList.Count;

            ISheet sheet = hssfworkbook.CreateSheet("客户");
            CreateTitle(sheet, "石场销售周报（按客户名称统计）", goodsListCount + 5);

            string firdayStr = firstDay.GetDateTimeFormats('D')[0].ToString();
            string lastDayStr = lastDay.GetDateTimeFormats('D')[0].ToString();
            string subTitle = firdayStr + "-" + lastDayStr;
            CreateSubTitle(sheet, subTitle, goodsListCount + 5);

            IRow row2 = sheet.CreateRow(2);
            IRow row3 = sheet.CreateRow(3);
            CreateMergeCellString("客户名称", sheet, row2, 2, 3, 0, 0);
            CreateMergeCellString("石料重量（单位：吨", sheet, row2, 2, 2, 1, goodsListCount);
            CreateMergeCellString("单价（吨/元）", sheet, row2, 2, 2, goodsListCount + 1, goodsListCount + 2);
            CreateCellString(row3, "石仔", goodsListCount + 1);
            CreateCellString(row3, "石粉", goodsListCount + 2);
            CreateMergeCellString("本周金额", sheet, row2, 2, 3, goodsListCount + 3, goodsListCount + 3);
            CreateMergeCellString("本月累计金额", sheet, row2, 2, 3, goodsListCount + 4, goodsListCount + 4);
            CreateMergeCellString("备注", sheet, row2, 2, 3, goodsListCount + 5, goodsListCount + 5);

            Dictionary<string, int> cellMap = new Dictionary<string, int>();
            int gIndex = 1;
            foreach(EXCEL_GOODS_LIST g in goodsList)
            {
                CreateCellString(row3, g.GOODS_NAME, gIndex);
                cellMap.Add(g.GOODS_NAME, gIndex);
                gIndex++;
            }

            List<EXCEL_DAILY_WEEKREPORT_V> shizaiList = GetWeeklyDetails(firstDay,lastDay, "石仔");
            var shizaiGroup = shizaiList;
            var shizaiPriceGroup = (from a in shizaiList
                                    group a by new { a.VENDOR, a.UNIT_PRICE } into b
                                    select new
                                    {
                                        VENDOR = b.Key.VENDOR,
                                        UNIT_PRICE = b.Key.UNIT_PRICE
                                    });
            Dictionary<string, Dictionary<string, string>> mm1 = new Dictionary<string, Dictionary<string, string>>();
            foreach (var tt in shizaiPriceGroup)
            {
                string key = tt.VENDOR + "|" + tt.UNIT_PRICE.Value.ToString();
                Dictionary<string, string> mm2 = new Dictionary<string, string>();
                mm1.Add(key, mm2);
            }
            foreach(var it2 in shizaiGroup)
            {
                string key = it2.VENDOR + "|" + it2.UNIT_PRICE.Value.ToString();
                Dictionary<string, string> mm2 = mm1[key];
                string mw = it2.MONTHLY_WEIGHT.HasValue ? it2.MONTHLY_WEIGHT.Value.ToString() : "0";
                mm2.Add(it2.GOODS_NAME, it2.WEEKLY_WEIGHT.ToString() +"|"+ mw);
                mm1[key] = mm2;
            }

            
            int indexr = 4;
            List<string> rowCheck = new List<string>();
            foreach (var tt in shizaiPriceGroup)
            {
                IRow rr = sheet.CreateRow(indexr);
                CreateCellString(rr, tt.VENDOR, 0);

                double uprice = (double)tt.UNIT_PRICE.Value;
                CreateCellDouble(rr, uprice, goodsListCount + 1);

                double weeklyPrice = 0;
                double monlyPrice = 0;
                string key = tt.VENDOR + "|" + tt.UNIT_PRICE.Value.ToString();
                Dictionary<string, string> mm2 = mm1[key];
                foreach(string kk in mm2.Keys)
                {
                    int cellIdx = cellMap[kk];
                    double v2 = Double.Parse(mm2[kk].Split('|')[0]);
                    CreateCellDouble(rr, v2, cellIdx);
                    
                    //weekly pri
                    double weight = Double.Parse(mm2[kk].Split('|')[0]);
                    double mon_weight = Double.Parse(mm2[kk].Split('|')[1]);
                    double dd1 = Math.Round(weight * (double)tt.UNIT_PRICE.Value, 2);
                    if (tt.VENDOR == "零售")
                    {
                        dd1 = GetMoneyRoundD(dd1);
                    }
                    
                    weeklyPrice += dd1;
                    //monly pri
                    double dd2 = Math.Round(mon_weight * (double)tt.UNIT_PRICE.Value, 2);
                    if (tt.VENDOR == "零售")
                    {
                        dd2 = GetMoneyRoundD(dd2);
                    }
                    monlyPrice += dd2;
                }

                CreateCellDouble(rr, weeklyPrice, goodsListCount + 3);
                CreateCellDouble(rr, monlyPrice, goodsListCount + 4);

                rowCheck.Add(tt.VENDOR+"|"+indexr.ToString());
                indexr++;
            }
            //=================shifeng=========================
            List<EXCEL_DAILY_WEEKREPORT_V> shifengList = GetWeeklyDetails(firstDay,lastDay, "石粉");
            var shifengPriceGroup = (from a in shifengList
                                     group a by new { a.VENDOR, a.UNIT_PRICE } into b
                                     select new
                                     {
                                         VENDOR = b.Key.VENDOR,
                                         UNIT_PRICE = b.Key.UNIT_PRICE
                                     });
            var shifengGroup = shifengList;

            int totalrow = rowCheck.Count;
            foreach (var tt in shifengPriceGroup)
            {
                int rindex = -1;
                string itemstr = "";
                foreach(string s in rowCheck)
                {
                    string vender = s.Split('|')[0];
                    if (tt.VENDOR == vender)
                    {
                        itemstr = s;
                        rindex = Int32.Parse(s.Split('|')[1]);
                        break;
                    }
                }
                if (itemstr != "")
                {
                    rowCheck.Remove(itemstr);
                    //shifeng price
                    IRow tmpR = sheet.GetRow(rindex);
                    double v23 = (double)tt.UNIT_PRICE.Value;
                    CreateCellDouble(tmpR, v23, goodsListCount + 2);

                    int cindex = cellMap["石粉"];
                    double weeklyPrice = 0;
                    double monthlyPrice = 0;
                    foreach (var ss in shifengGroup)
                    {
                        if(ss.VENDOR== tt.VENDOR && ss.UNIT_PRICE == tt.UNIT_PRICE)
                        {
                            IRow tmpR2 = sheet.GetRow(rindex);
                            CreateCellDouble(tmpR2, ss.WEEKLY_WEIGHT.Value, cindex);

                            string totalWP = sheet.GetRow(rindex).GetCell(goodsListCount + 3).ToString();
                            double totalWPD = 0;
                            if (!String.IsNullOrEmpty(totalWP))
                            {
                                totalWPD = Double.Parse(totalWP);
                            }
                            weeklyPrice += Math.Round((double)ss.UNIT_PRICE.Value * ss.WEEKLY_WEIGHT.Value, 2)+ totalWPD;
                            CreateCellDouble(tmpR2, weeklyPrice, goodsListCount + 3);

                            double mw1 = ss.MONTHLY_WEIGHT.HasValue ? ss.MONTHLY_WEIGHT.Value : 0;
                            monthlyPrice += Math.Round((double)ss.UNIT_PRICE.Value * mw1, 2) + totalWPD;
                            CreateCellDouble(tmpR2, monthlyPrice, goodsListCount + 4);
                            break;
                        }
                    }
                }
                else
                {
                    IRow rr3=sheet.CreateRow(totalrow +4);
                    CreateCellString(rr3, tt.VENDOR, 0);
                    CreateCellDouble(rr3, (double)tt.UNIT_PRICE.Value, goodsListCount + 2);
                    int cindex = cellMap["石粉"];
                    foreach (var ss in shifengGroup)
                    {
                        if (ss.VENDOR == tt.VENDOR && ss.UNIT_PRICE == tt.UNIT_PRICE)
                        {
                            CreateCellDouble(rr3,ss.WEEKLY_WEIGHT.Value, cindex);
                            double weeklyPrice = Math.Round((double)ss.UNIT_PRICE.Value * ss.WEEKLY_WEIGHT.Value, 2);
                            CreateCellDouble(rr3, weeklyPrice, goodsListCount + 3);

                            double mw2 = ss.MONTHLY_WEIGHT.HasValue ? ss.MONTHLY_WEIGHT.Value : 0;
                            double monthlyPrice = Math.Round((double)ss.UNIT_PRICE.Value * mw2, 2);
                            CreateCellDouble(rr3, monthlyPrice, goodsListCount +4);
                            break;
                        }
                    }

                    totalrow++;
                }
            }

            FormatCell(sheet, shifengList, shizaiList, goodsList);
            sheet.SetColumnWidth(0, 12 * 256);
            sheet.SetColumnWidth(10, 12 * 256);
        }

        private void FormatCell(ISheet sheet,List<EXCEL_DAILY_WEEKREPORT_V> shifengList, List<EXCEL_DAILY_WEEKREPORT_V> shizaiList, List<EXCEL_GOODS_LIST> goodsList)
        {
            int lastCol = goodsList.Count+6;
            Dictionary<string, string> map = new Dictionary<string, string>();
            foreach(EXCEL_DAILY_WEEKREPORT_V item in shifengList)
            {
                if (!map.ContainsKey(item.VENDOR))
                {
                    map.Add(item.VENDOR, "");
                }
            }
            foreach (EXCEL_DAILY_WEEKREPORT_V item in shizaiList)
            {
                if (!map.ContainsKey(item.VENDOR))
                {
                    map.Add(item.VENDOR, "");
                }
            }
            int lastRow = map.Count + 10;
            for(int i = 4; i < lastRow; i++)
            {
                IRow rr = sheet.GetRow(i);
                if (rr == null)
                {
                    continue;
                }
                
                for(int j = 0; j < lastCol; j++)
                {
                    ICell cc = rr.GetCell(j);
                    if(cc == null)
                    {
                        cc = rr.CreateCell(j);
                    }
                    cc.CellStyle = baseCellStyle;
                }
            }

        }

        #region Private

        List<EXCEL_GOODS_LIST> GetGoodsList()
        {
            List<EXCEL_GOODS_LIST> goodsList = new List<EXCEL_GOODS_LIST>();
            using (var db = new OracleDataAccess())
            {
                goodsList = db.GetItems<EXCEL_GOODS_LIST>(o => true).OrderBy(o => o.ORDER_INDEX).ToList();
            }

            return goodsList;
        }

        List<EXCEL_DAILY_WEEKREPORT_V> GetWeeklyDetails(DateTime startDate,DateTime endDate, string goodsName)
        {
            List<EXCEL_DAILY_WEEKREPORT_V> ddList = new List<EXCEL_DAILY_WEEKREPORT_V>();

            using (var da = new OracleDataAccess())
            {
                List<string> pNames = new List<string>();
                pNames.Add("v_StartDate");
                pNames.Add("v_EndDate");
                pNames.Add("v_Goodsname");
                pNames.Add("cur cv_1");
                List<object> pValues = new List<object>();
                pValues.Add(startDate);
                pValues.Add(endDate);
                pValues.Add(goodsName);
                pValues.Add(null);

                var item = da.SelectDataProc("EXCEL_PRO_PKG.P_GET_WEEKLY_REPORT", pNames.ToArray(), pValues.ToArray());
                 ddList = JsonUtils.ParseJson<List<EXCEL_DAILY_WEEKREPORT_V>>(JsonUtils.GetItemJson(item));
            }

            return ddList;
        }
        #endregion

#endregion
    }
}
