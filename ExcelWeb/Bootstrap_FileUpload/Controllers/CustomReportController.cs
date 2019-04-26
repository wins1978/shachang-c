using ExcelPro;
using ExeMgrLib;
using ExeMgrLib.Model;
using Finance.Controllers;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Tencent.OA.ATQ.Common.Json;
using Tencent.OA.ATQ.DataAccess;

namespace Bootstrap_FileUpload.Controllers
{
    public class CustomReportController : ParentController
    {
        [CheckLogin]
        public ActionResult Index(string type = "CustomReport")
        {
            return View();
        }

        [HttpGet]
        [CheckLogin]
        public JsonResult GetVendor()
        {
            List<EXCEL_VENDOR_LIST> datas = new List<EXCEL_VENDOR_LIST>();
            using (var db = new OracleDataAccess())
            {
                datas = db.GetItems<EXCEL_VENDOR_LIST>(o => true).OrderBy(o => o.VENDOR).ToList();
            }

            List<SelectModel> vendors = new List<SelectModel>();
            foreach (EXCEL_VENDOR_LIST v in datas)
            {
                SelectModel sm = new SelectModel();
                sm.value = v.VENDOR;
                sm.text = v.VENDOR;
                vendors.Add(sm);
            }

            return Json(vendors, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [CheckLogin]
        public JsonResult GetNumberFlag(string vendors)
        {
            List<string> vendorList = JsonUtils.ParseJson<List<string>>(vendors);
            string[] vendorArr = vendorList.ToArray();
            List<EXCEL_IMPORT_DAILY> datas = new List<EXCEL_IMPORT_DAILY>();
            using (var db = new OracleDataAccess())
            {
                datas = db.GetItems<EXCEL_IMPORT_DAILY>(o => vendorArr.Contains(o.TAKE_DEPT));
            }

            List<string> numberFlags = datas.Select(o => o.CAR_NUMBER).Distinct().ToList();
            List<SelectModel> numberFlagsModels = new List<SelectModel>();
            foreach (string v in numberFlags)
            {
                SelectModel sm = new SelectModel();
                sm.value = v;
                sm.text = v;
                numberFlagsModels.Add(sm);
            }

            return Json(numberFlagsModels, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [CheckLogin]
        public JsonResult GetGoods()
        {
            List<EXCEL_GOODS_LIST> datas = new List<EXCEL_GOODS_LIST>();
            using (var db = new OracleDataAccess())
            {
                datas = db.GetItems<EXCEL_GOODS_LIST>(o => true);
            }

            List<SelectModel> vendors = new List<SelectModel>();
            foreach(EXCEL_GOODS_LIST v in datas)
            {
                SelectModel sm = new SelectModel();
                sm.value = v.GOODS_NAME;
                sm.text = v.GOODS_NAME;
                vendors.Add(sm);
            }

            return Json(vendors, JsonRequestBehavior.AllowGet);
        }

        [CheckLogin]
        public JsonResult AllExport(CustomSearchParam csp)
        {
            AllDetailReport adr = new AllDetailReport();
            DateTime startDate = DateTime.Parse(csp.StartDate);
            DateTime endDate = DateTime.Parse(csp.EndDate);
            string path = adr.Run(startDate, endDate);

            string url = "http://" + HttpContext.Request.Url.Authority + "/";
            string env = ConfigurationManager.AppSettings["ENV"];
            if (env == "PROC")
            {
                url = ReportController.GetExcelSetting("WEBSITE_URL");
            }
            string link = url + "Output/" + path + "?tmp=" + Guid.NewGuid().ToString();
            return Json(link, JsonRequestBehavior.AllowGet);
        }

        [CheckLogin]
        public JsonResult Export(CustomSearchParam csp)
        {
            if (csp.VendorNameList == null || csp.VendorNameList.Count<1)
            {
                return Json("客户不能为空", JsonRequestBehavior.AllowGet);
            }
            if (String.IsNullOrEmpty(csp.StartDate))
            {
                return Json("开始时间不能为空", JsonRequestBehavior.AllowGet);
            }
            if (String.IsNullOrEmpty(csp.EndDate))
            {
                return Json("结束时间不能为空", JsonRequestBehavior.AllowGet);
            }

            List<EXCEL_IMPORT_DAILY_V2> datas = GetDailyDetails(csp);

            CustomReport cr = new CustomReport();
            DateTime startDate = DateTime.Parse(csp.StartDate);
            DateTime endDate = DateTime.Parse(csp.EndDate);
            string path = cr.Run(datas, startDate, endDate,csp.SearchType);

            string url = "http://" + HttpContext.Request.Url.Authority + "/";
            string env = ConfigurationManager.AppSettings["ENV"];
            if (env == "PROC")
            {
                url = ReportController.GetExcelSetting("WEBSITE_URL");
            }
            string link = url + "Output/" + path +"?tmp="+Guid.NewGuid().ToString();

            return Json(link, JsonRequestBehavior.AllowGet);
        }

        private List<EXCEL_IMPORT_DAILY_V2> GetDailyDetails(CustomSearchParam csp)
        {
            if (csp.VendorNameList != null)
            {
                csp.VendorNameList.Remove("");
            }
            if (csp.ShopNumberList != null)
            {
                csp.ShopNumberList.Remove("");
            }
            else
            {
                csp.ShopNumberList = new List<string>();
            }
            if (csp.CarNumberList != null)
            {
                csp.CarNumberList.Remove("");
            }
            else
            {
                csp.CarNumberList = new List<string>();
            }
            string[] carList = csp.CarNumberList.ToArray();
            string[] shopList = csp.ShopNumberList.ToArray();
            string[] vendor = csp.VendorNameList.ToArray();
            DateTime startDate = DateTime.Parse(csp.StartDate);
            DateTime endDate = DateTime.Parse(csp.EndDate);
            List<EXCEL_IMPORT_DAILY_V2> datas = new List<EXCEL_IMPORT_DAILY_V2>();
            using (var db = new OracleDataAccess())
            {
                if (carList.Count() > 0 && shopList.Count() > 0)
                {
                    datas = db.GetItems<EXCEL_IMPORT_DAILY_V2>(o => vendor.Contains(o.TAKE_DEPT) && o.REPORT_DATE >= startDate && o.REPORT_DATE <= endDate
                            && carList.Contains(o.CAR_NUMBER) && shopList.Contains(o.SHOP_NUMBER));
                }
                else if (carList.Count() > 0 && shopList.Count() == 0)
                {
                    datas = db.GetItems<EXCEL_IMPORT_DAILY_V2>(o => vendor.Contains(o.TAKE_DEPT) && o.REPORT_DATE >= startDate && o.REPORT_DATE <= endDate
                            && carList.Contains(o.CAR_NUMBER));
                }
                else if (carList.Count() == 0 && shopList.Count() > 0)
                {
                    datas = db.GetItems<EXCEL_IMPORT_DAILY_V2>(o => vendor.Contains(o.TAKE_DEPT) && o.REPORT_DATE >= startDate && o.REPORT_DATE <= endDate
                            && shopList.Contains(o.SHOP_NUMBER));
                }
                else
                {
                    datas = db.GetItems<EXCEL_IMPORT_DAILY_V2>(o => vendor.Contains(o.TAKE_DEPT) && o.REPORT_DATE >= startDate && o.REPORT_DATE <= endDate);
                }
            }

            return datas;
        }

        [CheckLogin]
        [HttpGet]
        public JsonResult Search(int limit, int offset, string VendorNameStr,string CarNumberList,string StartDate,string EndDate,string ShopNumberList,string SearchType)
        {
            if(VendorNameStr == null)
            {
                return Json("客户不能为空", JsonRequestBehavior.AllowGet);
            }

            CustomSearchParam csp = new CustomSearchParam();
            csp.CarNumberList = !String.IsNullOrEmpty(CarNumberList)? CarNumberList.Split('|').ToList():new List<string>();
            csp.EndDate = EndDate;
            csp.PageLimit = limit;
            csp.PageOffset = offset;
            csp.ShopNumberList = !String.IsNullOrEmpty(ShopNumberList)? ShopNumberList.Split('|').ToList() : new List<string>();
            csp.StartDate = StartDate;
            csp.VendorNameList = JsonUtils.ParseJson<List<string>>(VendorNameStr);
            csp.SearchType = SearchType;

            if (csp.VendorNameList == null || csp.VendorNameList.Count<1)
            {
                return Json("客户不能为空", JsonRequestBehavior.AllowGet);
            }
            if (String.IsNullOrEmpty(csp.StartDate))
            {
                return Json("开始时间不能为空", JsonRequestBehavior.AllowGet);
            }
            if (String.IsNullOrEmpty(csp.EndDate))
            {
                return Json("结束时间不能为空", JsonRequestBehavior.AllowGet);
            }

            List<EXCEL_IMPORT_DAILY_V2> datas = GetDailyDetails(csp);

            if(SearchType == "shop_number")
                return SearchByShopNumber(datas, limit,  offset);
            if (SearchType == "car_number")
                return SearchByCarNumber(datas, limit,  offset);
            if(SearchType == "car_shop")
                return SearchByCarShopNumber(datas, limit, offset);

            return Json("", JsonRequestBehavior.AllowGet);
        }

        private JsonResult SearchByCarShopNumber(List<EXCEL_IMPORT_DAILY_V2> datas, int limit, int offset)
        {
            var dataDetails = (from a in datas
                               group a by new { a.TAKE_DEPT, a.CAR_NUMBER, a.SHOP_NUMBER } into b
                               select new
                               {
                                   TAKE_DEPT = b.Key.TAKE_DEPT,
                                   SHOP_NUMBER = b.Key.SHOP_NUMBER,
                                   GROUP_NAME = b.Key.SHOP_NUMBER =="石粉"? "石粉" : "石仔",
                                   CAR_NUMBER = b.Key.CAR_NUMBER,
                                   CarCount = b.Count(),
                                   UNIT_PRICE = Math.Round(b.Average(c => c.UNIT_PRICE.Value), 2),
                                   TotalPrice = Math.Round(b.Sum(c => c.TOTAL_PRICE.Value), 2),
                                   TotalNetWeight = Math.Round(b.Sum(c => double.Parse(c.NET_WEIGHT) / 1000), 2)
                               }).OrderBy(o=>o.TAKE_DEPT).ThenBy(o=>o.CAR_NUMBER).ThenBy(o=>o.SHOP_NUMBER);

            var oRes = new
            {
                rows = dataDetails.Skip(offset).Take(limit).ToList(),
                //rows = dataDetails,
                total = dataDetails.Count()
            };

            return Json(oRes, JsonRequestBehavior.AllowGet);
        }

        private JsonResult SearchByCarNumber(List<EXCEL_IMPORT_DAILY_V2> datas, int limit, int offset)
        {
            var dataDetails = (from a in datas
                               group a by new { a.TAKE_DEPT, a.CAR_NUMBER,a.GROUP_NAME } into b
                               select new
                               {
                                   TAKE_DEPT = b.Key.TAKE_DEPT,
                                   SHOP_NUMBER = "",
                                   GROUP_NAME = b.Key.GROUP_NAME,
                                   CAR_NUMBER = b.Key.CAR_NUMBER,
                                   CarCount = b.Count(),
                                   UNIT_PRICE = Math.Round(b.Average(c => c.UNIT_PRICE.Value), 2),
                                   TotalPrice = Math.Round(b.Sum(c => c.TOTAL_PRICE.Value), 2),
                                   TotalNetWeight = Math.Round(b.Sum(c => double.Parse(c.NET_WEIGHT)/1000), 2)
                               }).OrderBy(o=>o.TAKE_DEPT).ThenBy(o=>o.CAR_NUMBER).ThenBy(o=>o.GROUP_NAME);

            var oRes = new
            {
                rows = dataDetails.Skip(offset).Take(limit).ToList(),
                //rows = dataDetails,
                total = dataDetails.Count()
            };

            return Json(oRes, JsonRequestBehavior.AllowGet);
        }

        private JsonResult SearchByShopNumber(List<EXCEL_IMPORT_DAILY_V2> datas, int limit, int offset)
        {
            var dataDetails = (from a in datas
                               group a by new { a.TAKE_DEPT, a.SHOP_NUMBER } into b
                               select new
                               {
                                   TAKE_DEPT = b.Key.TAKE_DEPT,
                                   SHOP_NUMBER = b.Key.SHOP_NUMBER,
                                   GROUP_NAME = b.Key.SHOP_NUMBER == "石粉" ? "石粉" :"石仔",
                                   CAR_NUMBER = "",
                                   CarCount = b.Count(),
                                   UNIT_PRICE = Math.Round(b.Average(c => c.UNIT_PRICE.Value), 2),
                                   TotalPrice = Math.Round(b.Sum(c => c.TOTAL_PRICE.Value), 2),
                                   TotalNetWeight = Math.Round(b.Sum(c => double.Parse(c.NET_WEIGHT) / 1000), 2)
                               }).OrderBy(o=>o.TAKE_DEPT).ThenBy(o=>o.SHOP_NUMBER);

            var oRes = new
            {
                rows = dataDetails.Skip(offset).Take(limit).ToList(),
                //rows = dataDetails,
                total = dataDetails.Count()
            };

            return Json(oRes, JsonRequestBehavior.AllowGet);

        }


    }
}