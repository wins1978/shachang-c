using ExcelPro;
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

namespace ExeMgrLib
{
    public class CustomReport: SheetBase
    {
        public string Run(List<EXCEL_IMPORT_DAILY_V2> datas,DateTime startDate,DateTime endDate,string searchType)
        {
            InitializeWorkbook();
            ConverMoneyRound(datas);
            datas = datas.OrderBy(o => o.TAKE_DEPT).ThenBy(o=>o.SHOP_NUMBER).ToList();

           CreeateFirstPage(datas,startDate, endDate, searchType);//summary
            CreeateSecondPage(datas, startDate, endDate);

            string filename = datas[0].TAKE_DEPT + "_" + startDate.ToString("yyyyMMdd") +"_"+ endDate.ToString("yyyyMMdd")+ "销售报表";
            WriteToFile(filename);
            return filename +".xls";
        }

        private void ConverMoneyRound(List<EXCEL_IMPORT_DAILY_V2> datas )
        {
            foreach(EXCEL_IMPORT_DAILY_V2 v2 in datas)
            {
                if (v2.TAKE_DEPT == "零售")
                {
                    double price = v2.TOTAL_PRICE.Value;
                    v2.TOTAL_PRICE = GetMoneyRound(price);
                }
            }
        }
        private void CreeateFirstPage(List<EXCEL_IMPORT_DAILY_V2> datas,DateTime startDate,DateTime endDate,string searchType)
        {
            ISheet sheet = hssfworkbook.CreateSheet("汇总");
            
            //title
            CreateTitle(sheet, "汇总", 7);

            string firdayStr = startDate.GetDateTimeFormats('D')[0].ToString();
            string lastDayStr = endDate.GetDateTimeFormats('D')[0].ToString();
            string title = firdayStr + "-" + lastDayStr;
            CreateSubTitle(sheet, title, 7);

            //title2
            IRow row2 = sheet.CreateRow(2);
            CreateCellString(row2, "客户名称", 0);
            CreateCellString(row2, "石料类型", 1);
            CreateCellString(row2, "石料名称", 2);
            CreateCellString(row2, "车牌号码", 3);
            CreateCellString(row2, "车数", 4);
            CreateCellString(row2, "净重(吨)", 5);
            CreateCellString(row2, "均价(吨)", 6);
            CreateCellString(row2, "总价", 7);
            //======================================
            if (searchType == "shop_number")
            {
                SearchByShopNumber(datas, sheet);
            }
            else if(searchType == "car_number")
            {
                SearchByCarNumber(datas, sheet);
            }
            else if(searchType == "car_shop")
            {
                SearchByCarShopNumber(datas, sheet);
            }
            else
            {
                throw new Exception("没有找到SearchType类型");
            }
            
            sheet.SetColumnWidth(0, 19 * 256);
            sheet.SetColumnWidth(4, 16 * 256);
           
        }


