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
using Tencent.OA.ATQ.DataAccess;

namespace ExeMgrLib
{
    public class MonthlyReport: SheetBase
    {
        int rowIndex = 3;
        public string Run(string yearMonth)
        {
            if (String.IsNullOrEmpty(yearMonth))
            {
                return "无效月份";
            }
            InitializeWorkbook();

            DateTime month = DateTime.Parse(yearMonth);
            string title = CreeateFirstPage(month);

            WriteToFile("仙仔山石场出库核算表"+title);

            return "仙仔山石场出库核算表" + title+".xls";
        }

        private string CreeateFirstPage(DateTime month)
        {
            ISheet sheet = hssfworkbook.CreateSheet("出库核算");
            BuildFirstPageHeader(sheet, month);

            List<EXCEL_IMPORT_DAILY> ddList = GetData(month);
            foreach(EXCEL_IMPORT_DAILY item in ddList)
            {
                item.GROUP_NAME = item.GROUP_NAME == "石粉" ? "石粉" : item.GROUP_NAME == "头破石" ? "头破石" : "石仔";
            }
            var dataSummary = (from a in ddList
                               group a by new { a.TAKE_DEPT, a.GROUP_NAME, a.UNIT_PRICE } into b
                               select new
                               {
                                   Vendor = b.Key.TAKE_DEPT,
                                   GroupName = b.Key.GROUP_NAME,
                                   CarCount = b.Count(),
                                   UnitPrice = (double)b.Key.UNIT_PRICE,
                                   TotalNetWeight = b.Sum(c => Math.Round(Double.Parse(c.NET_WEIGHT)/1000,2)),
                                   TotalPrice = b.Sum(c => Math.Round(Double.Parse(c.NET_WEIGHT) / 1000, 2)* (double)b.Key.UNIT_PRICE)
                               });

            List<MonthlyModel> monlyModels = new List<MonthlyModel>();
            foreach (var item in dataSummary)
            {
                MonthlyModel mm = new MonthlyModel();
                mm.CarCount = item.CarCount;
                mm.GroupName = item.GroupName;
                mm.TotalNetWeight = item.TotalNetWeight;
                mm.TotalPrice = item.TotalPrice;
                mm.UnitPrice = item.UnitPrice;
                mm.Vendor = item.Vendor;

                monlyModels.Add(mm);
            }

            Dictionary<string, string> vendorMap = new Dictionary<string, string>();
            foreach(var item in dataSummary)
            {
                if (!vendorMap.ContainsKey(item.Vendor))
                {
                    vendorMap.Add(item.Vendor, item.Vendor);
                }
            }

            List<MonthlyModelMap> MonthlyModelMapList = new List<MonthlyModelMap>();
            foreach (string vendor in vendorMap.Keys)
            {
                MonthlyModelMap mmm = new MonthlyModelMap();
                mmm.Vendor = vendor;
                mmm.MonthlyModelList = new List<MonthlyModel>();

                //shifeng
                List<MonthlyModel> mmShifengList = monlyModels.Where(o => o.Vendor == vendor && o.GroupName == "石粉").ToList();
                foreach(MonthlyModel mmShifeng in mmShifengList)
                {
                    mmm.MonthlyModelList.Add(mmShifeng);
                }
                
                //touposhi
                List<MonthlyModel> mmTouPoShiList = monlyModels.Where(o => o.Vendor == vendor && o.GroupName == "头破石").ToList();
                foreach (MonthlyModel mmTouPoShi in mmTouPoShiList)
                {
                    mmm.MonthlyModelList.Add(mmTouPoShi);
                }

                List<MonthlyModel> mmShiZaiList = monlyModels.Where(o => o.Vendor == vendor && o.GroupName == "石仔").ToList();
                foreach(MonthlyModel mmShiZai in mmShiZaiList)
                {
                    mmm.MonthlyModelList.Add(mmShiZai);
                }

                MonthlyModelMapList.Add(mmm);
            }

            int mrIndex = 1;
            List<MonthlyRowMap> MonthlyRowMapList = new List<MonthlyRowMap>();
            foreach (MonthlyModelMap mmm in MonthlyModelMapList)
            {
                var dd = (from a in mmm.MonthlyModelList
                          group a by new { a.Vendor, a.UnitPrice } into b
                                   select new
                                   {
                                       Vendor = b.Key.Vendor,
                                       UnitPrice = b.Key.UnitPrice
                                   });
                MonthlyRowMap mrm = new MonthlyRowMap();
                mrm.GroupCount = dd.Count();
                mrm.Index = mrIndex;
                mrm.Vendor = mmm.Vendor;
                //mrm.TotalPrice
                mrm.MonthlyRowList = new List<MonthlyRow>();
                foreach(var item in dd)
                {
                    double unitPrice = item.UnitPrice;
                    MonthlyRow mr = new MonthlyRow();
                    foreach(MonthlyModel mm1 in mmm.MonthlyModelList)
                    {
                        if(mm1.UnitPrice == unitPrice)
                        {
                            if (mm1.GroupName == "石粉")
                            {
                                mr.ShiFengCarCount = mm1.CarCount;
                                mr.ShiFengUnitPrice = mm1.UnitPrice;
                                mr.ShiFengWeight = mm1.TotalNetWeight;
                                if (item.Vendor == "零售")
                                {
                                    double d = mm1.TotalNetWeight * mm1.UnitPrice;
                                    d = GetMoneyRoundD(d);
                                    mr.TotalGroupPrice += d;
                                }
                                else
                                {
                                    mr.TotalGroupPrice += mm1.TotalNetWeight * mm1.UnitPrice;
                                }
                                
                            }
                            else if(mm1.GroupName == "头破石")
                            {
                                mr.TouPoShiCarCount = mm1.CarCount;
                                mr.TouPoShiUnitPrice = mm1.UnitPrice;
                                mr.TouPoShiWeight = mm1.TotalNetWeight;
                                if (item.Vendor == "零售")
                                {
                                    double d = mm1.TotalNetWeight * mm1.UnitPrice;
                                    d = GetMoneyRoundD(d);
                                    mr.TotalGroupPrice += d;
                                }
                                else
                                {
                                    mr.TotalGroupPrice += mm1.TotalNetWeight * mm1.UnitPrice;
                                }
                                    
                            }
                            else if(mm1.GroupName == "石仔")
                            {
                                mr.ShiZaiCarCount = mm1.CarCount;
                                mr.ShiZaiUnitPrice = mm1.UnitPrice;
                                mr.ShiZaiWeight = mm1.TotalNetWeight;
                                if (item.Vendor == "零售")
                                {
                                    double d = mm1.TotalNetWeight * mm1.UnitPrice;
                                    d = GetMoneyRoundD(d);
                                    mr.TotalGroupPrice += d;
                                }
                                else
                                {
                                    mr.TotalGroupPrice += mm1.TotalNetWeight * mm1.UnitPrice;
                                }
                            }
                        }
                    }

                    mrm.MonthlyRowList.Add(mr);
                }

                MonthlyRowMapList.Add(mrm);
                mrIndex++;
            }

            foreach(MonthlyRowMap mrr in MonthlyRowMapList)
            {
                mrr.TotalPrice = mrr.MonthlyRowList.Sum(o => o.TotalGroupPrice);
            }

            //BUILD ROW
            BuildRows(sheet, MonthlyRowMapList);

            BuildSummary(sheet, MonthlyRowMapList);
            return month.ToString("yyyyMM");
        }

