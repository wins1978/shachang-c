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
    public class GoodsController : ParentController
    {
        [CheckLogin]
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public JsonResult GetGoods(int limit, int offset)
        {
            List<EXCEL_GOODS_LIST> datas = new List<EXCEL_GOODS_LIST>();
            using(var db=new OracleDataAccess())
            {
                datas = db.GetItems<EXCEL_GOODS_LIST>(o => true).OrderBy(o=>o.ORDER_INDEX).ToList();
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
        public JsonResult Add(EXCEL_GOODS_LIST oData)
        {
            string goodsName = oData.GOODS_NAME.Trim().ToUpper();
            string status = "添加成功";
            oData.GOODS_NAME = oData.GOODS_NAME.Trim();
            if (String.IsNullOrEmpty(oData.GOODS_NAME))
            {
                return Json("物品名称不能为空", JsonRequestBehavior.AllowGet);
            }

            EXCEL_GOODS_LIST old = null;
            using (var db = new OracleDataAccess())
            {
                old = db.GetSingleItem<EXCEL_GOODS_LIST>(o => o.GOODS_NAME == goodsName);
                if(old != null)
                {
                    status = "添加失败，该物品已经存在";
                }
                else
                {
                    db.InsertItem(oData);
                }
            }

            return Json(status, JsonRequestBehavior.AllowGet);
        }

        [CheckLogin]
        [HttpPost]
        public JsonResult Update(EXCEL_GOODS_LIST oData)
        {
            if(oData == null)
            {
                return Json("没有选择行", JsonRequestBehavior.AllowGet);
            }
            string status = "更新失败";
            string goodsName = oData.GOODS_NAME.Trim().ToUpper();
            using (var db = new OracleDataAccess())
            {
                var data = db.GetSingleItem<EXCEL_GOODS_LIST>(o => o.GOODS_NAME == goodsName);
                if (data != null)
                {
                    db.UpdateItem(oData, new string[] { "GOODS_TYPE", "NODES", "ORDER_INDEX" });
                    status = "更新成功";
                }
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        [CheckLogin]
        [HttpPost]
        public JsonResult Delete(List<EXCEL_GOODS_LIST> oData)
        {
            if(oData == null || oData.Count == 0)
            {
                return Json("没有选择行", JsonRequestBehavior.AllowGet);
            } 
            string[] vendors = oData.Select(o => o.GOODS_NAME).ToArray();
            string status = "";
            using (var db = new OracleDataAccess())
            {
                var datas = db.GetItems<EXCEL_GOODS_LIST>(o => vendors.Contains(o.GOODS_NAME));
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