        private void CreeateSecondPage(List<EXCEL_IMPORT_DAILY_V2> datas, DateTime startDate, DateTime endDate)
        {
            ISheet sheet = hssfworkbook.CreateSheet("明细");
            List<EXCEL_IMPORT_DAILY_V2> datas2 = datas.OrderByDescending(o => o.REPORT_DATE).ToList();

            IRow rr1 = sheet.CreateRow(0);
            CreateCellString(rr1, "收货单位", 0);
            CreateCellString(rr1, "日期", 1);
            CreateCellString(rr1, "车号", 2);
            CreateCellString(rr1, "货物名称", 3);
            CreateCellString(rr1, "毛重(吨)", 4);
            CreateCellString(rr1, "皮重(吨)", 5);
            CreateCellString(rr1, "净重(吨)", 6);
            CreateCellString(rr1, "单价", 7);
            CreateCellString(rr1, "磅单号", 8);
            CreateCellString(rr1, "金额", 9);

            double tPrice = 0;
            int rIndex = 1;
            foreach (var item in datas2)
            {
                //TAKE_DEPT 收货单位
                //DATE_STR
                //CAR_NUMBER 车号
                //SHOP_NUMBER 货号
                //GROSS_WEIGHT 毛重
                //TARE_WEIGHT 皮重
                //NET_WEIGHT 净重
                //UNIT_PRICE
                IRow rr = sheet.CreateRow(rIndex);
                CreateCellString(rr, item.TAKE_DEPT, 0);
                CreateCellString(rr, item.DATE_STR, 1);
                CreateCellString(rr, item.CAR_NUMBER, 2);
                CreateCellString(rr, item.SHOP_NUMBER, 3);
                CreateCellDouble(rr, Math.Round(Double.Parse(item.GROSS_WEIGHT) / 1000, 2), 4);
                CreateCellDouble(rr, Math.Round(Double.Parse(item.TARE_WEIGHT) / 1000, 2), 5);
                CreateCellDouble(rr, Math.Round(Double.Parse(item.NET_WEIGHT) / 1000, 2), 6);

                

                if (item.UNIT_PRICE.HasValue)
                {
                    CreateCellDouble(rr, item.UNIT_PRICE.Value, 7);
                    double totalPrice = Math.Round((Double.Parse(item.NET_WEIGHT) / 1000) * item.UNIT_PRICE.Value, 2);
                    CreateCellDouble(rr, totalPrice, 9);
                    tPrice += totalPrice;
                }
                else
                {
                    CreateCellString(rr, "", 7);
                    CreateCellString(rr, "", 9);
                }

                CreateCellString(rr, item.NUMBER_FLAG, 8);
                
                rIndex++;
            }

            IRow rr2 = sheet.CreateRow(rIndex);
            CreateCellString(rr2, "合计：", 0);
            CreateCellString(rr2, "", 1);
            CreateCellString(rr2, "", 2);
            CreateCellString(rr2, "", 3);
            CreateCellString(rr2, "", 4);
            CreateCellString(rr2, "", 5);
            CreateCellString(rr2, "", 6);
            CreateCellString(rr2, "", 7);
            CreateCellString(rr2, "", 8);
            CreateCellDouble(rr2, tPrice, 9);

            sheet.SetColumnWidth(0, 19 * 256);
            sheet.SetColumnWidth(1, 16 * 256);
            sheet.SetColumnWidth(8, 16 * 256);
        }

        #region SearchType


        private void SearchByCarShopNumber(List<EXCEL_IMPORT_DAILY_V2> datas, ISheet sheet)
        {
            var dataDetails = (from a in datas
                               group a by new { a.TAKE_DEPT, a.CAR_NUMBER, a.SHOP_NUMBER } into b
                               select new
                               {
                                   TAKE_DEPT = b.Key.TAKE_DEPT,
                                   SHOP_NUMBER = b.Key.SHOP_NUMBER,
                                   GROUP_NAME = b.Key.SHOP_NUMBER == "石粉" ? "石粉" : "石仔",
                                   CAR_NUMBER = b.Key.CAR_NUMBER,
                                   CarCount = b.Count(),
                                   UNIT_PRICE = Math.Round(b.Average(c => c.UNIT_PRICE.Value), 2),
                                   TotalPrice = Math.Round(b.Sum(c => c.TOTAL_PRICE.Value), 2),
                                   TotalNetWeight = Math.Round(b.Sum(c => double.Parse(c.NET_WEIGHT) / 1000), 2)
                               }).OrderBy(o => o.CAR_NUMBER).ThenBy(o => o.SHOP_NUMBER);

            int rrIndex = 3;
            foreach (var item in dataDetails)
            {
                IRow rr = sheet.CreateRow(rrIndex);
                CreateCellString(rr, item.TAKE_DEPT, 0);
                CreateCellString(rr, item.GROUP_NAME, 1);
                CreateCellString(rr, item.SHOP_NUMBER, 2);
                CreateCellString(rr, item.CAR_NUMBER, 3);
                CreateCellDouble(rr, item.CarCount, 4);
                CreateCellDouble(rr, item.TotalNetWeight, 5);
                CreateCellDouble(rr, item.UNIT_PRICE, 6);
                CreateCellDouble(rr, item.TotalPrice, 7);
                rrIndex++;
            }

        }

