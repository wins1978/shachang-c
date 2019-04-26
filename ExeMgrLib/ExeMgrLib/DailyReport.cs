using ExcelPro;
using ExeMgrLib.Model;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using Stock.Utils;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tencent.OA.ATQ.DataAccess;

namespace ExeMgrLib
{
    public class DailyReport: SheetBase
    {
        public void DeleteData(string[] datestrArr)
        {
            using (var db = new OracleDataAccess())
            {
                var datas = db.GetItems<EXCEL_IMPORT_DAILY>(o => datestrArr.Contains(o.IMPORT_DATE_STR));
                if(datas != null && datas.Count > 0)
                {
                    db.DeleteItems(datas);
                }
            }
        }

        public void InsertData(List<InputInfo> items,string guid,string filename)
        {
            Lg.Init("mylog");

            IFormatProvider ifp = new CultureInfo("zh-CN", true);

            List<EXCEL_IMPORT_DAILY> tempDataList = new List<EXCEL_IMPORT_DAILY>();
            foreach(InputInfo item in items)
            {
                //Lg.Error("-----------d1-----" + item.Date);
                item.Date = item.Date.Substring(0, 8);
                string date2 = item.Date.Substring(0, 4) + "-" + item.Date.Substring(4, 2) + "-" + item.Date.Substring(6, 2);
                EXCEL_IMPORT_DAILY ite = new EXCEL_IMPORT_DAILY();
                ite.CAR_NUMBER = item.CarNumber;
                ite.DATE_STR = item.Date;
                ite.TIME_STR = item.Time;
                //Lg.Error("-----------t1-----" + ite.TIME_STR);
                string t1 = ite.TIME_STR.Split(' ')[1];
                
                if (t1.Length == 7) // 2:12:12
                {
                    t1 = "0" + t1;
                }
                else if (t1.Length == 5) //12:12
                {
                    t1 = t1 + ":00";
                }
                else if (t1.Length == 4) // 3:12
                {
                    t1 = "0" + t1 + ":00";
                }
                    string str = item.Date + t1;
                //Lg.Error("----------------" + str);
                ite.CREATE_DATETIME = DateTime.ParseExact(str, "yyyyMMddHH:mm:ss", ifp);
                

                ite.REPORT_DATE = DateTime.Parse(date2);
                ite.DRIVER_NAME = item.DriverName;
                ite.FILE_NAME = filename;
                ite.GROSS_WEIGHT = item.GrossWeight;
                ite.GROUP_NAME = item.GroupName;
                ite.GUID_KEY = guid;
                ite.IMPORT_DATE = DateTime.Now;
                ite.IMPORT_DATE_STR = item.Date;
                ite.NET_WEIGHT = item.NetWeight;
                ite.NUMBER_FLAG = item.NumberFlag;
                ite.SEND_OUT_DEPT = item.SendOutDept;
                ite.SHOP_NUMBER = item.ShopNumber;
                ite.TAKE_DEPT = item.TakeDept;
                ite.TARE_WEIGHT = item.TareWeight;
                
                ite.VENDOR_NET_WEIGHT = item.VendorNetWeight;
                if (!String.IsNullOrEmpty(item.UnitPrice))
                {
                    ite.UNIT_PRICE = Int32.Parse(item.UnitPrice);
                }
                
                //ite.ROW_INDEX = Int32.Parse(item.RowIndex);
                tempDataList.Add(ite);
            }
            using (var db = new OracleDataAccess())
            {
                db.InsertItems(tempDataList);
            }
        }
        string GetDateString(string[] datestrArr)
        {
            string d = "";
            foreach (string s in datestrArr)
            {
                d += s + "-";
            }
            d = d.TrimEnd('-');

            return d;
        }

