using ExcelPro;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
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
    public class SheetBase
    {
        protected HSSFWorkbook hssfworkbook;
        protected ICellStyle baseCellStyle;
        protected string MomeySetting = "";
        
        protected void WriteToFile(string title)
        {
            //Write the stream data of workbook to the root directory
            string path = AppDomain.CurrentDomain.BaseDirectory + "/Output/" + title + ".xls";
            //Write the stream data of workbook to the root directory
            FileStream file = new FileStream(path, FileMode.Create);
            hssfworkbook.Write(file);
            file.Close();
        }

        /// <summary>
        /// yyyyMMdd ==>yyyy-MM-dd
        /// </summary>
        /// <param name="DateStr"></param>
        /// <returns></returns>
        protected string ConverDateStr(string dateStr)
        {
            return dateStr.Substring(0, 4) + "-" + dateStr.Substring(4, 2) + "-" + dateStr.Substring(6, 2);
        }
        protected void SetWidth(ISheet sheet,int col,int w)
        {
            sheet.SetColumnWidth(col, w * 256);
        }

        protected string GetMoneyRoundSetting()
        {
            using (var db = new OracleDataAccess())
            {
                var item = db.GetSingleItem<EXCEL_SETTINGS>(o => o.KEY == "MONEY_ROUND");

                return item.VALUE;
            }
        }

        protected int GetMoneyRound(double price)
        {
            if(MomeySetting == "")
            {
                throw new Exception("NOT FOUND MomeySetting");
            }

            //string sett = "0=0,1=0,2=0,3=5,4=5,5=5,6=5,7=5,8=10,9=10";
            string[] settArr = MomeySetting.Split(',');

            int money = 0;
            double price1 = Math.Round(price + 0.0001, 0, MidpointRounding.ToEven);
            money = Int32.Parse(price1.ToString());
            int mod = money % 10;
            int mad = money - money % 10;

            foreach (string m in settArr)
            {
                string[] m1 = m.Split('=');
                int name = Int32.Parse(m1[0]);
                int value = Int32.Parse(m1[1]);
                if (name == mod)
                {
                    mod = value;
                    break;
                }
            }
            money = mad + mod;

            return money;
        }

        protected double GetMoneyRoundD(double price)
        {
            if (MomeySetting == "")
            {
                throw new Exception("NOT FOUND MomeySetting");
            }

            //string sett = "0=0,1=0,2=0,3=5,4=5,5=5,6=5,7=5,8=10,9=10";
            string[] settArr = MomeySetting.Split(',');

            int money = 0;
            double price1 = Math.Round(price + 0.0001, 0, MidpointRounding.ToEven);
            money = Int32.Parse(price1.ToString());
            int mod = money % 10;
            int mad = money - money % 10;

            foreach (string m in settArr)
            {
                string[] m1 = m.Split('=');
                int name = Int32.Parse(m1[0]);
                int value = Int32.Parse(m1[1]);
                if (name == mod)
                {
                    mod = value;
                    break;
                }
            }
            money = mad + mod;

            return Double.Parse( money.ToString());
        }

        protected void CreateCellDouble(IRow rr, double value, int cellIndex)
        {
            ICell cc = rr.CreateCell(cellIndex);
            if (value == 0)
            {
                cc.SetCellValue("");
            }
            else
            {
                cc.SetCellValue(value);
            }

            cc.CellStyle = baseCellStyle;
        }

        protected void CreateCellDoubleWithColor(IRow rr, double value, int cellIndex)
        {
            ICell cc = rr.CreateCell(cellIndex);
            if (value == 0)
            {
                cc.SetCellValue("");
            }
            else
            {
                cc.SetCellValue(value);
            }

            ICellStyle ic = hssfworkbook.CreateCellStyle();
            ic.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            ic.TopBorderColor = HSSFColor.Grey80Percent.Index;
            ic.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            ic.BottomBorderColor = HSSFColor.Grey80Percent.Index;
            ic.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            ic.LeftBorderColor = HSSFColor.Grey80Percent.Index;
            ic.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            ic.RightBorderColor = HSSFColor.Grey80Percent.Index;
            ic.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            ic.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            IFont font = hssfworkbook.CreateFont();
            font.FontHeightInPoints = 11;
            ic.SetFont(font);
            ic.FillForegroundColor = HSSFColor.Orange.Index;
            ic.FillPattern = FillPattern.SolidForeground;
            cc.CellStyle = ic;
            
        }

        protected void CreateCellString(IRow rr, string value, int cellIndex)
        {
            if (Stock.Utils.StringUtil.IsNumber(value))
            {
                CreateCellDouble(rr, Double.Parse(value), cellIndex);
            }
            else
            {
                ICell cc = rr.CreateCell(cellIndex);
                cc.SetCellValue(value);
                cc.CellStyle = baseCellStyle;
            }
            
        }

        protected void CreateSumFormula(IRow rr, int cellIndex,string firstColName,string lastColName)
        {
            ICell cc = rr.CreateCell(cellIndex);
            cc.CellStyle = baseCellStyle;
            cc.SetCellFormula("sum("+firstColName+":"+lastColName+")");//sum(A1,C1)
        }

        protected void MearchCellFormat(ISheet sheet, int firstRow, int lastRow, int firstCol, int lastCol)
        {
            CellRangeAddress region = new CellRangeAddress(firstRow, lastRow, firstCol, lastCol);
            sheet.AddMergedRegion(region);

            RegionUtil.SetBorderLeft(1, region, sheet, hssfworkbook);
            RegionUtil.SetBorderBottom(1, region, sheet, hssfworkbook);
            RegionUtil.SetBorderRight(1, region, sheet, hssfworkbook);
            RegionUtil.SetBorderTop(1, region, sheet, hssfworkbook);
            RegionUtil.SetBottomBorderColor(HSSFColor.Grey80Percent.Index, region, sheet, hssfworkbook);
            RegionUtil.SetTopBorderColor(HSSFColor.Grey80Percent.Index, region, sheet, hssfworkbook);
            RegionUtil.SetLeftBorderColor(HSSFColor.Grey80Percent.Index, region, sheet, hssfworkbook);
            RegionUtil.SetRightBorderColor(HSSFColor.Grey80Percent.Index, region, sheet, hssfworkbook);
        }

        protected void CreateMergeCellString(string value, ISheet sheet, IRow row, int firstRow, int lastRow, int firstCol, int lastCol)
        {
            CreateCellString(row, value, firstCol);
            MearchCellFormat(sheet, firstRow, lastRow, firstCol, lastCol);
        }

        protected void CreateMergeCellDouble(double value, ISheet sheet, IRow row, int firstRow, int lastRow, int firstCol, int lastCol)
        {
            CreateCellDouble(row, value, firstCol);
            MearchCellFormat(sheet, firstRow, lastRow, firstCol, lastCol);
        }

        protected void InitializeWorkbook()
        {
            hssfworkbook = new HSSFWorkbook();

            //Create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "";
            hssfworkbook.DocumentSummaryInformation = dsi;

            //Create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "";
            hssfworkbook.SummaryInformation = si;

            baseCellStyle = hssfworkbook.CreateCellStyle();
            baseCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            baseCellStyle.TopBorderColor = HSSFColor.Grey80Percent.Index;
            baseCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            baseCellStyle.BottomBorderColor= HSSFColor.Grey80Percent.Index;
            baseCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            baseCellStyle.LeftBorderColor= HSSFColor.Grey80Percent.Index;
            baseCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            baseCellStyle.RightBorderColor= HSSFColor.Grey80Percent.Index;
            baseCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            baseCellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            IFont font = hssfworkbook.CreateFont();
            font.FontHeightInPoints = 11;
            baseCellStyle.SetFont(font);

            MomeySetting = GetMoneyRoundSetting();
        }

        protected void InitWorkbook(string fileFullPath)
        {
            //read the template via FileStream, it is suggested to use FileAccess.Read to prevent file lock.
            //book1.xls is an Excel-2007-generated file, so some new unknown BIFF records are added. 
            FileStream file = new FileStream(fileFullPath, FileMode.Open, FileAccess.Read);

            hssfworkbook = new HSSFWorkbook(file);

            //create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "";
            hssfworkbook.DocumentSummaryInformation = dsi;

            //create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "";
            hssfworkbook.SummaryInformation = si;

            MomeySetting = GetMoneyRoundSetting();
        }

        protected void CreateTitle(ISheet sheet,string title,int lastCol)
        {
            IRow row = sheet.CreateRow(0);
            ICell titleCell = row.CreateCell(0);

            ICellStyle style;
            style = hssfworkbook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.Center;
            IFont font = hssfworkbook.CreateFont();
            font.FontHeightInPoints = 20;
            style.SetFont(font);

            titleCell.SetCellValue(title);
            titleCell.CellStyle = style;
            MearchCellFormat(sheet, 0, 0, 0, lastCol);
        }

        protected void CreateSubTitle(ISheet sheet, string title, int lastCol)
        {
            IRow row1 = sheet.CreateRow(1);
            ICell subTitleCell = row1.CreateCell(0);
            subTitleCell.SetCellValue(title);
            MearchCellFormat(sheet, 1, 1, 0, lastCol);

            ICellStyle style;
            style = hssfworkbook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.Center;
            IFont font = hssfworkbook.CreateFont();
            font.FontHeightInPoints = 14;
            style.SetFont(font);

            subTitleCell.CellStyle = style;
        }
    }
}
