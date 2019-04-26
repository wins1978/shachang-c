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
    public class AdminController : ParentController
    {
        [CheckLogin]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [CheckLogin]
        public JsonResult GetVendor(int limit, int offset)
        {
            List<EXCEL_VENDOR_LIST> datas = new List<EXCEL_VENDOR_LIST>();
            using(var db=new OracleDataAccess())
            {
                datas = db.GetItems<EXCEL_VENDOR_LIST>(o => true).OrderBy(o=>o.VENDOR).ToList();
            }
            
            var oRes = new
            {
                //rows = datas.Skip(offset).Take(limit).ToList(),
                rows= datas.Skip(offset).Take(limit).ToList(),
                total = datas.Count
            };
            return Json(oRes, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [CheckLogin]
        public JsonResult Add(EXCEL_VENDOR_LIST oData)
        {
            string vender = oData.VENDOR.Trim().ToUpper();
            string status = "添加成功";
            oData.VENDOR = oData.VENDOR.Trim();
            if (String.IsNullOrEmpty(oData.VENDOR))
            {
                return Json("客户名称不能为空", JsonRequestBehavior.AllowGet);
            }

            if (oData.COST_ALERT.HasValue)
            {
                int cost = 0;
                bool chk = Int32.TryParse(oData.COST_ALERT.Value.ToString(), out cost);
                if (!chk)
                {
                    return Json("警戒线非数字", JsonRequestBehavior.AllowGet);
                }
            }

            EXCEL_VENDOR_LIST old = null;
            using (var db = new OracleDataAccess())
            {
                old = db.GetSingleItem<EXCEL_VENDOR_LIST>(o => o.VENDOR == vender);
                if(old != null)
                {
                    status = "添加失败，该客户已经存在";
                }
                else
                {
                    db.InsertItem(oData);
                }
            }

            return Json(status, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [CheckLogin]
        public JsonResult Update(EXCEL_VENDOR_LIST oData)
        {
            string status = "";
            string vender = oData.VENDOR.Trim();
            using (var db = new OracleDataAccess())
            {
                var data = db.GetSingleItem<EXCEL_VENDOR_LIST>(o => o.VENDOR == vender);
                if (data != null)
                {
                    db.UpdateItem(oData, new string[] { "TEL", "CONTACT_NAME","COST_ALERT" });
                    status = "更新成功";
                }
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [CheckLogin]
        public JsonResult Delete(List<EXCEL_VENDOR_LIST> oData)
        {
            string[] vendors = oData.Select(o => o.VENDOR).ToArray();
            string status = "";
            using (var db = new OracleDataAccess())
            {
                var datas = db.GetItems<EXCEL_VENDOR_LIST>(o => vendors.Contains(o.VENDOR));
                if (datas.Count > 0)
                {
                    db.DeleteItems(datas);
                    status = "删除成功:" + datas.Count + "行";
                }
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Search(string name)
        {
            List<EXCEL_VENDOR_LIST> datas = new List<EXCEL_VENDOR_LIST>();
            using (var db = new OracleDataAccess())
            {
                datas = db.GetItems<EXCEL_VENDOR_LIST>(o => o.VENDOR.Contains(name));
            }

            var oRes = new
            {
                rows = datas,
                total = datas.Count
            };
            return Json(oRes, JsonRequestBehavior.AllowGet);
        }
    }
}