        // 创建微信日报
        public string Run(string fileFullPath, List<InputInfo> items)
        {
            string status = "";
            InitWorkbook(fileFullPath);
            ISheet sheet1 = hssfworkbook.GetSheet("Sheet1");
            
            string[] datestrArr = items.Select(o => o.Date).Distinct().ToArray();
            string datestr = GetDateString(datestrArr);
            string guid = Guid.NewGuid().ToString();
            string date1 = datestr.Substring(0, 4) + "年" + datestr.Substring(4, 2) + "月" + datestr.Substring(6, 2) + "日";
            string outFileName = "信日报"+date1 + ".xls";
            string date2 = datestr.Substring(0, 4) + "-" + datestr.Substring(4, 2) + "-" + datestr.Substring(6, 2);
            DateTime today = DateTime.Parse(date2);
            foreach (var item in items)
            {
                if (item.ShopNumber == "石粉")
                {
                    item.GroupName = "石粉";
                }
                else
                {
                    item.GroupName = "石仔";
                }
            }

            //删除临时数据
            DeleteData(datestrArr);

            string chk = "";
            using (var db = new OracleDataAccess())
            {
                foreach (InputInfo item in items)
                {
                    var t1 = db.GetSingleItem<EXCEL_IMPORT_DAILY>(o => o.NUMBER_FLAG == item.NumberFlag);
                    if (t1 != null)
                    {
                        chk = "错误:" + item.NumberFlag + "已经存在";
                        break;
                    }
                }
            }
            if (chk != "")
            {
                return chk;
            }

            //插入临时数据
            InsertData(items,guid,outFileName);
            //获取包括价格在内的视图
            List<EXCEL_IMPORT_DAILY_V3> dataViews = new List<EXCEL_IMPORT_DAILY_V3>();
            // 昨日早上8：00到今日早上8：00
            string date3 = date2 + " 08:00:00";

            DateTime today1 = DateTime.Parse(date3);
            DateTime yesterday = today1.AddHours(-24);

            using (var db = new OracleDataAccess())
            {
                //dataViews = db.GetItems<EXCEL_IMPORT_DAILY_V3>(o => datestrArr.Contains( o.IMPORT_DATE_STR));
                dataViews = db.GetItems<EXCEL_IMPORT_DAILY_V3>(o => o.CREATE_DATETIME >=yesterday && o.CREATE_DATETIME <today1);
            }
            foreach(EXCEL_IMPORT_DAILY_V3 v in dataViews)
            {
                if (v.TAKE_DEPT.StartsWith("成协"))
                {
                    v.TAKE_DEPT = "成协";
                    v.VG_VENDOR = "成协";
                    v.V_VENDOR = "成协";
                }
            }

            dataViews = dataViews.OrderBy(o => o.TAKE_DEPT).ToList();



            //校验数据
            foreach (EXCEL_IMPORT_DAILY_V3 ite in dataViews)
            {
                //判断商品是否为空
                if (String.IsNullOrEmpty(ite.G_GOODS_NAME))
                {
                    status = "部分数据商品为空或未定义";
                }
                if (String.IsNullOrEmpty(ite.V_VENDOR))
                {
                    status = "部分数据客户为空或未定义";
                }
                if (String.IsNullOrEmpty(ite.VG_VENDOR))
                {
                    status = "部分数据客户-价格关系为空或未定义";
                }
                if (String.IsNullOrEmpty(ite.VG_VENDOR))
                {
                    status = "部分数据商品-价格关系为空或未定义";
                }
                if (!ite.UNIT_PRICE.HasValue || ite.UNIT_PRICE.Value <1)
                {
                    status = "部分数据价格关系为空或未定义";
                }
            }
            if(status != "")
            {
                return status;
            }
            foreach (InputInfo item in items)
            {
                if (item.TakeDept.StartsWith("成协"))
                {
                    item.TakeDept = "成协";
                }
            }

                var dataSummary = (from a in dataViews
                                   group a by new { a.TAKE_DEPT, a.SHOP_NUMBER } into b
                        select new
                        {
                            TakeDept = b.Key.TAKE_DEPT,
                            ShopNumber = b.Key.SHOP_NUMBER,
                            Count = b.Count(),
                            TotalNetWeight = b.Sum(c => Int32.Parse(c.NET_WEIGHT))
                        });

            
            var dataDetails = (from a in dataViews
                               group a by new { a.VG_VENDOR, a.GROUP_NAME,a.UNIT_PRICE } into b
                        select new
                        {
                            TakeDept = b.Key.VG_VENDOR,
                            GroupName = b.Key.GROUP_NAME,
                            Count = b.Count(),
                            PRICE = b.Key.UNIT_PRICE.Value,
                            TotalNetWeight = b.Sum(c => Int32.Parse(c.NET_WEIGHT))
                        });



            int rowIndex = 9;
            double totalprice = 0;
            double curPrice = 0;//现金
            double shiFeng = 0;//石粉（吨）
            double shiZai = 0;//石仔
            ICellStyle cs2 = sheet1.GetRow(8).GetCell(1).CellStyle;
            foreach (var item in dataDetails)
            {
                sheet1.CreateRow(rowIndex);
                sheet1.GetRow(rowIndex).CreateCell(0);
                sheet1.GetRow(rowIndex).CreateCell(1);
                sheet1.GetRow(rowIndex).CreateCell(2);
                sheet1.GetRow(rowIndex).CreateCell(3);
                sheet1.GetRow(rowIndex).CreateCell(4);
                sheet1.GetRow(rowIndex).CreateCell(5);

                sheet1.GetRow(rowIndex).GetCell(0).SetCellValue(item.TakeDept);
                string cell1 = String.Format("{0}{1}车", item.GroupName,item.Count);
                sheet1.GetRow(rowIndex).GetCell(1).SetCellValue(cell1);
                
                double cell2 = Math.Round(item.TotalNetWeight / 1000d, 2);
                sheet1.GetRow(rowIndex).GetCell(2).SetCellValue(cell2);
                
                sheet1.GetRow(rowIndex).GetCell(0).CellStyle = cs2;
                sheet1.GetRow(rowIndex).GetCell(1).CellStyle = cs2;
                sheet1.GetRow(rowIndex).GetCell(2).CellStyle = cs2;
                sheet1.GetRow(rowIndex).GetCell(3).CellStyle = cs2;
                sheet1.GetRow(rowIndex).GetCell(4).CellStyle = cs2;
                sheet1.GetRow(rowIndex).GetCell(5).CellStyle = cs2;

                //石粉吨
                if (item.GroupName == "石粉")
                {
                    shiFeng += cell2;
                }
                else
                {
                    shiZai += cell2;
                }

                double price = (double)item.PRICE;
                sheet1.GetRow(rowIndex).GetCell(3).SetCellValue(price);

                double total = price * cell2;
                if (item.TakeDept == "零售")
                {
                    int total1 = GetMoneyRound(total);
                    total = Double.Parse(total1.ToString());
                    curPrice += total;

                }
                sheet1.GetRow(rowIndex).GetCell(4).SetCellValue(total);

                totalprice += total;
                rowIndex++;
            }
            sheet1.CreateRow(rowIndex);
            sheet1.GetRow(rowIndex).CreateCell(0);
            sheet1.GetRow(rowIndex).CreateCell(1);
            sheet1.GetRow(rowIndex).CreateCell(2);
            sheet1.GetRow(rowIndex).CreateCell(3);
            sheet1.GetRow(rowIndex).CreateCell(4);
            sheet1.GetRow(rowIndex).CreateCell(5);
            sheet1.GetRow(rowIndex).GetCell(0).SetCellValue("合计:");
            sheet1.GetRow(rowIndex).GetCell(4).SetCellValue(totalprice);

           
            sheet1.GetRow(rowIndex).GetCell(0).CellStyle = cs2;
            sheet1.GetRow(rowIndex).GetCell(1).CellStyle = cs2;
            sheet1.GetRow(rowIndex).GetCell(2).CellStyle = cs2;
            sheet1.GetRow(rowIndex).GetCell(3).CellStyle = cs2;
            sheet1.GetRow(rowIndex).GetCell(4).CellStyle = cs2;
            sheet1.GetRow(rowIndex).GetCell(5).CellStyle = cs2;

            //营业收入：
            sheet1.GetRow(1).GetCell(1).SetCellValue(totalprice);
            //其中：现金
            //int price1 = GetMoneyRound(curPrice);
            sheet1.GetRow(2).GetCell(1).SetCellValue(curPrice);
            //挂账
            sheet1.GetRow(3).GetCell(1).SetCellValue(totalprice-curPrice);
            //石仔
            sheet1.GetRow(4).GetCell(1).SetCellValue(shiZai);
            //石粉
            sheet1.GetRow(5).GetCell(1).SetCellValue(shiFeng);
            //型号0--5
            int type05car = 0;//型号05车
            int type12car = 0;//型号12车
            int type13car = 0;//型号13车
            int type24car = 0;//型号14车
            int typeshiPotouCar = 0;//头破石车数
            double type05w = 0;//型号05重
            double type12w = 0;//型号12重
            double type13w = 0;//型号13重
            double type24w = 0;//型号14重
            double typeshiPotouCarW = 0;//头破石重


            type05car = dataSummary.Where(o => o.ShopNumber == "0-5").Sum(o=>o.Count);
            type12car = dataSummary.Where(o => o.ShopNumber == "1-2").Sum(o => o.Count);
            type13car = dataSummary.Where(o => o.ShopNumber == "1-3").Sum(o => o.Count);
            type24car = dataSummary.Where(o => o.ShopNumber == "2-4").Sum(o => o.Count);
            typeshiPotouCar = dataSummary.Where(o => o.ShopNumber == "头破石").Sum(o => o.Count);
            sheet1.GetRow(2).GetCell(3).SetCellValue(type05car);
            sheet1.GetRow(3).GetCell(3).SetCellValue(type12car);
            sheet1.GetRow(4).GetCell(3).SetCellValue(type13car);
            sheet1.GetRow(5).GetCell(3).SetCellValue(type24car);
            sheet1.GetRow(6).GetCell(3).SetCellValue(typeshiPotouCar);

            type05w = dataSummary.Where(o => o.ShopNumber == "0-5").Sum(o => o.TotalNetWeight);
            type12w = dataSummary.Where(o => o.ShopNumber == "1-2").Sum(o => o.TotalNetWeight);
            type13w = dataSummary.Where(o => o.ShopNumber == "1-3").Sum(o => o.TotalNetWeight);
            type24w = dataSummary.Where(o => o.ShopNumber == "2-4").Sum(o => o.TotalNetWeight);
            typeshiPotouCarW = dataSummary.Where(o => o.ShopNumber == "头破石").Sum(o => o.TotalNetWeight);

            type05w = Math.Round(type05w / 1000d, 2);
            type12w = Math.Round(type12w / 1000d, 2);
            type13w = Math.Round(type13w / 1000d, 2);
            type24w = Math.Round(type24w / 1000d, 2);
            typeshiPotouCarW = Math.Round(typeshiPotouCarW / 1000d, 2);
            sheet1.GetRow(2).GetCell(4).SetCellValue(type05w);
            sheet1.GetRow(3).GetCell(4).SetCellValue(type12w);
            sheet1.GetRow(4).GetCell(4).SetCellValue(type13w);
            sheet1.GetRow(5).GetCell(4).SetCellValue(type24w);
            sheet1.GetRow(6).GetCell(4).SetCellValue(typeshiPotouCarW);
            
            sheet1.GetRow(0).GetCell(0).SetCellValue(date1 + "销售报表");

            //EXCEL_DAILY_SUMMARY
            EXCEL_DAILY_SUMMARY ds = new EXCEL_DAILY_SUMMARY();
            ds.CASH = (decimal)curPrice;
            ds.DATE_STR = datestr;
            ds.DEBT = (decimal)totalprice - (decimal)curPrice;
            ds.FILE_NAME = outFileName;
            ds.GUID_KEY = guid;
            ds.REPORT_DATE = today;
            ds.SHIFENG_W = (decimal)shiFeng;
            ds.SHIZAI_W = (decimal)shiZai;
            ds.TOTAL_INCOME = (decimal)totalprice;
            
            using(var db = new OracleDataAccess())
            {
                var oldItem = db.GetItems<EXCEL_DAILY_SUMMARY>(o => o.DATE_STR == datestr);
                if(oldItem !=null && oldItem.Count > 0)
                {
                    db.DeleteItems(oldItem);
                }
                db.InsertItem(ds);
            }

            //EXCEL_DAILY_DETAIL
            InsertDailyDetail(dataViews, datestr,outFileName,guid,today);

            WriteToFile("信日报"+date1);

            return status;
        }

