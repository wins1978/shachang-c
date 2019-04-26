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
using System.Web.Routing;
using Tencent.OA.ATQ.Common.Json;
using Tencent.OA.ATQ.DataAccess;

namespace Bootstrap_FileUpload.Controllers
{
    public class AfterCostEditController : ParentController
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
        public JsonResult GetGoods()
        {
            List<EXCEL_GOODS_LIST> ShowNumbers = new List<EXCEL_GOODS_LIST>();
            using (var db = new OracleDataAccess())
            {
                ShowNumbers = db.GetItems<EXCEL_GOODS_LIST>(o => true);
            }

            List<SelectModel> vendors = new List<SelectModel>();
            foreach (EXCEL_GOODS_LIST v in ShowNumbers)
            {
                SelectModel sm = new SelectModel();
                sm.value = v.GOODS_NAME;
                sm.text = v.GOODS_NAME;
                vendors.Add(sm);
            }

            return Json(vendors, JsonRequestBehavior.AllowGet);
        }

        [CheckLogin]
        public JsonResult Export(List<UpdatePriceModel> csp,double price)
        {
            try
            {
                if(csp.Count<1 || price < 0.1)
                {
                    return Json("更新失败,没有数据", JsonRequestBehavior.AllowGet);
                }

                List<InputInfo> list = new List<InputInfo>();
                
                using(var db=new OracleDataAccess())
                {
                    foreach(UpdatePriceModel pm in csp)
                    {
                        string Vendor = pm.Vendor;

                        string DateStr = pm.DateStr;

                        string ShopNumber = pm.ShopNumber;

                        decimal UnitPrice =Decimal.Parse(pm.UnitPrice);

                        var datasa = db.GetItems<EXCEL_IMPORT_DAILY>(o => o.TAKE_DEPT==Vendor && o.DATE_STR==DateStr && o.SHOP_NUMBER==ShopNumber && o.UNIT_PRICE== UnitPrice);
                        foreach (EXCEL_IMPORT_DAILY ite in datasa)
                        {
                            ite.UNIT_PRICE = (decimal)price;
                        }
                        db.UpdateItems(datasa, new string[] { "UNIT_PRICE" });
                    }
                }

                string[] datestrArr = csp.Select(o => o.DateStr).Distinct().ToArray();
                List<EXCEL_IMPORT_DAILY> datas2 = new List<EXCEL_IMPORT_DAILY>();
                using (var db = new OracleDataAccess())
                {
                    datas2 = db.GetItems<EXCEL_IMPORT_DAILY>(o => datestrArr.Contains(o.DATE_STR));
                   
                }

                foreach (EXCEL_IMPORT_DAILY ite in datas2)
                {
                    InputInfo item = new InputInfo();
                    item.CarNumber = ite.CAR_NUMBER;
                    item.Date = ite.DATE_STR;
                    item.DriverName = ite.DRIVER_NAME;
                    item.GrossWeight = ite.GROSS_WEIGHT;
                    item.GroupName = ite.GROUP_NAME;
                    ite.NET_WEIGHT = item.NetWeight;
                    item.NumberFlag = ite.NUMBER_FLAG;
                    item.SendOutDept = ite.SEND_OUT_DEPT;
                    item.ShopNumber = ite.SHOP_NUMBER;
                    item.TakeDept = ite.TAKE_DEPT;
                    item.TareWeight = ite.TARE_WEIGHT;
                    item.Time = ite.TIME_STR;
                    item.VendorNetWeight = ite.VENDOR_NET_WEIGHT;
                    item.UnitPrice = ite.UNIT_PRICE.ToString();
                    list.Add(item);
                }

                DailyReportB rt = new DailyReportB();

                string filePath = AppDomain.CurrentDomain.BaseDirectory + "/Template/DailyReport.xls";
                string status = rt.Run(filePath, list);

                DailyMoneyUpdate dmu = new DailyMoneyUpdate();
                dmu.Run();

                return Json("更新成功", JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                Lg.Error(ex.Message);
                return Json("更新失败", JsonRequestBehavior.AllowGet);
            }
            
        }

        private List<EXCEL_IMPORT_DAILY> GetDailyDetails(CustomSearchParam csp)
        {
            if (csp.VendorNameList != null)
            {
                csp.VendorNameList.Remove("");
            }
            if(csp.ShopNumberList != null)
            {
                csp.ShopNumberList.Remove("");
            }
            
            string[] vendor = csp.VendorNameList.ToArray();
            DateTime startDate = DateTime.Parse(csp.StartDate);
            DateTime endDate = DateTime.Parse(csp.EndDate);
            List<EXCEL_IMPORT_DAILY> datas = new List<EXCEL_IMPORT_DAILY>();
            using (var db = new OracleDataAccess())
            {
                if (csp.ShopNumberList !=null && csp.ShopNumberList.Count > 0)
                {
                    string[] shops = csp.ShopNumberList.ToArray();
                    datas = db.GetItems<EXCEL_IMPORT_DAILY>(o => vendor.Contains(o.TAKE_DEPT) && shops.Contains(o.SHOP_NUMBER) && o.REPORT_DATE >= startDate && o.REPORT_DATE <= endDate);
                }
                else
                {
                    datas = db.GetItems<EXCEL_IMPORT_DAILY>(o => vendor.Contains(o.TAKE_DEPT) && o.REPORT_DATE >= startDate && o.REPORT_DATE <= endDate);
                }
                

            }

            return datas.OrderBy(o=>o.REPORT_DATE).ToList();
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
            csp.CarNumberList = new List<string>();
            csp.EndDate = EndDate;
            csp.PageLimit = limit;
            csp.PageOffset = offset;
            if(!String.IsNullOrEmpty(ShopNumberList))
                csp.ShopNumberList = JsonUtils.ParseJson<List<string>>(ShopNumberList);
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

            List<EXCEL_IMPORT_DAILY> datas = GetDailyDetails(csp);
            var dataDetails = (from a in datas
                               group a by new { a.TAKE_DEPT, a.DATE_STR, a.UNIT_PRICE,a.SHOP_NUMBER } into b
                               select new
                               {
                                   TakeDept = b.Key.TAKE_DEPT,
                                   DATE_STR = b.Key.DATE_STR,
                                   SHOP_NUMBER = b.Key.SHOP_NUMBER,
                                   PRICE = b.Key.UNIT_PRICE.Value
                               });

            List<UpdatePriceModel> List2 = new List<UpdatePriceModel>();
            foreach(var item in dataDetails)
            {
                UpdatePriceModel upm = new UpdatePriceModel();
                upm.DateStr = item.DATE_STR;
                upm.ShopNumber = item.SHOP_NUMBER;
                upm.UnitPrice = item.PRICE.ToString();
                upm.Vendor = item.TakeDept;

                List2.Add(upm);
            }

            List2 = List2.OrderBy(o => o.ShopNumber).ThenBy(o => o.DateStr).ToList();

            var oRes = new
            {
                rows = List2,
                //rows = dataDetails,
                total = datas.Count()
            };

            return Json(oRes, JsonRequestBehavior.AllowGet);
        }
    }
}