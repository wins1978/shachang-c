using ExcelPro;
using ExeMgrLib.Model;
using NPOI.SS.UserModel;
using NPOI.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tencent.OA.ATQ.DataAccess;

namespace ExeMgrLib
{
    public class AllDetailReport: SheetBase
    {
        public string Run(DateTime startDate, DateTime endDate)
        {
            string path = "";
            InitializeWorkbook();

            List<EXCEL_IMPORT_DAILY_V2> datas = GetAllDetails(startDate, endDate);
            if (datas.Count < 1) return "";
            CombineData(datas);

            List<string> vendors = datas.Select(o => o.TAKE_DEPT).Distinct().ToList();

            foreach(string vendor in vendors)
            {
                CreatePage(vendor, datas);
            }

            path = "仙仔山" + startDate.Year.ToString() + "年" + startDate.Month.ToString() + "月明细";

            WriteToFile(path);

            return path+".xls";
        }

        private void CombineData(List<EXCEL_IMPORT_DAILY_V2> datas)
        {
            foreach(EXCEL_IMPORT_DAILY_V2 v in datas)
            {
                if (v.TAKE_DEPT.StartsWith("成协"))
                {
                    v.GUID_KEY = v.TAKE_DEPT;
                    v.TAKE_DEPT = "成协";
                }
            }
        }

        private void CreatePage(string vendor, List<EXCEL_IMPORT_DAILY_V2> datas)
        {
            List<EXCEL_IMPORT_DAILY_V2> sources = datas.Where(o => o.TAKE_DEPT == vendor).ToList();
            ISheet sheet = hssfworkbook.CreateSheet(vendor);

            var dataDetails = (from a in sources
                               group a by new { a.TAKE_DEPT, a.CAR_NUMBER, a.SHOP_NUMBER,a.UNIT_PRICE,a.DATE_STR,a.NUMBER_FLAG } into b
                               select new
                               {
                                   Vendor = b.Key.TAKE_DEPT,
                                   SubVendor = b.Min(c=>c.GUID_KEY),
                                   DateStr = b.Key.DATE_STR,
                                   ShopNumber = b.Key.SHOP_NUMBER,
                                   CarNumber = b.Key.CAR_NUMBER,
                                   UnitPrice = b.Key.UNIT_PRICE.Value,
                                   CarCount = b.Count(),
                                   NumberFlag=b.Key.NUMBER_FLAG,
                                   TotalPrice = Math.Round(b.Sum(c => c.TOTAL_PRICE.Value), 2),
                                   TotalNetWeight = Math.Round(b.Sum(c => double.Parse(c.NET_WEIGHT) / 1000), 2)
                               });

            List<DetailLineModel> lines = new List<DetailLineModel>();
            foreach(var item in dataDetails)
            {
                DetailLineModel dm = new DetailLineModel();
                dm.Vendor = item.Vendor;
                dm.CarCount = item.CarCount;
                dm.SubVendor = item.SubVendor;
                dm.DateStr = item.DateStr;
                dm.TotalPrice = item.TotalPrice;
                dm.UnitPrice = item.UnitPrice;
                dm.CarNumber = item.CarNumber;
                dm.NumberFlag = item.NumberFlag;
                if (item.ShopNumber == "0-5")
                {
                    dm.Weight05 = item.TotalNetWeight;
                }else if(item.ShopNumber == "1-2")
                {
                    dm.Weight12 = item.TotalNetWeight;
                }
                else if (item.ShopNumber == "1-3")
                {
                    dm.Weight13 = item.TotalNetWeight;
                }
                else if (item.ShopNumber == "2-4")
                {
                    dm.Weight24 = item.TotalNetWeight;
                }
                else if (item.ShopNumber == "头破石")
                {
                    dm.WeightTouposhi = item.TotalNetWeight;
                }
                else if (item.ShopNumber == "石粉")
                {
                    dm.WeightShifeng = item.TotalNetWeight;
                    
                }
                lines.Add(dm);
            }

            CreateRow(lines,sheet);
        }

