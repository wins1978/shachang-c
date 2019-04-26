using ExcelPro;
using ExeMgrLib;
using ExeMgrLib.Model;
using Finance.Controllers;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Tencent.OA.ATQ.DataAccess;

namespace Bootstrap_FileUpload.Controllers
{
    public class CostEditController : ParentController
    {
        [CheckLogin]
        public ActionResult Index(string vendor="")
        {
           
            ViewBag.VendorName = vendor;
            return View();
        }


        [HttpGet]
        public JsonResult GetCostDetail(int limit, int offset,string vendorName)
        {
            List<EXCEL_VENDOR_MONEYS> datas = new List<EXCEL_VENDOR_MONEYS>();
            using(var db=new OracleDataAccess())
            {
                datas = db.GetItems<EXCEL_VENDOR_MONEYS>(o => o.VENDOR== vendorName).OrderByDescending(o=>o.UPDATE_DATE).ToList();
            }
            
            var oRes = new
            {
                rows = datas.Skip(offset).Take(limit).ToList(),
                total = datas.Count
            };
            return Json(oRes, JsonRequestBehavior.AllowGet);
        }

        [CheckLogin]
        [HttpPost]
        public JsonResult Add(EXCEL_VENDOR_MONEYS oData)
        {

            if (String.IsNullOrEmpty(oData.VENDOR))
            {
                return Json("客户名称不能为空", JsonRequestBehavior.AllowGet);
            }
            if (!oData.MONEY_ADVANCE.HasValue)
            {
                return Json("预付款不能为空", JsonRequestBehavior.AllowGet);
            }
            try
            {
                DateTime.Parse(oData.DATE_STR);
            }
            catch (Exception ex)
            {
                return Json("日期格式必须为yyyy-MM-dd", JsonRequestBehavior.AllowGet);
            }

            //oData.DATE_STR = DateTime.Now.ToString("yyyy-MM-dd");
            oData.IMPORT_DATE = DateTime.Parse(oData.DATE_STR);
            oData.UPDATE_DATE = DateTime.Now;
            using (var db = new OracleDataAccess())
            {
                db.InsertItem(oData);
            }

            return Json("添加成功", JsonRequestBehavior.AllowGet);
        }

        [CheckLogin]
        [HttpPost]
        public JsonResult Update(EXCEL_VENDOR_MONEYS oData)
        {
            if(oData == null)
            {
                return Json("没有选择行", JsonRequestBehavior.AllowGet);
            }
            if (!oData.MONEY_ADVANCE.HasValue)
            {
                return Json("预收款不能为空", JsonRequestBehavior.AllowGet);
            }
            string status = "更新失败";

            try
            {
                DateTime.Parse(oData.DATE_STR);
            }catch(Exception ex)
            {
                return Json("日期格式必须为yyyy-MM-dd", JsonRequestBehavior.AllowGet);
            }

            oData.IMPORT_DATE= DateTime.Parse(oData.DATE_STR);

            string vendor = oData.VENDOR;
            string datestr = oData.DATE_STR;
            long id = oData.ID;

            using (var db = new OracleDataAccess())
            {
                var data = db.GetSingleItem<EXCEL_VENDOR_MONEYS>(o => o.ID==id);
                if (data != null)
                {
                    db.UpdateItem(oData, new string[] { "MONEY_ADVANCE", "NOTES","DATE_STR" });
                    status = "更新成功";
                }
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        [CheckLogin]
        [HttpPost]
        public JsonResult Delete(List<EXCEL_VENDOR_MONEYS> oData)
        {
            if(oData == null || oData.Count == 0)
            {
                return Json("没有选择行", JsonRequestBehavior.AllowGet);
            } 
            long[] vendors = oData.Select(o => o.ID).ToArray();
            string status = "";
            using (var db = new OracleDataAccess())
            {
                var datas = db.GetItems<EXCEL_VENDOR_MONEYS>(o => vendors.Contains(o.ID));
                if (datas.Count > 0)
                {
                    db.DeleteItems(datas);
                    status = "删除成功:" + datas.Count + "行";
                }
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
    }
}