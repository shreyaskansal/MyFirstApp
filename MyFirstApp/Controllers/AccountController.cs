using MyFirstApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Services;
using Core.Models;
using System.Web.Security;
using log4net;

namespace MyFirstApp.Controllers
{
    public class AccountController : Controller
    {
        private static readonly ILog logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Default page of the application
        /// ActionResult Index()
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        /// <summary>
        /// Default page of the application
        /// ActionResult Login()
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Login Action that passes the Username and Password to the Authentication Function
        /// that on success login to the account.
        /// ActionResult Login(LoginModel model, string returnUrl)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                UserService userService = new UserService();
                //ViewBag.uname = model.UserName;
                user = userService.AuthenticateUser(model.UserName, model.Password);



                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, true, "/");
                    this.ControllerContext.HttpContext.Response.Cookies.Add(GetCookie(model.UserName));
                    return RedirectToAction("Index","Dashboard");
                }
                else
                {
                    ModelState.AddModelError("", "*UserName or Password is Incorrect");
                    return View(model);

                }
            }
            else
                return View(model);
        }

        /// <summary>
        /// This is the Post Actio that Logs Out the user
        /// ActionResult LogOut()
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");        
        }

        /// <summary>
        /// This function generates the cookies
        /// static HttpCookie GetCookie(string username)
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static HttpCookie GetCookie(string username)
        {
            FormsAuthenticationTicket ticket;
            string cookiestr;
            HttpCookie cookie;
            ticket = new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now.AddMinutes(1), true, string.Empty);
            cookiestr = FormsAuthentication.Encrypt(ticket);
            cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
            cookie.Expires = ticket.Expiration;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            return cookie;
        }
            
    }
}