        private void BuildSummary(ISheet sheet, List<MonthlyRowMap> MonthlyRowMapList)
        {
            int rIndex = 0;
            double totalShiZaiW = 0;
            int totalShiZaiCar = 0;
            double totalShiFengW = 0;
            int totalShiFengCar = 0;
            double totalTouPoW = 0;
            int totalTouPoCar = 0;
            double totalPrice = 0;
            foreach (MonthlyRowMap mrm in MonthlyRowMapList)
            {
                rIndex += mrm.MonthlyRowList.Count;
                foreach(MonthlyRow mr in mrm.MonthlyRowList)
                {
                    totalShiZaiW += mr.ShiZaiWeight;
                    totalShiZaiCar += mr.ShiZaiCarCount;
                    totalShiFengW += mr.ShiFengWeight;
                    totalShiFengCar += mr.ShiFengCarCount;
                    totalTouPoW += mr.TouPoShiWeight;
                    totalTouPoCar += mr.TouPoShiCarCount;
                    totalPrice += mr.TotalGroupPrice;
                }
            }

            IRow rr = sheet.CreateRow(rIndex + 3);
            CreateCellString(rr, "合计：", 0);
            CreateCellString(rr, "", 1);
            CreateCellDouble(rr, totalShiZaiW, 2);
            CreateCellDouble(rr, totalShiZaiCar, 3);
            CreateCellDouble(rr, totalShiFengW, 4);
            CreateCellDouble(rr, totalShiFengCar, 5);
            CreateCellDouble(rr, totalTouPoW, 6);
            CreateCellDouble(rr, totalTouPoCar, 7);
            CreateCellString(rr, "", 8);
            CreateCellString(rr, "", 9);
            CreateCellString(rr, "", 10);
            CreateCellDouble(rr, totalPrice, 11);
            CreateCellString(rr, "", 12);
            CreateCellString(rr, "", 13);
        }

