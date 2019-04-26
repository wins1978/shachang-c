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
    public class VendorGoodsController : ParentController
    {
        [CheckLogin]
        public ActionResult Index()
        {
            List<EXCEL_GOODS_LIST> datas = new List<EXCEL_GOODS_LIST>();
            using (var db = new OracleDataAccess())
            {
                datas = db.GetItems<EXCEL_GOODS_LIST>(o => true);
                ViewBag.GoodsList = datas;
            }
            List<EXCEL_VENDOR_GOODS> vgoods = new List<EXCEL_VENDOR_GOODS>();
            using (var db = new OracleDataAccess())
            {
                ViewBag.VenorGoodsList = db.GetItems<EXCEL_VENDOR_GOODS>(o => true);
            }

            List<EXCEL_VENDOR_LIST> vendorList = new List<EXCEL_VENDOR_LIST>();
            using (var db = new OracleDataAccess())
            {
                vendorList = db.GetItems<EXCEL_VENDOR_LIST>(o => true).OrderBy(o=>o.VENDOR).ToList();
                ViewBag.VendorList = vendorList;
            }

            return View();
        }

        [HttpGet]
        public JsonResult GetVendorGoods(int limit, int offset)
        {
            List<EXCEL_VENDOR_GOODS> datas = new List<EXCEL_VENDOR_GOODS>();
            using(var db=new OracleDataAccess())
            {
                datas = db.GetItems<EXCEL_VENDOR_GOODS>(o => true);
            }

            datas = datas.OrderBy(o => o.VENDER).ToList();

            foreach(var item in datas)
            {
                item.DATE_STR = item.UPDATE_DATE.Value.ToString("yyyy-MM-dd");
            }


            var oRes = new
            {
                rows = datas.Skip(offset).Take(limit).ToList(),
                total = datas.Count
            };
            return Json(oRes, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetVendors()
        {
            List<EXCEL_VENDOR_LIST> datas = new List<EXCEL_VENDOR_LIST>();
            using (var db = new OracleDataAccess())
            {
                datas = db.GetItems<EXCEL_VENDOR_LIST>(o => true).OrderBy(o=>o.VENDOR).ToList();
            }


            string[] lst = datas.Select(o => o.VENDOR).ToArray();

            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetGoods()
        {
            List<EXCEL_GOODS_LIST> datas = new List<EXCEL_GOODS_LIST>();
            using (var db = new OracleDataAccess())
            {
                datas = db.GetItems<EXCEL_GOODS_LIST>(o => true);
            }

            string[] lst = datas.Select(o => o.GOODS_NAME).ToArray();

            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        [CheckLogin]
        [HttpPost]
        public JsonResult Add(ParmModel pModel)
        {
            if (String.IsNullOrEmpty(pModel.GoodsNameStr) || pModel.GoodsNameStr.IndexOf('|')<0)
            {
                return Json("没有选择物品", JsonRequestBehavior.AllowGet);
            }

            string errMsg = "";
            List<string> goodsNames = pModel.GoodsNameStr.Split('|').ToList();
            List<EXCEL_VENDOR_GOODS> newGoods = new List<EXCEL_VENDOR_GOODS>();
            foreach (string goods in goodsNames)
            {
                EXCEL_VENDOR_GOODS newgoods = new EXCEL_VENDOR_GOODS();
                newgoods.UNIT_PRICE = pModel.VendorGoods.UNIT_PRICE;
                newgoods.VENDER = pModel.VendorGoods.VENDER;
                EXCEL_VENDOR_GOODS g1 = ConverToVendorGoods(goods, newgoods, out errMsg);
                if (errMsg == "EXITS")
                {
                    continue;
                } else if (errMsg != "")
                {
                    return Json(errMsg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    newGoods.Add(g1);
                }
            }
            if (newGoods.Count > 0)
            {
                using (var db = new OracleDataAccess())
                {
                    db.InsertItems(newGoods);
                }
                    return Json("添加成功", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("新增0行，记录已经存在", JsonRequestBehavior.AllowGet);
            }

        }


        private EXCEL_VENDOR_GOODS ConverToVendorGoods(string goods , EXCEL_VENDOR_GOODS data, out string errmsg)
        {
            errmsg = "";
            EXCEL_VENDOR_GOODS oData = data;
            string goodsName = goods;
            data.GOODS_NAME = goods;
            string vendor = oData.VENDER.Trim().ToUpper();
            oData.GOODS_NAME = oData.GOODS_NAME.Trim();
            oData.VENDER = oData.VENDER.Trim();
            oData.UPDATE_DATE = DateTime.Now;
            if (String.IsNullOrEmpty(oData.GOODS_NAME))
            {
                errmsg = "石材名称不能为空";
            }
            if (String.IsNullOrEmpty(oData.VENDER))
            {
                errmsg = "客户名称不能为空";
            }
            if (!oData.UNIT_PRICE.HasValue || oData.UNIT_PRICE < 1)
            {
                errmsg = "单价不能为空";
            }

            using (var db = new OracleDataAccess())
            {
                var old = db.GetSingleItem<EXCEL_VENDOR_GOODS>(o => o.GOODS_NAME == goodsName && o.VENDER == vendor);
                if (old != null)
                {
                    errmsg = "EXITS";
                }
            }

            return oData;
        }

        [CheckLogin]
        [HttpPost]
        public JsonResult Update(ParmModel pModel)
        {
            EXCEL_VENDOR_GOODS oData = pModel.VendorGoods;
            if (oData == null)
            {
                return Json("没有选择行", JsonRequestBehavior.AllowGet);
            }
            string status = "更新失败";

            string vendor = pModel.VendorGoods.VENDER;
            string GoodsPriceStr = pModel.GoodsPriceStr;
            string[] GoodsPriceArr = pModel.GoodsPriceStr.Split(new string[] { "|-|" }, StringSplitOptions.None);
            List<EXCEL_VENDOR_GOODS> vendorGoodsUpdateList = new List<EXCEL_VENDOR_GOODS>();
            foreach(string gpstr in GoodsPriceArr)
            {
                if (!String.IsNullOrEmpty(gpstr))
                {
                    string goods = gpstr.Split('|')[0];
                    string price = gpstr.Split('|')[1];
                    double unitPirce = price == "" ? 0 : double.Parse(price);
                    EXCEL_VENDOR_GOODS vg = new EXCEL_VENDOR_GOODS();
                    vg.VENDER = vendor;
                    vg.UPDATE_DATE = DateTime.Now;
                    vg.GOODS_NAME = goods;
                    vg.UNIT_PRICE = (decimal)unitPirce;
                    vendorGoodsUpdateList.Add(vg);
                }
            }
            if (vendorGoodsUpdateList.Count > 0)
            {
                using (var db = new OracleDataAccess())
                {
                    var old = db.GetItems<EXCEL_VENDOR_GOODS>(o => o.VENDER == vendor);
                    if(old != null)
                    {
                        db.DeleteItems(old);
                    }
                    db.InsertItems(vendorGoodsUpdateList);
                }

                status = "更新成功";
            }
            else
            {
                status = "没有可更新的记录";
            }


           
            //using (var db = new OracleDataAccess())
            //{
            //    var data = db.GetSingleItem<EXCEL_VENDOR_GOODS>(o => o.ID == id);
            //    if (data != null)
            //    {
            //        oData.UPDATE_DATE = DateTime.Now;
            //        db.UpdateItem(oData, new string[] {"UNIT_PRICE", "UPDATE_DATE" });
            //        status = "更新成功";

            //        EXCEL_VENDOR_GOODS_HIS his = new EXCEL_VENDOR_GOODS_HIS();
            //        his.DATE_STR = oData.DATE_STR;
            //        his.GOODS_NAME = oData.GOODS_NAME;
            //        his.UNIT_PRICE = oData.UNIT_PRICE;
            //        his.UPDATE_DATE = DateTime.Now;
            //        his.VENDER = oData.VENDER;
            //        db.InsertItem(his);
            //    }
            //}
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        [CheckLogin]
        [HttpPost]
        public JsonResult Delete(List<EXCEL_VENDOR_GOODS> oData)
        {
            if(oData == null || oData.Count == 0)
            {
                return Json("没有选择行", JsonRequestBehavior.AllowGet);
            } 
            long[] ids = oData.Select(o => o.ID).ToArray();
            string status = "删除失败";
            using (var db = new OracleDataAccess())
            {
                var datas = db.GetItems<EXCEL_VENDOR_GOODS>(o => ids.Contains(o.ID));
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