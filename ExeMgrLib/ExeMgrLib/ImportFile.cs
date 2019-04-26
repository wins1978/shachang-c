using ExcelPro;
using ExeMgrLib.Model;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
//using Stock.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tencent.OA.ATQ.DataAccess;

namespace ExeMgrLib
{
    public class ImportFile
    {
        IWorkbook hssfworkbook;
        Dictionary<int, string> Header = new Dictionary<int, string>();
        List<InputInfo> items = new List<InputInfo>();
        StringBuilder ErrorMsg = new StringBuilder();

        public InputResults InitializeWorkbook(Stream fs,string fileExt)
        {
           
            InputResults result = new InputResults();
            
            Header = new Dictionary<int, string>();
            ErrorMsg = new StringBuilder();
            //read the template via FileStream, it is suggested to use FileAccess.Read to prevent file lock.
            //book1.xls is an Excel-2007-generated file, so some new unknown BIFF records are added. 

            if (fileExt.EndsWith(".xls"))
            {
                hssfworkbook = new HSSFWorkbook(fs);
            }
            else if (fileExt.EndsWith(".xlsx"))
            {
                hssfworkbook = new XSSFWorkbook(fs);
            }
            else
            {
                return null;
            }

            //hssfworkbook = new HSSFWorkbook(fs);
            
            ConvertToTables();

            result.ErrMessage= ErrorMsg.ToString();

            //
            string[] datestrArr = items.Select(o => o.Date).Distinct().ToArray();
            string datestr = GetDateString(datestrArr);
            string guid = Guid.NewGuid().ToString();
            //string date1 = datestr.Substring(0, 4) + "年" + datestr.Substring(4, 2) + "月" + datestr.Substring(6, 2) + "日";
            string outFileName = "信日报" + datestr + ".xls";
            //string date2 = datestr.Substring(0, 4) + "-" + datestr.Substring(4, 2) + "-" + datestr.Substring(6, 2);
            //DateTime today = DateTime.Parse(date2);
            DailyReport rt = new DailyReport();

            rt.DeleteData(datestrArr);

            string chk = "";
            using (var db = new OracleDataAccess())
            {
                foreach(InputInfo item in items)
                {
                    var t1 = db.GetSingleItem<EXCEL_IMPORT_DAILY>(o => o.NUMBER_FLAG == item.NumberFlag);
                    if(t1 != null)
                    {
                        chk = "错误:"+item.NumberFlag +"已经存在";
                        break;
                    }
                }
            }
            

                rt.InsertData(items, guid, outFileName);
            List<EXCEL_IMPORT_DAILY_V3> dataViews = new List<EXCEL_IMPORT_DAILY_V3>();
            using (var db = new OracleDataAccess())
            {
                dataViews = db.GetItems<EXCEL_IMPORT_DAILY_V3>(o => datestrArr.Contains(o.IMPORT_DATE_STR));
            }
            List<InputInfo> newList = new List<InputInfo>();
            string err = "";
            foreach (var item in dataViews)
            {
                InputInfo ii = new InputInfo();
                ii.CarNumber = item.CAR_NUMBER;
                if (String.IsNullOrEmpty(ii.CarNumber))
                {
                    err = "部分车号为空";
                }
                ii.Date = item.DATE_STR;
                if (String.IsNullOrEmpty(ii.Date))
                {
                    err += "部分日期为空；";
                }
                ii.DriverName = item.DRIVER_NAME;
                ii.GrossWeight = item.GROSS_WEIGHT;
                if (String.IsNullOrEmpty(ii.GrossWeight))
                {
                    err += "部分毛重为空；";
                }
                ii.GroupName = item.GROUP_NAME;
                ii.NetWeight = item.NET_WEIGHT;
                if (String.IsNullOrEmpty(ii.NetWeight))
                {
                    err += "部分净重为空；";
                }
                ii.NumberFlag = item.NUMBER_FLAG;
                if (String.IsNullOrEmpty(ii.NumberFlag))
                {
                    err += "部分磅单编号为空；";
                }
                ii.RowIndex = item.ROW_INDEX.HasValue? item.ROW_INDEX.Value.ToString():"";
                ii.SendOutDept = item.SEND_OUT_DEPT;
                ii.ShopNumber = item.SHOP_NUMBER;
                if (String.IsNullOrEmpty(ii.ShopNumber))
                {
                    err += "部分货物名称为空；";
                }
                ii.TakeDept = item.TAKE_DEPT;
                if (String.IsNullOrEmpty(ii.TakeDept))
                {
                    err += "部分进货单位为空；";
                }
                ii.TareWeight = item.TARE_WEIGHT;
                if (String.IsNullOrEmpty(ii.TareWeight))
                {
                    err += "部分皮重为空；";
                }
                ii.Time = item.TIME_STR;
                ii.UnitPrice = item.UNIT_PRICE.HasValue ? item.UNIT_PRICE.Value.ToString() : "";
                newList.Add(ii);
                if (String.IsNullOrEmpty(ii.UnitPrice) || ii.UnitPrice=="0")
                {
                    err = "部分价格为空，请检查客户名称是否正确或该型号是否设置了价格";
                }
            }
            rt.DeleteData(datestrArr);
            rt.InsertData(newList, guid, outFileName);

            List<InputInfo> orderitems = newList.OrderBy(o => o.TakeDept).ToList();
            result.InputInfoList = orderitems;
            if(String.IsNullOrEmpty(result.ErrMessage))
            {
             
                result.ErrMessage = err;
            }
            else
            {
               
            }
            rt.DeleteData(datestrArr);

            if(chk != "")
            {
                result.ErrMessage = chk;
            }
            return result;
        }