        private void BuildRows(ISheet sheet,List<MonthlyRowMap> MonthlyRowMapList)
        {
            Dictionary<string, int> vendorMap = new Dictionary<string, int>();

            // create row
            int rrindex = 3;
            int rdx = 1;
            foreach(MonthlyRowMap mrm in MonthlyRowMapList)
            {
                foreach(MonthlyRow mr in mrm.MonthlyRowList)
                {
                    IRow rr = sheet.CreateRow(rrindex);
                    if (!vendorMap.ContainsKey(mrm.Vendor))
                    {
                        vendorMap.Add(mrm.Vendor, 1);

                        CreateCellDouble(rr, rdx, 0);//序号
                        CreateCellString(rr, mrm.Vendor, 1);//客户名称
                        CreateCellDouble(rr, mrm.TotalPrice, 12);//汇总金额
                        rdx++;
                    }
                    CreateCellDouble(rr, mr.ShiZaiWeight, 2);//石仔（吨数）
                    CreateCellDouble(rr, mr.ShiZaiCarCount, 3);//石仔车数
                    CreateCellDouble(rr, mr.ShiFengWeight, 4);//石粉（吨数）
                    CreateCellDouble(rr, mr.ShiFengCarCount, 5);//石粉车数
                    CreateCellDouble(rr, mr.TouPoShiWeight, 6);//头破石（吨数）
                    CreateCellDouble(rr, mr.TouPoShiCarCount, 7);//头破石车数
                    CreateCellDouble(rr, mr.ShiZaiUnitPrice, 8);//石仔单价
                    CreateCellDouble(rr, mr.ShiFengUnitPrice, 9);//石粉单价
                    CreateCellDouble(rr, mr.TouPoShiUnitPrice, 10);//头破石单价
                    CreateCellDouble(rr, mr.TotalGroupPrice, 11);//金额
                    CreateCellString(rr, "", 13);//备注

                    rrindex++;
                }
            }
            rrindex = 3;
            Dictionary<string, string> hasMerged = new Dictionary<string, string>();
            foreach (MonthlyRowMap mrm in MonthlyRowMapList)
            {
                foreach (MonthlyRow mr in mrm.MonthlyRowList)
                {
                    if (vendorMap.ContainsKey(mrm.Vendor))
                    {
                        if (mrm.MonthlyRowList.Count > 1)
                        {
                            if (!hasMerged.ContainsKey(mrm.Vendor))
                            {
                                hasMerged.Add(mrm.Vendor, "");
                                MearchCellFormat(sheet, rrindex, rrindex + mrm.MonthlyRowList.Count - 1, 0, 0);
                                MearchCellFormat(sheet, rrindex, rrindex + mrm.MonthlyRowList.Count - 1, 1, 1);
                                MearchCellFormat(sheet, rrindex, rrindex + mrm.MonthlyRowList.Count - 1, 12, 12);
                            }
                        }

                    }
                    rrindex++;
                }
            }



        }