        private void SearchByCarNumber(List<EXCEL_IMPORT_DAILY_V2> datas, ISheet sheet)
        {
            var dataDetails = (from a in datas
                               group a by new { a.TAKE_DEPT, a.CAR_NUMBER, a.GROUP_NAME } into b
                               select new
                               {
                                   TAKE_DEPT = b.Key.TAKE_DEPT,
                                   SHOP_NUMBER = "",
                                   GROUP_NAME = b.Key.GROUP_NAME,
                                   CAR_NUMBER = b.Key.CAR_NUMBER,
                                   CarCount = b.Count(),
                                   UNIT_PRICE = Math.Round(b.Average(c => c.UNIT_PRICE.Value), 2),
                                   TotalPrice = Math.Round(b.Sum(c => c.TOTAL_PRICE.Value), 2),
                                   TotalNetWeight = Math.Round(b.Sum(c => double.Parse(c.NET_WEIGHT) / 1000), 2)
                               }).OrderBy(o => o.CAR_NUMBER).ThenBy(o => o.GROUP_NAME);
            int rrIndex = 3;
            foreach (var item in dataDetails)
            {
                IRow rr = sheet.CreateRow(rrIndex);
                CreateCellString(rr, item.TAKE_DEPT, 0);
                CreateCellString(rr, item.GROUP_NAME, 1);
                CreateCellString(rr, item.SHOP_NUMBER, 2);
                CreateCellString(rr, item.CAR_NUMBER, 3);
                CreateCellDouble(rr, item.CarCount, 4);
                CreateCellDouble(rr, item.TotalNetWeight, 5);
                CreateCellDouble(rr, item.UNIT_PRICE, 6);
                CreateCellDouble(rr, item.TotalPrice, 7);
                rrIndex++;
            }
        }

        private void SearchByShopNumber(List<EXCEL_IMPORT_DAILY_V2> datas, ISheet sheet)
        {
            var dataDetails = (from a in datas
                               group a by new { a.TAKE_DEPT, a.SHOP_NUMBER } into b
                               select new
                               {
                                   TAKE_DEPT = b.Key.TAKE_DEPT,
                                   SHOP_NUMBER = b.Key.SHOP_NUMBER,
                                   GROUP_NAME = b.Key.SHOP_NUMBER == "石粉" ? "石粉" :  "石仔",
                                   CAR_NUMBER = "",
                                   CarCount = b.Count(),
                                   UNIT_PRICE = Math.Round(b.Average(c => c.UNIT_PRICE.Value), 2),
                                   TotalPrice = Math.Round(b.Sum(c => c.TOTAL_PRICE.Value), 2),
                                   TotalNetWeight = Math.Round(b.Sum(c => double.Parse(c.NET_WEIGHT) / 1000), 2)
                               }).OrderBy(o => o.SHOP_NUMBER);

            int rrIndex = 3;
            foreach (var item in dataDetails)
            {
                IRow rr = sheet.CreateRow(rrIndex);
                CreateCellString(rr, item.TAKE_DEPT, 0);
                CreateCellString(rr, item.GROUP_NAME, 1);
                CreateCellString(rr, item.SHOP_NUMBER, 2);
                CreateCellString(rr, item.CAR_NUMBER, 3);
                CreateCellDouble(rr, item.CarCount, 4);
                CreateCellDouble(rr, item.TotalNetWeight, 5);
                CreateCellDouble(rr, item.UNIT_PRICE, 6);
                CreateCellDouble(rr, item.TotalPrice, 7);
                rrIndex++;
            }
        }



        #endregion
    }
}
