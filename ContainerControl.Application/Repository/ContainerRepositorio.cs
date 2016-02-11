using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using ContainerControl.DAL.Context;
using ContainerControl.Domain.Model;

namespace ContainerControl.Application.Repository
{
    public class ContainerRepositorio : RepositorioBase<Container>
    {
        public override IEnumerable<Container> Listar()
        {
            using (ContainerControlContext dao = new ContainerControlContext())
            {
                return dao.Containers.Include(c => c.CodigoIso).ToList();
            }
        }

        public override IEnumerable<Container> CapturarPor(Expression<Func<Container, bool>> condicao)
        {
            using (ContainerControlContext dao = new ContainerControlContext())
            {
                return dao.Containers.Include(c => c.CodigoIso).Where(condicao).ToList();
            }
        }

        public override Container CapturarPorId(Guid id)
        {
            using (ContainerControlContext dao = new ContainerControlContext())
            {
                return dao.Containers.Include(c => c.CodigoIso).FirstOrDefault(c => c.Id == id);
            }
        }

        public override Container Excluir(Container ctnr)
        {
            using (ContainerControlContext dao = new ContainerControlContext())
            {
                dao.Entry(ctnr).State = EntityState.Deleted;
                //dao.Entry(ctnr.CodigoIso).State = EntityState.Unchanged;

                dao.SaveChanges();
            }
            return ctnr;
        }

        public override Container Inserir(Container model)
        {
            Container entity = null;
            using (ContainerControlContext dao = new ContainerControlContext())
            {
                entity = dao.Containers.Add(model);

                dao.Entry(entity).State = EntityState.Added;
                dao.Entry(model.CodigoIso).State = EntityState.Unchanged;

                dao.SaveChanges();
            }
            return entity;
        }

        public override Container Atualizar(Container model)
        {
            Container entity = null;
            using (ContainerControlContext dao = new ContainerControlContext())
            {
                entity = dao.Containers.Add(model);

                dao.Entry(entity).State = EntityState.Modified;
                dao.Entry(model.CodigoIso).State = EntityState.Unchanged;

                dao.SaveChanges();
            }
            return entity;
        }
    }
}
