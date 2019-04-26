using ExcelPro;
using ExeMgrLib;
using ExeMgrLib.Model;
using Finance.Controllers;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using Stock.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Tencent.OA.ATQ.DataAccess;

namespace Bootstrap_FileUpload.Controllers
{

    public class ReportController : ParentController
    {
        [CheckLogin]
        public ActionResult Index(string type="DailyReport")
        {
            if (type == "DailyReport")
            {
                return GetDailyReport();
            }
            else if (type == "CustomDailyReport")
            {
                return CustomDailyReport();
            }
            else if(type == "WeeklyReport")
            {
                return GetWeeklyReport();
            }else if(type== "MonthlyReport")
            {
                return GetMonthlyReport();
            }

            return View();
        }

        [CheckLogin]
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult CreateMonthlyReport(string startMonth)
        {
            try
            {
                MonthlyReport wr = new MonthlyReport();
                string filename = wr.Run(startMonth);
                if (filename != "")
                {
                    EXCEL_MONTHLY_SUMMARY item = new EXCEL_MONTHLY_SUMMARY();
                    item.FILE_NAME = filename;
                    item.REPORT_DATE = DateTime.Parse(startMonth);
                    item.TITLE = filename;
                    
                    using (var db = new OracleDataAccess())
                    {
                        var old = db.GetSingleItem<EXCEL_MONTHLY_SUMMARY>(o => o.TITLE == filename);
                        if(old != null)
                        {
                            db.DeleteItem(old);
                        }
                        db.InsertItem(item);
                    }
                    return Json("生成月报成功", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                IOHelper.WriteLogToFile(ex.Message, @"c:\Log\shichang1.log");
                IOHelper.WriteLogToFile(ex.StackTrace, @"c:\Log\shichang1.log");
                return Json("未知错误", JsonRequestBehavior.AllowGet);
            }

            return Json("月报表生成成功", JsonRequestBehavior.AllowGet);
        }

        [CheckLogin]
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult CreateWeeklyReport(string startDate,string endDate)
        {
            try
            {
                WeeklyReport wr = new WeeklyReport();
                string err = wr.Run(startDate, endDate);
                if(err != "")
                {
                    return Json(err, JsonRequestBehavior.AllowGet);
                }

            }
            catch(Exception ex)
            {
                IOHelper.WriteLogToFile(ex.Message, @"c:\Log\shichang1.log");
                IOHelper.WriteLogToFile(ex.StackTrace, @"c:\Log\shichang1.log");
                return Json("未知错误", JsonRequestBehavior.AllowGet);
            }
           
            return Json("周报表生成成功", JsonRequestBehavior.AllowGet);
        }

        private ActionResult GetMonthlyReport()
        {
            List<DownloadInfo> items = new List<DownloadInfo>();

            List<EXCEL_MONTHLY_SUMMARY> list = new List<EXCEL_MONTHLY_SUMMARY>();
            using(var db = new OracleDataAccess())
            {
                list = db.GetItems<EXCEL_MONTHLY_SUMMARY>(o => true);
            }

            string url = "http://" + HttpContext.Request.Url.Authority + "/";
            string env = ConfigurationManager.AppSettings["ENV"];
            if (env == "PROC")
            {
                url = GetExcelSetting("WEBSITE_URL");
            }

            foreach (EXCEL_MONTHLY_SUMMARY ms in list)
            {
                DownloadInfo dl = new DownloadInfo();
                dl.FileFullPath = url + "Output/" + ms.FILE_NAME +"?stm="+Guid.NewGuid().ToString();
                dl.FileName = ms.FILE_NAME;
                dl.ReportDate = ms.REPORT_DATE.Value.ToString("yyyy-MM-dd");
                items.Add(dl);
            }

            ViewBag.ActionName = "MonthlyReport";
            ViewBag.Model = items;
            return View();
        }

        private ActionResult CustomDailyReport()
        {
            DateTime startDate = DateTime.Now.AddYears(-3);

            List<EXCEL_C_DAILY_SUMMARY> lst = new List<EXCEL_C_DAILY_SUMMARY>();
            using (var db = new OracleDataAccess())
            {
                lst = db.GetItems<EXCEL_C_DAILY_SUMMARY>(o => o.REPORT_DATE > startDate).OrderByDescending(o => o.REPORT_DATE).ToList();
            }

            string url = "http://" + HttpContext.Request.Url.Authority + "/";
            string env = ConfigurationManager.AppSettings["ENV"];
            if (env == "PROC")
            {
                url = GetExcelSetting("WEBSITE_URL");
            }

            List<DownloadInfo> items = new List<DownloadInfo>();
            foreach (EXCEL_C_DAILY_SUMMARY ds in lst)
            {
                DownloadInfo dl = new DownloadInfo();
                dl.FileFullPath = url + "Output/" + ds.FILE_NAME + "?stm=" + Guid.NewGuid().ToString();
                dl.FileName = ds.FILE_NAME;
                dl.ReportDate = ds.REPORT_DATE.Value.ToString("yyyy-MM-dd");
                items.Add(dl);
            }

            ViewBag.ActionName = "CustomDailyReport";
            ViewBag.Model = items;
            return View();
        }

        private ActionResult GetWeeklyReport()
        {
            DateTime startDate = DateTime.Now.AddYears(-3);

            List<EXCEL_WEEKLY_SUMMARY> lst = new List<EXCEL_WEEKLY_SUMMARY>();
            using (var db = new OracleDataAccess())
            {
                lst = db.GetItems<EXCEL_WEEKLY_SUMMARY>(o => o.REPORT_DATE > startDate).OrderByDescending(o => o.REPORT_DATE).ToList();
            }

            string url = "http://"+ HttpContext.Request.Url.Authority+"/";
            string env = ConfigurationManager.AppSettings["ENV"];
            if (env == "PROC")
            {
                url = GetExcelSetting("WEBSITE_URL");
            }

            List<DownloadInfo> items = new List<DownloadInfo>();
            foreach (EXCEL_WEEKLY_SUMMARY ds in lst)
            {
                DownloadInfo dl = new DownloadInfo();
                dl.FileFullPath = url + "Output/" + ds.FILE_NAME + "?stm=" + Guid.NewGuid().ToString();
                dl.FileName = ds.FILE_NAME;
                dl.ReportDate = ds.REPORT_DATE.Value.ToString("yyyy-MM-dd");
                items.Add(dl);
            }

            ViewBag.ActionName = "WeeklyReport";
            ViewBag.Model = items;
            return View();
        }


        private ActionResult GetDailyReport()
        {
            DateTime startDate = DateTime.Now.AddYears(-3);

            List<EXCEL_DAILY_SUMMARY> lst = new List<EXCEL_DAILY_SUMMARY>();
            using(var db=new OracleDataAccess())
            {
                lst = db.GetItems<EXCEL_DAILY_SUMMARY>(o => o.REPORT_DATE > startDate).OrderByDescending(o=>o.REPORT_DATE).ToList();
            }

            string url = "http://" + HttpContext.Request.Url.Authority + "/";
            string env = ConfigurationManager.AppSettings["ENV"];
            if (env == "PROC")
            {
                url = GetExcelSetting("WEBSITE_URL");
            }
            
            List<DownloadInfo> items = new List<DownloadInfo>();
            foreach(EXCEL_DAILY_SUMMARY ds in lst)
            {
                DownloadInfo dl = new DownloadInfo();
                dl.FileFullPath = url+"Output/" + ds.FILE_NAME + "?stm=" + Guid.NewGuid().ToString();
                dl.FileName = ds.FILE_NAME;
                dl.ReportDate = ds.REPORT_DATE.Value.ToString("yyyy-MM-dd");
                items.Add(dl);
            }

            ViewBag.ActionName = "DailyReport";
            ViewBag.Model = items;
            return View();
        }

        public static string GetExcelSetting(string key)
        {
            using(var db=new OracleDataAccess())
            {
                var item = db.GetSingleItem<EXCEL_SETTINGS>(o => o.KEY == key);

                return item.VALUE;
            }
        }
    }
}