        void InsertDailyDetail(List<EXCEL_IMPORT_DAILY_V3> dataViews,string datestr,string filename,string guid,DateTime today)
        {
            var dataSummary = (from a in dataViews
                               group a by new { a.V_VENDOR, a.SHOP_NUMBER,a.UNIT_PRICE } into b
                               select new
                               {
                                   TakeDept = b.Key.V_VENDOR,
                                   ShopNumber = b.Key.SHOP_NUMBER,
                                   Count = b.Count(),
                                   UNIT_PRICE = b.Key.UNIT_PRICE,
                                   TotalNetWeight = b.Sum(c => Int32.Parse(c.NET_WEIGHT))
                               });

            List<EXCEL_DAILY_DETAIL> ddList = new List<EXCEL_DAILY_DETAIL>();
            foreach(var it in dataSummary)
            {
                EXCEL_DAILY_DETAIL dd = new EXCEL_DAILY_DETAIL();
                dd.DATE_STR = datestr;
                dd.FILE_NAME = filename;
                dd.GOODS_NAME = it.ShopNumber;
                dd.GROUP_NAME = it.ShopNumber == "石粉" ? "石粉" : "石仔";
                dd.GUID_KEY = guid;
                dd.REPORT_DATE = today;
                dd.UNIT_PRICE = it.UNIT_PRICE;
                dd.VENDOR = it.TakeDept;
                dd.WEIGHT = (decimal)Math.Round(it.TotalNetWeight / 1000d, 2);
                dd.IS_CHECK = "N";
                ddList.Add(dd);
            }
            using(var db =new OracleDataAccess())
            {
                var tmp = db.GetItems<EXCEL_DAILY_DETAIL>(o => o.DATE_STR == datestr);
                if(tmp!=null && tmp.Count > 0)
                {
                    db.DeleteItems(tmp);
                }
                db.InsertItems(ddList);
            }
        }
    }
}
