using Bootstrap_FileUpload.Controllers;
using Stock.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Finance.Controllers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false)]
    public sealed class CheckLogin : Attribute
    {
        
        //什么都无需写
    }

    public class ParentController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.UserName = "";
            ViewBag.UpdateUser = "";
            base.OnActionExecuting(filterContext);

            //判断action是否有CheckLogin特性
            bool isNeedLogin = filterContext.ActionDescriptor.IsDefined(typeof(CheckLogin), false);

            if (isNeedLogin)
            {
                if (!IsLogin())
                {
                    string url = "";
                    string env = ConfigurationManager.AppSettings["ENV"];
                    if (env == "PROC")
                    {
                        url = ReportController.GetExcelSetting("WEBSITE_URL");
                        filterContext.Result = Redirect(url+"Login/Index");
                    }
                    else
                    {
                        filterContext.Result = Redirect("/Login/Index");
                    }
                    //如果没有登录，则跳至登陆页
                    
                }
            }
        }

        protected bool IsLogin()
        {
            if (Session["UserInfo"] != null)
            {
                ViewBag.UserName = Session["UserInfo"].ToString();
                return true;
            }
                

            return false;
        }
    }
}