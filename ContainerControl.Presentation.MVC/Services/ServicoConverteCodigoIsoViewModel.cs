using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContainerControl.Domain.Model;
using ContainerControl.Presentation.MVC.Models;

namespace ContainerControl.Presentation.MVC.Services
{
    public static class ServicoConverteCodigoIsoViewModel
    {
        public static CodigoIsoViewModel DeDomainModel(CodigoIso model)
        {
            CodigoIsoViewModel vmCod = new CodigoIsoViewModel()
            {
                Id = model.Id,
                Nome = model.Nome,
                Codigo = model.Codigo
            };

            return vmCod;
        }

        public static CodigoIso ParaDomainModel(CodigoIsoViewModel viewModel)
        {
            CodigoIso cod = new CodigoIso()
            {
                Id = viewModel.Id,
                Nome = viewModel.Nome,
                Codigo = viewModel.Codigo
            };

            return cod;
        }
    }
}