        string GetDateString(string[] datestrArr)
        {
            string d = "";
            foreach(string s in datestrArr)
            {
                d += s + "-";
            }
            d=d.TrimEnd('-');

            return d;
        }

        void  ConvertToTables()
        {
            ISheet sheet = hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
            int index = 0;
            int haderIndex = 0;
            bool isHeader = false;

            while (rows.MoveNext())
            {
                IRow row = (IRow)rows.Current;
                InputInfo item = new InputInfo();

                //check header
                if(isHeader == false)
                {
                    isHeader = IsHeader(row);
                    if (isHeader)
                    {
                        haderIndex = row.LastCellNum;
                        for (int i = 0; i < row.LastCellNum; i++)
                        {
                            ICell cell = row.GetCell(i);
                            if (cell != null && !String.IsNullOrEmpty(cell.ToString()))
                            {
                                Header.Add(i, cell.ToString());
                            }
                        }
                    }
                    index++;
                    continue;
                }

                for (int i = 0; i < haderIndex; i++)
                {
                    ICell cell = row.GetCell(i);

                    if (cell == null)
                    {
                        BuildRow(index, i, "", item);
                    }
                    else
                    {
                        string cellTitle = Header[i];


                        if (cellTitle == "货物名称")
                        {
                            if (cell.CellType == CellType.Numeric)
                            {
                                DateTime dt = cell.DateCellValue;
                                string c1 = dt.Month.ToString() + "-" + dt.Day.ToString();
                                BuildRow(index, i, c1, item);
                            }
                            else
                            {
                                BuildRow(index, i, cell.ToString(), item);
                            }


                        }else if(cellTitle == "首次称重日期" || cellTitle == "日期2")
                        {
                            if (cell.CellType == CellType.Numeric)
                            {
                                if (cell.ToString().Length == 8)
                                {
                                    BuildRow(index, i, cell.ToString(), item);
                                }
                                else
                                {
                                    DateTime dt = cell.DateCellValue;
                                    string c1 = dt.ToString("yyyyMMdd");
                                    BuildRow(index, i, c1, item);
                                }
                                
                            }
                            else
                            {
                                BuildRow(index, i, cell.ToString(), item);
                            }
                        }
                        else
                        {
                            BuildRow(index, i, cell.ToString(), item);
                        }

                    }
                }
                item.RowIndex = index.ToString();
                if(!String.IsNullOrEmpty(item.CarNumber))
                items.Add(item);

                index++;
            }
        }

