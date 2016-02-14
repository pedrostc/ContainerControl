using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContainerControl.Application.Repository;
using ContainerControl.Application.Services;
using ContainerControl.Domain.Model;
using ContainerControl.Presentation.MVC.Models;
using ContainerControl.Presentation.MVC.Services;

namespace ContainerControl.Presentation.MVC.Controllers
{
    public class CodigoIsoController : Controller
    {
        public ActionResult CodigoIsoSelectList(bool edicao = true)
        {
            return PartialView(edicao);
        }

        public JsonResult ListarCodigos()
        {
            List<CodigoIso> list = new List<CodigoIso>();
            using (CodigoIsoRepositorio codIsoRepo = new CodigoIsoRepositorio())
            {
                list = codIsoRepo.Listar().ToList();
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CapturaCodigo(Guid id)
        {
            CodigoIso cod = new CodigoIso();
            using (CodigoIsoRepositorio codIsoRepo = new CodigoIsoRepositorio())
            {
                cod = codIsoRepo.CapturarPorId(id);
            }

            return Json(cod, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SalvaCodigo(CodigoIsoViewModel model)
        {
            CodigoIso cod = new CodigoIso();
            using (CodigoIsoRepositorio codIsoRepo = new CodigoIsoRepositorio())
            {
                if(model.Id != Guid.Empty)
                    cod = codIsoRepo.CapturarPorId(model.Id);

                cod.Populate(ServicoConverteCodigoIsoViewModel.ParaDomainModel(model));
                cod = codIsoRepo.InserirOuAtualizar(cod);
            }
            return Json(cod);
        }

        public ActionResult ExcluiCodigo()
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult ExcluiCodigo(Guid id)
        {
            CodigoIso cod = new CodigoIso();
            using (CodigoIsoRepositorio codIsoRepo = new CodigoIsoRepositorio())
            {
                cod = codIsoRepo.Excluir(id);
            }

            return Json(cod);
        }
    }
}