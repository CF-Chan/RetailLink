using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartTradeKendo.Models.ViewModel;
using System.Web.Security;
using SmartTradeKendo.Models.Constants;
using SmartTradeKendo.Models.IRepository;
using SmartTradeKendo.Models.Repository;
using SmartTradeKendo.Helpers;
using System.Web.Routing;

namespace SmartTradeKendo.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

 
        public ActionResult Index()
        {
            LoginViewModel login = new LoginViewModel();
            Helpers.Utils.Session.ClearAll();
            string rememberMe = Helpers.Utils.Common.GetCookie(Helpers.Constants.Cookies.RememberMe);
            login.RememberMe = rememberMe.ToLower() == "true" ? true : false;
            if (login.RememberMe)
                login.LoginId = Helpers.Utils.Common.GetCookie(Helpers.Constants.Cookies.LoginId);
            return View(login);
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel login)
        {
            int userId;
            int status;
            int userType;
            string userName;
            DateTime lastLogin; 
            int loginAttempt;
            int expirydays;
            string loginId;
            string password;

            if (login.RememberMe)
                Helpers.Utils.Common.SetCookie(Helpers.Constants.Cookies.LoginId, login.LoginId);
            Helpers.Utils.Common.SetCookie(Helpers.Constants.Cookies.RememberMe, login.RememberMe.ToString());
            if (ModelState.IsValid)
            {
                loginId = login.LoginId.ToString();
                password = login.Password.ToString();
                IUserRepository userRepositoy = new UserRepository();

                userRepositoy.Login(loginId, password, out userId, out status, out userType, out userName, out lastLogin, out loginAttempt, out expirydays);

                switch (status)
	            {
                    case 0:
                        string route;
                        route = Request.QueryString["ReturnUrl"];
                          
                        Helpers.Utils.Session.Set(SessionObjects.UserId, userId);
                        Helpers.Utils.Session.Set(SessionObjects.UserName, userName);
                        Helpers.Utils.Session.Set(SessionObjects.LastLogin, lastLogin.ToString().GridView_FmtDate());
                        Helpers.Utils.Session.Set(SessionObjects.LoginAttempt, loginAttempt);
                        Helpers.Utils.Session.Set(SessionObjects.PasswordExpiryDays, expirydays);
                        if (route == null)
                            return RedirectToAction("Index", "Home", new { });
                        else
                            return Redirect(route);
                        break;
                    case 1:
                        ViewBag.ErrorMessage = Resources.Message.LoginFail1;
                        break;
                    case 2:
                        ViewBag.ErrorMessage = Resources.Message.LoginFail2;
                        break;
                    case 3:
                        ViewBag.ErrorMessage = Resources.Message.LoginFail3;
                        break;
                    case 4:
                        ViewBag.ErrorMessage = Resources.Message.LoginFail4;
                        break;
                    case 5:
                        ViewBag.ErrorMessage = Resources.Message.LoginFail5;
                        break;
                    default:
                        ViewBag.ErrorMessage = "Please try again";
                        break;
	            }
            }
            return View(login);
        }
    }
}
