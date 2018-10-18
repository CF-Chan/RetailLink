using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartTradeKendo.Models.Constants;

using SmartTradeKendo.CustomFilters;

namespace SmartTradeKendo.Controllers
{
    [TrackUserAttribute]
    [NotifyExceptionAttribute]
    [ValidateUserAttribute]
    public abstract class BaseController : Controller
    {
    }
}
