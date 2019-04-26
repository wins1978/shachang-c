using ExcelPro;
using ExeMgrLib;
using ExeMgrLib.Model;
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
using System.Web.Routing;
using Tencent.OA.ATQ.DataAccess;

namespace Bootstrap_FileUpload.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "";
            string loginHidden= Request["loginHidden"];
            string txtRegHidden = Request["txtRegHidden"];

            if(HttpContext.Request.Cookies["LoginUserName"] != null)
            {
                ViewBag.LoginUserName = HttpContext.Request.Cookies["LoginUserName"].Value;
            }
            else
            {
                ViewBag.LoginUserName = "";
            }

            if (!String.IsNullOrEmpty(loginHidden))
            {
                return Login();
            }else if (!String.IsNullOrEmpty(txtRegHidden) && txtRegHidden=="reg")
            {
                return RegUser();
            }

            return View();
        }

        public ActionResult UpdateUser()
        {
            if(Session["UserInfo"] != null)
            {
                string txtRegHidden = Request["txtRegHidden"];
                string oldUserName = Session["UserInfo"].ToString();
                ViewBag.OldUserName = oldUserName;
                if (!String.IsNullOrEmpty(txtRegHidden) && txtRegHidden == "repassword")
                {
                    string username = Request["username"];
                    string register_password = Request["register_password"];
                    string rpassword = Request["rpassword"];
                    string oldpass = Request["old_password"];

                    if (String.IsNullOrEmpty(username))
                    {
                        ViewBag.ErrorMessage = "用户名不能为空";
                        return View();
                    }
                    if (String.IsNullOrEmpty(register_password))
                    {
                        ViewBag.ErrorMessage = "密码不能为空";
                        return View();
                    }
                    if(register_password.Trim() != rpassword)
                    {
                        ViewBag.ErrorMessage = "密码不匹配";
                        return View();
                    }

                    if (username != oldUserName)
                    {
                        ViewBag.ErrorMessage = "不存在的用户名或非法登录";
                        return View();
                    }

                    EXCEL_USERS user = null;
                    using(var db=new OracleDataAccess())
                    {
                        user = db.GetSingleItem<EXCEL_USERS>(o => o.USER_NAME == oldUserName && o.PASSWORD == oldpass);
                    }
                    if(user == null)
                    {
                        ViewBag.ErrorMessage = "密码不正确";
                        return View();
                    }
                    else
                    {
                        user.PASSWORD = register_password.Trim();
                        using (var db = new OracleDataAccess())
                        {
                            db.UpdateItem(user, new string[] { "PASSWORD" });
                        }
                        Session["UserInfo"] = null;
                        return RedirectToAction("Index", new RouteValueDictionary(
                        new { controller = "Login", action = "Index" }));
                    }
                }

                return View();
            }


            return View();
        }

        public ActionResult Logout()
        {
            if (Session["UserInfo"] != null)
            {
                Session["UserInfo"] = null;
            }
            return RedirectToAction("Index", new RouteValueDictionary(
            new { controller = "Login", action = "Index" }));
        }

        private ActionResult RegUser()
        {
            string username = Request["username"];
            string register_password = Request["register_password"];
            string rpassword = Request["rpassword"];

            if (String.IsNullOrEmpty(username))
            {
                ViewBag.Message = "用户名不能为空";
                return View();
            }
            username = username.Trim();
            using (var db = new OracleDataAccess())
            {
                EXCEL_USERS user = db.GetSingleItem<EXCEL_USERS>(o => o.USER_NAME == username);
                if(user != null)
                {
                    ViewBag.Message = "该用户已存在";
                    return View();
                }
            }

            using (var db=new OracleDataAccess())
            {
                EXCEL_USERS user = new EXCEL_USERS();
                user.IS_VALID = "N";
                user.PASSWORD = register_password.Trim();
                user.USER_NAME = username.Trim();
                db.InsertItem(user);

                ViewBag.Message = "注册成功";
            }
            return View();
        }

        private ActionResult Login()
        {
            string txtUserName = Request["txtUserName"];
            string txtPassword = Request["txtPassword"];
            //string remember = Request["remember"];

            if (String.IsNullOrEmpty(txtUserName))
            {
                ViewBag.Message = "用户名不能为空";
                return View();
            }
            

            EXCEL_USERS user = null;
            using (var db = new OracleDataAccess())
            {
                user = db.GetSingleItem<EXCEL_USERS>(o => o.USER_NAME == txtUserName && o.PASSWORD == txtPassword);
            }

            if(user == null)
            {
                ViewBag.Message = "用户不存在或密码错误";
                return View();
            }
            else if (user.IS_VALID != "Y")
            {
                ViewBag.Message = "用户没有激活，请联系管理员";
                return View();
            }

            //if(!String.IsNullOrEmpty(remember))
            //{
            //    HttpCookie cookie = new HttpCookie("LoginUserName");
            //    cookie.Value = txtUserName;
            //    HttpContext.Response.Cookies.Add(cookie);
            //}
            

            Session["UserInfo"] = txtUserName;
            return RedirectToAction("Index", new RouteValueDictionary(
            new { controller = "Home", action = "Index" }));
        }
    }
}