        private bool IsHeader(IRow row)
        {
            bool isHeader = false;
            //收货单位
            string cell1 = "";
            int haderIndex = row.LastCellNum;
            for (int i = 0; i < row.LastCellNum; i++)
            {
                ICell cell = row.GetCell(i);
                if (cell != null && !String.IsNullOrEmpty(cell.ToString()))
                {
                    cell1 = cell.ToString();
                    if(cell1 == "收货单位")
                    {
                        isHeader = true;
                        break;
                    }
                }
            }

            return isHeader;
        }

        private void BuildRow(int index,int col,string cell, InputInfo item)
        {
            string colName = Header[col];
            switch (colName)
            {
                case "首次称重操作员": //old --驾驶员
                    item.DriverName = cell;
                    break;
                case "磅单编号":
                    CheckEmptyError(index, colName, col, cell);
                    item.NumberFlag = cell;
                    break;
                case "首次称重日期":
                    if (!String.IsNullOrEmpty(cell))
                    {
                        string c1=CheckDateError(index, colName, col, cell);
                        item.Date = c1;
                    }
                    break;
                case "日期2":
                    if (String.IsNullOrEmpty(item.Date))
                    {
                        if (!String.IsNullOrEmpty(cell))
                        {
                            string c1=CheckDateError(index, colName, col, cell);
                            item.Date = c1;
                        }
                    }
                    break;
                case "毛重时间":
                    item.Time = cell;
                    break;
                case "车号":
                    CheckEmptyError(index, colName, col, cell);
                    item.CarNumber = cell;
                    break;
                case "货物名称":
                    CheckEmptyError(index, colName, col, cell);
                    item.ShopNumber = cell;
                    item.GroupName = item.ShopNumber == "石粉" ? "石粉" : "石仔";
                    break;
                case "毛重":
                    CheckIntError(index, colName, col, cell);
                    item.GrossWeight = cell;
                    break;
                case "皮重":
                    CheckIntError(index, colName, col, cell);
                    item.TareWeight = cell;
                    break;
                case "净重":
                    CheckIntError(index, colName, col, cell);
                    item.NetWeight = cell;
                    item.VendorNetWeight = "";
                    break;
                case "收货单位":
                    CheckEmptyError(index, colName, col, cell);
                    item.TakeDept = cell;
                    break;
                case "发货单位":
                    item.SendOutDept = cell;
                    break;
            }
        }

        private void CheckEmptyError(int index,string colName,int col, string cell)
        {
            if (String.IsNullOrEmpty(cell))
            {
                string msg = String.Format("第{0}行，第{1}列错误，{2}：没有数据", index,col, colName);
                ErrorMsg.AppendLine(msg);
            }
        }

        private string CheckDateError(int index, string colName, int col, string cell)
        {
            string timestr = cell;
            if (String.IsNullOrEmpty(cell))
            {
                string msg = String.Format("第{0}行，第{1}列错误，{2}：没有数据", index, col, colName);
                ErrorMsg.AppendLine(msg);
            }
            else
            {
                try
                {
                    if (cell.IndexOf("-") <0)
                    {
                        
                        if (timestr.Length > 9)
                        {
                            timestr = timestr.Substring(0, 8);
                        }
                        DateTime ConvertDate = DateTime.ParseExact(timestr, "yyyyMMdd", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces);
                    }
                    else
                    {
                        DateTime ConvertDate = DateTime.ParseExact(cell, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces);
                    }
                }
                catch (Exception ex)
                {
                    string msg = String.Format("第{0}行，第{1}列，{2}：错误的日期：{3}", index, col, colName, cell);
                    ErrorMsg.AppendLine(msg);
                }
            }

            return timestr;
        }

        private void CheckIntError(int index, string colName, int col, string cell)
        {
            if (String.IsNullOrEmpty(cell))
            {
                string msg = String.Format("第{0}行，第{1}列错误，{2}：没有数据", index, col, colName);
                ErrorMsg.AppendLine(msg);
            }
            else
            {
                int num = -1;
                bool chk = Int32.TryParse(cell, out num);
                if (!chk)
                {
                    string msg = String.Format("第{0}行，第{1}列，{2}：错误的整数{3}", index, col, colName, cell);
                    ErrorMsg.AppendLine(msg);
                }
            }
        }
    }
}
