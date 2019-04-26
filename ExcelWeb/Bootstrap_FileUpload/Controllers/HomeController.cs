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
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Tencent.OA.ATQ.Common;
using Tencent.OA.ATQ.Common.Json;
using Tencent.OA.ATQ.DataAccess;

namespace Bootstrap_FileUpload.Controllers
{
    
    public class demo
    {
        public int id { get; set; }

        public string one { get; set; }
        public string two { get; set; }
    }

    public class HomeController : ParentController
    {
        [CheckLogin]
        public ActionResult Index()
        {
            String username = Session["UserInfo"].ToString();

            return View();
        }

        [CheckLogin]
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult SaveData(string getepassdata)//WebMethod to Save the data  
        {
            string status = "success";
            try
            {
                var serializeData = JsonConvert.DeserializeObject<List<InputInfo>>(getepassdata);

                DailyReportB rt = new DailyReportB();
                //信日报
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "/Template/DailyReport.xls";
                status = rt.Run(filePath, serializeData);

                DailyMoneyUpdate dmu = new DailyMoneyUpdate();
                dmu.Run();
            }
            catch (Exception ex)
            {
                IOHelper.WriteLogToFile(ex.Message, @"c:\Log\shichang1.log");
                IOHelper.WriteLogToFile(ex.StackTrace, @"c:\Log\shichang1.log");
                return Json("fail");
            }

            return Json(status);
        }

        [CheckLogin]
        [HttpPost]
        [ValidateInput(false)]
        //[ValidateAntiForgeryToken]
        public JsonResult Update()
        {
            try
            {
                var oFile = Request.Files["txt_file"];

                string fileName = oFile.FileName;

                var oStream = oFile.InputStream;
                //得到了文件的流对象，我们不管是用NPOI、GDI还是直接保存文件都不是问题了吧。。。。

                ImportFile imp = new ImportFile();
                InputResults rst = imp.InitializeWorkbook(oStream, fileName);
                
                return Json(rst, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                IOHelper.WriteLogToFile(ex.Message, @"c:\Log\shichang1.log");
                IOHelper.WriteLogToFile(ex.StackTrace, @"c:\Log\shichang1.log");
                throw new Exception(ex.Message);
            }
        }


    }
}