using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ContainerControl.Application.Repository;
using ContainerControl.Application.Services;
using ContainerControl.Domain.Model;

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
            using (CodigoIsoRepositorio codIsoRepo = new CodigoIsoRepositorio())
            {
                ViewBag.CodigosIso = ServicoConverteCodigosIso.ParaSelectListItens(codIsoRepo.Listar().ToList());
            }
            List<Container> containers = new List<Container>();

            using (ContainerRepositorio contrRepo = new ContainerRepositorio())
            {
                containers = contrRepo.Listar().ToList();
            }
            return View(containers);
        }
        
        [HttpPost]
        public ActionResult ManutenirContainer(Container ParametrosPesquisa)
        {
            using (CodigoIsoRepositorio codIsoRepo = new CodigoIsoRepositorio())
            {
                ViewBag.CodigosIso = ServicoConverteCodigosIso.ParaSelectListItens(codIsoRepo.Listar().ToList());
            }

            return View(ServicoEncontraContainers.ComFiltro(ParametrosPesquisa));
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