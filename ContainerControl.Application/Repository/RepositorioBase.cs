using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ContainerControl.Application.Repository.Contract;
using ContainerControl.DAL.Context;
using ContainerControl.Domain.Model;

namespace ContainerControl.Application.Repository
{
    public abstract class RepositorioBase<T> : IDisposable, IRepositorio<T> where T : ModelBase
    {
        public virtual IEnumerable<T> Listar()
        {
            using (ContainerControlContext dao = new ContainerControlContext())
            {
                return dao.Set<T>().ToList();
            }
        }

        public virtual IEnumerable<T> CapturarPor(Expression<Func<T, bool>> condicao)
        {
            using (ContainerControlContext dao = new ContainerControlContext())
            {
                return dao.Set<T>().Where(condicao).ToList();
            }
        }

        public virtual T CapturarPorId(Guid id)
        {
            using (ContainerControlContext dao = new ContainerControlContext())
            {
                return dao.Set<T>().FirstOrDefault(c => c.Id == id);
            }
        }

        public virtual T Excluir(Guid id)
        {
            T excluir = null;
            using (ContainerControlContext dao = new ContainerControlContext())
            {
                excluir = CapturarPorId(id);
            }
            return Excluir(excluir);
        }

        public virtual T Excluir(T model)
        {
            using (ContainerControlContext dao = new ContainerControlContext())
            {
                dao.Entry(model).State = EntityState.Deleted;

                dao.SaveChanges();
            }
            return model;
        }

        public virtual T InserirOuAtualizar(T model)
        {
            T entity = model.Id == Guid.Empty ? Inserir(model) : Atualizar(model);

            return entity;
        }

        public virtual T Inserir(T model)
        {
            T entity = null;
            using (ContainerControlContext dao = new ContainerControlContext())
            {
                entity = dao.Set<T>().Add(model);
                dao.Entry(entity).State = EntityState.Added;
                dao.SaveChanges();
            }
            return entity;
        }

        public virtual T Atualizar(T model)
        {
            T entity = null;
            using (ContainerControlContext dao = new ContainerControlContext())
            {
                entity = dao.Set<T>().Attach(model);
                dao.Entry(entity).State = EntityState.Modified;
                dao.SaveChanges();
            }
            return entity;
        }

        public void Dispose()
        {
            
        }
    }
}
