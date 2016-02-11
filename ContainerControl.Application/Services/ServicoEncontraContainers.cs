using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContainerControl.Application.Repository;
using ContainerControl.Domain.Model;
using static System.Convert;
using static System.Math;

namespace ContainerControl.Application.Services
{
    public static class ServicoEncontraContainers
    {
        public static List<Container> ComFiltro(Container filtro)
        {
            List<Container> containers = new List<Container>();
            using (ContainerRepositorio repo = new ContainerRepositorio())
            {
                if (filtro == null)
                    containers = repo.Listar().ToList();
                else
                    containers = repo.Listar()
                        .Where(c => string.IsNullOrEmpty(filtro.NroContainer?.Replace(" ",string.Empty) ?? string.Empty) || c.NroContainer.Contains(filtro.NroContainer)) 
                        .Where(c => filtro.CodigoIsoId == Guid.Empty || c.CodigoIsoId == filtro.CodigoIsoId)
                        .Where(
                            c => filtro.Fabricacao.Equals(default(DateTime)) || c.Fabricacao.Equals(filtro.Fabricacao))
                        .Where(c => ToInt32(filtro.CM) == 0 || Abs(c.CM - filtro.CM) < 1)
                        .Where(c => ToInt32(filtro.Tara) == 0 || Abs(c.Tara - filtro.Tara) < 1)
                        .Where(c => ToInt32(filtro.Peso) == 0 || Abs(c.Peso - filtro.Peso) < 1)
                        .ToList();
            }

            return containers;
        }
    }
}
