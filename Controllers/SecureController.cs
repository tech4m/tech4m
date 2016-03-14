using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace tech4mUI.Controllers
{
    [Authorize]
    public class SecureController : Controller
    {
        // GET: Secure
        public ActionResult Index()
        {
            return View();
        }
    }
}