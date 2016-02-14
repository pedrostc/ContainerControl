using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ContainerControl.Application.Repository;
using ContainerControl.Domain.Model;

namespace ContainerControl.Application.Services
{
    public static class ServicoConverteCodigosIso
    {
        public static List<SelectListItem> ParaSelectListItens(List<CodigoIso> codigosIso)
        {
            List<SelectListItem> itens = new List<SelectListItem>();

            foreach (CodigoIso cod in codigosIso)
            {
                itens.Add(new SelectListItem() {
                    Disabled = false,
                    Text = cod.Nome,
                    Value = cod.Id.ToString()
                });
            }
            return itens;
        }
    }
}
