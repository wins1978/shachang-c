using ExcelPro;
using ExeMgrLib.Model;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tencent.OA.ATQ.DataAccess;

namespace ExeMgrLib
{
    public class DailyReportForCustomer: SheetBase
    {
        public string Run(string fileFullPath,string datestr)
        {
            //获取包括价格在内的视图
            List<EXCEL_IMPORT_DAILY_V3> dataViews = new List<EXCEL_IMPORT_DAILY_V3>();
            using (var db = new OracleDataAccess())
            {
                dataViews = db.GetItems<EXCEL_IMPORT_DAILY_V3>(o => o.IMPORT_DATE_STR == datestr);
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

            string status = CheckData(dataViews);
            if(status != ""){
                return status;
            }

            InitializeWorkbook();
            ISheet sheet = hssfworkbook.CreateSheet("客户日报");
            sheet.ForceFormulaRecalculation = true;

            List<CustomerDailyModel> cdms = BuildData(dataViews);

            string title = CreateHeader(sheet, datestr);
            BuildRow(cdms, sheet);
            InsertSummary(datestr, title);

            SetWidth(sheet, 1, 15);

            WriteToFile(title);


            return status;
        }

        private void InsertSummary(string datestr,string title)
        {
            string date2 = datestr.Substring(0, 4) + "-" + datestr.Substring(4, 2) + "-" + datestr.Substring(6, 2);
            DateTime today = DateTime.Parse(date2);
            using (var db = new OracleDataAccess())
            {
                var oldItem = db.GetItems<EXCEL_C_DAILY_SUMMARY>(o => o.DATE_STR == datestr);
                if (oldItem != null && oldItem.Count > 0)
                {
                    db.DeleteItems(oldItem);
                }
                EXCEL_C_DAILY_SUMMARY ds = new EXCEL_C_DAILY_SUMMARY();
                ds.DATE_STR = datestr;
                ds.FILE_NAME = title + ".xls";
                ds.TITLE = title;
                ds.REPORT_DATE = today;
                db.InsertItem(ds);
            }
        }

        private string  CreateHeader(ISheet sheet,string datestr)
        {
            string date1 = datestr.Substring(0, 4) + "年" + datestr.Substring(4, 2) + "月" + datestr.Substring(6, 2) + "日";
            string title = date1+ "惠东县仙仔山石场出库核算表";
            CreateTitle(sheet, title, 14);

            IRow rr = sheet.CreateRow(1);
            CreateCellString(rr, "序号", 0);
            CreateCellString(rr, "客户名称", 1);
            CreateCellString(rr, "0—5", 2);
            CreateCellString(rr, "车数", 3);
            CreateCellString(rr, "1—2", 4);
            CreateCellString(rr, "车数", 5);
            CreateCellString(rr, "1—3", 6);
            CreateCellString(rr, "车数", 7);
            CreateCellString(rr, "2—4", 8);
            CreateCellString(rr, "车数", 9);
            CreateCellString(rr, "石粉", 10);
            CreateCellString(rr, "车数", 11);
            CreateCellString(rr, "头破石", 12);
            CreateCellString(rr, "车数", 13);
            CreateCellString(rr, "备注", 14);

            return title;
        }

        private void BuildRow(List<CustomerDailyModel> cdms,ISheet sheet)
        {
            int index = 2;
            double c2 = 0;
            double c3 = 0;
            double c4 = 0;
            double c5 = 0;
            double c6 = 0;
            double c7 = 0;
            double c8 = 0;
            double c9 = 0;
            double c10 = 0;
            double c11 = 0;
            double c12 = 0;
            double c13 = 0;
            foreach (CustomerDailyModel cdm in cdms)
            {
                IRow rr = sheet.CreateRow(index);
                CreateCellDouble(rr, index-1, 0);
                CreateCellString(rr, cdm.Vendor, 1);
                CreateCellDouble(rr, cdm.Weight05, 2);
                c2 += cdm.Weight05;
                CreateCellDouble(rr, cdm.CarCount05, 3);
                c3 += cdm.CarCount05;
                CreateCellDouble(rr, cdm.Weight12, 4);
                c4 += cdm.Weight12;
                CreateCellDouble(rr, cdm.CarCount12, 5);
                c5 += cdm.CarCount12;
                CreateCellDouble(rr, cdm.Weight13, 6);
                c6 += cdm.Weight13;
                CreateCellDouble(rr, cdm.CarCount13, 7);
                c7 += cdm.CarCount13;
                CreateCellDouble(rr, cdm.Weight24, 8);
                c8 += cdm.Weight24;
                CreateCellDouble(rr, cdm.CarCount24, 9);
                c9 += cdm.CarCount24;
                CreateCellDouble(rr, cdm.WeightShifeng, 10);
                c10 += cdm.WeightShifeng;
                CreateCellDouble(rr, cdm.CarCountShifeng, 11);
                c11 += cdm.CarCountShifeng;
                CreateCellDouble(rr, cdm.WeightTouposhi, 12);
                c12 += cdm.WeightTouposhi;
                CreateCellDouble(rr, cdm.CarCountTouposhi, 13);
                c13 += cdm.CarCountTouposhi;
                CreateCellString(rr, "", 14);
                index++;
            }
            IRow ri = sheet.CreateRow(index);
            CreateMergeCellString("合计：", sheet, ri, index, index, 0, 1);
            if (c2 > 0)
            {
                CreateSumFormula(ri, 2, "C3", "C" + index);
            }
            else
            {
                CreateCellString(ri, "", 2);
            }

            if (c3 > 0)
            {
                CreateSumFormula(ri, 3, "D3", "D" + index);
            }
            else
            {
                CreateCellString(ri, "", 3);
            }
            if (c4 > 0)
            {
                CreateSumFormula(ri, 4, "E3", "E" + index);
            }
            else
            {
                CreateCellString(ri, "", 4);
            }
            if (c5 > 0)
            {
                CreateSumFormula(ri, 5, "F3", "F" + index);
            }
            else
            {
                CreateCellString(ri, "", 5);
            }
            if (c6 > 0)
            {
                CreateSumFormula(ri, 6, "G3", "G" + index);
            }
            else
            {
                CreateCellString(ri, "", 6);
            }
            if (c7 > 0)
            {
                CreateSumFormula(ri, 7, "H3", "H" + index);
            }
            else
            {
                CreateCellString(ri, "", 7);
            }
            if (c8 > 0)
            {
                CreateSumFormula(ri, 8, "I3", "I" + index);
            }
            else
            {
                CreateCellString(ri, "", 8);
            }
            if (c9 > 0)
            {
                CreateSumFormula(ri, 9, "J3", "J" + index);
            }
            else
            {
                CreateCellString(ri, "", 9);
            }
            if (c10 > 0)
            {
                CreateSumFormula(ri, 10, "K3", "K" + index);
            }
            else
            {
                CreateCellString(ri, "", 10);
            }
            if (c11 > 0)
            {
                CreateSumFormula(ri, 11, "L3", "L" + index);
            }
            else
            {
                CreateCellString(ri, "", 11);
            }
            if (c12 > 0)
            {
                CreateSumFormula(ri, 12, "M3", "M" + index);
            }
            else
            {
                CreateCellString(ri, "", 12);
            }
            if (c13 > 0)
            {
                CreateSumFormula(ri, 13, "N3", "N" + index);
            }
            else
            {
                CreateCellString(ri, "", 13);
            }
            
            CreateCellString(ri, "", 14);
        }

        private List<CustomerDailyModel> BuildData(List<EXCEL_IMPORT_DAILY_V3> items)
        {
            List<string> vendors = new List<string>();
            vendors = items.Select(o => o.V_VENDOR).Distinct().ToList();
            List<CustomerDailyModel> cdms = new List<CustomerDailyModel>();
            foreach(string vendor in vendors)
            {
                CustomerDailyModel cdm = new CustomerDailyModel();
                cdm.Vendor = vendor;
                List<EXCEL_IMPORT_DAILY_V3> iiVendor = items.Where(o => o.V_VENDOR == vendor).ToList();
                foreach(EXCEL_IMPORT_DAILY_V3 ii in iiVendor)
                {
                    if (ii.SHOP_NUMBER == "0-5")
                    {
                        cdm.Weight05 += double.Parse(ii.NET_WEIGHT);
                        cdm.CarCount05 += 1;
                    }
                    else if(ii.SHOP_NUMBER == "1-2")
                    {
                        cdm.Weight12 += double.Parse(ii.NET_WEIGHT);
                        cdm.CarCount12 += 1;
                    }
                    else if (ii.SHOP_NUMBER == "1-3")
                    {
                        cdm.Weight13 += double.Parse(ii.NET_WEIGHT);
                        cdm.CarCount13 += 1;
                    }
                    else if (ii.SHOP_NUMBER == "2-4")
                    {
                        cdm.Weight24 += double.Parse(ii.NET_WEIGHT);
                        cdm.CarCount24 += 1;
                    }
                    else if (ii.SHOP_NUMBER == "石粉")
                    {
                        cdm.WeightShifeng += double.Parse(ii.NET_WEIGHT);
                        cdm.CarCountShifeng += 1;
                    }
                    else if (ii.SHOP_NUMBER == "头破石")
                    {
                        cdm.WeightTouposhi+= double.Parse(ii.NET_WEIGHT);
                        cdm.CarCountTouposhi += 1;
                    }
                }
                cdm.Weight05 = Math.Round(cdm.Weight05 / 1000, 2);
                cdm.Weight12 = Math.Round(cdm.Weight12 / 1000, 2);
                cdm.Weight13 = Math.Round(cdm.Weight13 / 1000, 2);
                cdm.Weight24 = Math.Round(cdm.Weight24 / 1000, 2);
                cdm.WeightShifeng = Math.Round(cdm.WeightShifeng / 1000, 2);
                cdm.WeightTouposhi = Math.Round(cdm.WeightTouposhi / 1000, 2);
                cdms.Add(cdm);
            }

            return cdms;
        }

        private string CheckData(List<EXCEL_IMPORT_DAILY_V3> dataViews)
        {
            string status = "";
           
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
                if (!ite.UNIT_PRICE.HasValue || ite.UNIT_PRICE.Value < 1)
                {
                    status = "部分数据价格关系为空或未定义";
                }
            }

            return status;
        }

        #region private


        #endregion
    }
}
