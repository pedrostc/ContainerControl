using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContainerControl.Presentation.MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ManutenirContainer()
        {
            return View();
        }

        public ActionResult EditarContainer()
        {
            return PartialView();
        }

        public ActionResult EditarCodigoIso()
        {
            return PartialView();
        }
    }
}