        private void BuildFirstPageHeader(ISheet sheet,DateTime month)
        {
            //title
            string title = string.Format("{0:Y}", month) + "惠东县仙仔山石场出库核算表";
            CreateTitle(sheet, title,13);

            //sub title
            IRow row2 = sheet.CreateRow(1);
            IRow row3 = sheet.CreateRow(2);
            CreateMergeCellString("序号", sheet, row2, 1, 2, 0, 0);
            CreateMergeCellString("客户名称", sheet, row2, 1, 2, 1, 1);
            CreateMergeCellString("石仔（吨数）", sheet, row2, 1, 2, 2, 2);
            CreateMergeCellString("车数", sheet, row2, 1, 2, 3, 3);
            CreateMergeCellString("石粉（吨数）", sheet, row2, 1, 2, 4, 4);
            CreateMergeCellString("车数", sheet, row2, 1, 2, 5, 5);
            CreateMergeCellString("头破石", sheet, row2, 1, 2, 6, 6);
            CreateMergeCellString("车数", sheet, row2, 1, 2, 7, 7);
            CreateMergeCellString("单价(吨)", sheet, row2, 1, 1, 8, 10);
            CreateCellString(row3, "石仔", 8);
            CreateCellString(row3, "石粉", 9);
            CreateCellString(row3, "头破石", 10);
            CreateMergeCellString("金额", sheet, row2, 1, 2, 11, 11);
            CreateMergeCellString("汇总金额", sheet, row2, 1, 2, 12, 12);
            CreateMergeCellString("备注", sheet, row2, 1, 2, 13, 13);

            sheet.SetColumnWidth(0, 6 * 256);
            sheet.SetColumnWidth(1, 15 * 256);
            sheet.SetColumnWidth(2, 15 * 256);
            sheet.SetColumnWidth(3, 8 * 256);
            sheet.SetColumnWidth(4, 15 * 256);
            sheet.SetColumnWidth(5, 8 * 256);
            sheet.SetColumnWidth(6, 8 * 256);
            sheet.SetColumnWidth(7, 8 * 256);
            sheet.SetColumnWidth(8, 8 * 256);
            sheet.SetColumnWidth(9, 8 * 256);
            sheet.SetColumnWidth(10, 8 * 256);
            sheet.SetColumnWidth(11, 15 * 256);
            sheet.SetColumnWidth(12, 15 * 256);
        }

        private void CreateShiFengRow(MonthlyModel mm, ISheet sheet)
        {
            IRow rr = sheet.CreateRow(rowIndex);
            CreateCellString(rr,mm.Vendor, 1);//客户名称
            CreateCellDouble(rr,mm.TotalNetWeight, 4);//石粉（吨数）
            CreateCellDouble(rr,mm.CarCount, 5); //车数
            CreateCellDouble(rr,mm.UnitPrice, 9); //石粉单价

            rowIndex++;
        }

        private void CreateTouPoShiRow(MonthlyModel mm, ISheet sheet)
        {
            IRow rr = sheet.CreateRow(rowIndex);
            CreateCellString(rr, mm.Vendor, 1);//客户名称
            CreateCellDouble(rr, mm.TotalNetWeight, 6);//头破石（吨数）
            CreateCellDouble(rr, mm.CarCount, 7); //车数
            CreateCellDouble(rr, mm.UnitPrice, 10); //头破石单价

            rowIndex++;
        }

        private void CreateShiZaiRow(MonthlyModel mm, ISheet sheet)
        {
            IRow rr = sheet.CreateRow(rowIndex);
            CreateCellString(rr, mm.Vendor, 1);//客户名称
            CreateCellDouble(rr, mm.TotalNetWeight, 2);//石仔（吨数）
            CreateCellDouble(rr, mm.CarCount, 3); //车数
            CreateCellDouble(rr, mm.UnitPrice, 8); //石仔单价

            rowIndex++;
        }

        private List<EXCEL_IMPORT_DAILY> GetData(DateTime month)
        {
            DateTime nextMonth = month.AddMonths(1);
            DateTime d1 = new DateTime(nextMonth.Year, nextMonth.Month, 1);
            List<EXCEL_IMPORT_DAILY> ddList = new List<EXCEL_IMPORT_DAILY>();
            using (var db = new OracleDataAccess())
            {
                ddList = db.GetItems<EXCEL_IMPORT_DAILY>(o => o.REPORT_DATE>= month && o.REPORT_DATE< d1);
            }

            foreach(EXCEL_IMPORT_DAILY d in ddList)
            {
                if (d.TAKE_DEPT.StartsWith("成协"))
                {
                    d.TAKE_DEPT = "成协";
                }
            }


            return ddList;
        }
    }
}
