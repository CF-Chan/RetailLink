using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using SmartTradeKendo.Models.IRepository;
using SmartTradeKendo.Models.Repository;

namespace SmartTradeKendo.Controllers
{
    public class MenuController : Controller
    {
        //
        // GET: /_Menu/

        public ActionResult Index()
        {
            IMenuRepository menuRepository = new MenuRepository();
            JsonResult json;
            json = Json(menuRepository.Get());
            ViewData["MENU_DATA"] = new JavaScriptSerializer().Serialize(json.Data);

            return View();
        }

    }
}