        private void CreateRow(List<DetailLineModel> lines, ISheet sheet)
        {
            string vendor = lines[0].Vendor;
            CreateTitle(sheet, vendor, 2);
            IRow r1 = sheet.GetRow(0);
            CreateMergeCellString("", sheet, r1, 0, 0, 3, 18);

            if (vendor != "成协")
            {
                sheet.SetColumnHidden(1, true);
            }

            IRow r2 = sheet.CreateRow(1);
            IRow r3 = sheet.CreateRow(2);
            CreateMergeCellString("序号", sheet, r2, 1, 2, 0, 0);
            CreateMergeCellString("客户名称", sheet, r2, 1, 2, 1, 1);
            CreateMergeCellString("日期", sheet, r2, 1, 2, 2, 2);
            CreateMergeCellString("车牌", sheet, r2, 1, 2, 3, 3);
            CreateMergeCellString("磅单号", sheet, r2, 1, 2, 4, 4);
            CreateMergeCellString("单据编号", sheet, r2, 1, 2, 5, 5);
            CreateMergeCellString("石料型号", sheet, r2, 1, 1, 6, 11);
            CreateCellString(r3, "0-5", 6);
            CreateCellString(r3, "1-2", 7);
            CreateCellString(r3, "1-3", 8);
            CreateCellString(r3, "2-4", 9);
            CreateCellString(r3, "头破石", 10);
            CreateCellString(r3, "石粉", 11);
            CreateMergeCellString("单价", sheet, r2, 1, 2, 12, 12);
            CreateMergeCellString("车数", sheet, r2, 1, 2, 13, 13);
            CreateMergeCellString("金额", sheet, r2, 1, 2, 14, 14);
            CreateMergeCellString("工地名称", sheet, r2, 1, 2, 15, 15);
            CreateCellString(r2, "上月余额", 16);
            CreateCellString(r2, "本月收款", 17);
            CreateCellString(r2, "本月余额", 18);

            EXCEL_VENDOR_COST_CUR_V vcc = null;
            if(vendor !="零售" && vendor != "成协")
            {
                using(var db=new OracleDataAccess())
                {
                    vcc = db.GetSingleItem<EXCEL_VENDOR_COST_CUR_V>(o => o.VENDOR == vendor);
                }
            }
            if(vcc != null)
            {
                if (vcc.LAST_MON_REMAIN.HasValue)
                {
                    CreateCellDouble(r3, vcc.LAST_MON_REMAIN.Value, 16);
                }
                else
                {
                    CreateCellString(r3, "", 16);
                }
                if (vcc.CUR_MON_INCOME.HasValue)
                {
                    CreateCellDouble(r3, vcc.CUR_MON_INCOME.Value, 17);
                }
                else
                {
                    CreateCellString(r3, "", 17);
                }
                if (vcc.CUR_MON_REMAIN.HasValue)
                {
                    double alert = 0;
                    if (vcc.COST_ALERT.HasValue)
                    {
                        alert = vcc.COST_ALERT.Value;
                    }
                    if (vcc.CUR_MON_REMAIN.Value < alert)
                    {
                        CreateCellDoubleWithColor(r3, vcc.CUR_MON_REMAIN.Value, 18);
                    }
                    else
                    {
                        CreateCellDouble(r3, vcc.CUR_MON_REMAIN.Value, 18);
                    }
                }
                else
                {
                    CreateCellString(r3, "", 18);
                }
            }


            int ridx = 3;
            List<DetailLineModel> linesO = lines.OrderBy(o => o.DateStr).ThenBy(o => o.CarNumber).ToList();
            if(vendor == "成协")
            {
                linesO = lines.OrderBy(o => o.DateStr).ThenBy(o => o.SubVendor).ThenBy(o => o.CarNumber).ToList();
            }

            foreach (DetailLineModel dlm in linesO)
            {
                IRow rr = sheet.CreateRow(ridx);
                CreateCellDouble(rr, ridx - 2, 0);
                if (dlm.Vendor == "成协")
                {
                    CreateCellString(rr, dlm.SubVendor, 1);
                }
                else
                {
                    CreateCellString(rr, dlm.Vendor, 1);
                }
                
                string date2 = ConverDateStr(dlm.DateStr);
                CreateCellString(rr, date2, 2);
                CreateCellString(rr, dlm.CarNumber, 3);
                CreateCellString(rr, dlm.NumberFlag, 4);
                CreateCellString(rr, "", 5);
                if (dlm.Weight05 < 0.1)
                {
                    CreateCellString(rr, "", 6);
                }
                else
                {
                    CreateCellDouble(rr, dlm.Weight05, 6);
                }
                if (dlm.Weight12 < 0.1)
                {
                    CreateCellString(rr, "", 7);
                }
                else
                {
                    CreateCellDouble(rr, dlm.Weight12, 7);
                }
                if (dlm.Weight13 < 0.1)
                {
                    CreateCellString(rr, "", 8);
                }
                else
                {
                    CreateCellDouble(rr, dlm.Weight13, 8);
             
                }

                if (dlm.Weight24 < 0.1)
                {
                    CreateCellString(rr, "", 9);
                }
                else
                {
                    CreateCellDouble(rr, dlm.Weight24, 9);
            
                }
                if (dlm.WeightTouposhi < 0.1)
                {
                    CreateCellString(rr, "", 10);
                }
                else
                {
                    CreateCellDouble(rr, dlm.WeightTouposhi, 10);
                 
                }
                if (dlm.WeightShifeng < 0.1)
                {
                    CreateCellString(rr, "", 11);
                }
                else
                {
                    CreateCellDouble(rr, dlm.WeightShifeng, 11);
                   
                }
                CreateCellDouble(rr, dlm.UnitPrice, 12);
                CreateCellDouble(rr, dlm.CarCount, 13);
                if (dlm.Vendor == "零售")
                {
                    double p1 = GetMoneyRoundD(dlm.TotalPrice);
                    CreateCellDouble(rr, p1, 14);
                }
                else
                {
                    CreateCellDouble(rr, dlm.TotalPrice, 14);
                }
                
                CreateCellString(rr, "", 15);
                ridx++;
            }

            //
            IRow rra = sheet.CreateRow(ridx);
            CreateMergeCellString("合计：", sheet, rra, ridx, ridx, 0, 2);
            CreateCellString(rra, "", 3);
            CreateCellString(rra, "", 4);
            CreateCellString(rra, "", 5);
            CreateSumFormula(rra, 6, "G4", "G" + (ridx).ToString());
            CreateSumFormula(rra, 7, "H4", "H" + (ridx).ToString());
            CreateSumFormula(rra, 8, "I4", "I" + (ridx).ToString());
            CreateSumFormula(rra, 9, "J4", "J" + (ridx).ToString());
            CreateSumFormula(rra, 10, "K4", "K" + (ridx).ToString());
            CreateSumFormula(rra, 11, "L4", "L" + (ridx).ToString());
            CreateCellString(rra, "", 12);
            CreateSumFormula(rra, 13, "N4", "N" + (ridx).ToString());
            CreateSumFormula(rra, 14, "O4", "O" + (ridx).ToString());
            CreateCellString(rra, "", 15);

            SetWidth(sheet, 1, 14);
            SetWidth(sheet, 2, 14);
            SetWidth(sheet, 3, 8);
            SetWidth(sheet, 4, 15);
            SetWidth(sheet, 14, 14);
            SetWidth(sheet, 15, 14);
            SetWidth(sheet, 16, 14);
            SetWidth(sheet, 17, 14);
            SetWidth(sheet, 18, 15);
        }



        private List<EXCEL_IMPORT_DAILY_V2> GetAllDetails(DateTime startDate, DateTime endDate)
        {
            List<EXCEL_IMPORT_DAILY_V2> datas = new List<EXCEL_IMPORT_DAILY_V2>();
            using (var db =new OracleDataAccess())
            {
                datas = db.GetItems<EXCEL_IMPORT_DAILY_V2>(o => o.REPORT_DATE >= startDate && o.REPORT_DATE <= endDate);
            }

            return datas;
        }

       
    }
}
