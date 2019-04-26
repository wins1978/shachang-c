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
    public class CostController : ParentController
    {
        [CheckLogin]
        public ActionResult Index(string moneytype="ALL")
        {
            List<EXCEL_VENDOR_LIST> vendorList = new List<EXCEL_VENDOR_LIST>();
            using (var db = new OracleDataAccess())
            {
                vendorList = db.GetItems<EXCEL_VENDOR_LIST>(o => true).OrderBy(o => o.VENDOR).ToList();
                ViewBag.VendorList = vendorList;
            }

            ViewBag.MoneyType = moneytype;
            return View();
        }


        [HttpGet]
        public JsonResult GetCostView(int limit, int offset,string moneyType)
        {
            return GetCostViewCurrentMonth(limit, offset);
        }

        private JsonResult GetCostViewCurrentMonth(int limit, int offset)
        {

            List<EXCEL_VENDOR_COST_CUR_V> datas = new List<EXCEL_VENDOR_COST_CUR_V>();
            using (var db = new OracleDataAccess())
            {
                datas = db.GetItems<EXCEL_VENDOR_COST_CUR_V>(o => true);
            }

            foreach(EXCEL_VENDOR_COST_CUR_V cc in datas)
            {
                if(cc.COST_ALERT.HasValue && cc.COST_ALERT.Value > 0 && cc.CUR_MON_REMAIN.HasValue)
                {
                    cc.REMAIN_PER = Math.Round( cc.CUR_MON_REMAIN.Value / cc.COST_ALERT.Value,2);
                }
                else
                {
                    cc.REMAIN_PER = 0;
                }
            }

            datas = datas.OrderBy(o => o.REMAIN_PER).ToList();

            var oRes = new
            {
                //rows = datas.Skip(offset).Take(limit).ToList(),
                rows = datas,
                total = datas.Count
            };
            return Json(oRes, JsonRequestBehavior.AllowGet);
        }

        private JsonResult GetCostViewAll(int limit, int offset)
        {

            List<EXCEL_VENDOR_COST_V> datas = new List<EXCEL_VENDOR_COST_V>();
            using (var db = new OracleDataAccess())
            {
                datas = db.GetItems<EXCEL_VENDOR_COST_V>(o => true).OrderBy(o => o.VENDOR).ToList();
            }

            var oRes = new
            {
                //rows = datas.Skip(offset).Take(limit).ToList(),
                rows = datas,
                total = datas.Count
            };
            return Json(oRes, JsonRequestBehavior.AllowGet);
        }
    }
}