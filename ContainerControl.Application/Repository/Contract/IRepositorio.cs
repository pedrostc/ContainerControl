using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ContainerControl.Application.Repository.Contract
{
    public interface IRepositorio<T>
    {
        T CapturarPorId(Guid id);
        IEnumerable<T> CapturarPor(Expression<Func<T, bool>> predicate);
        IEnumerable<T> Listar();

        T InserirOuAtualizar(T model);
        T Inserir(T model);
        T Atualizar(T model);

        T Excluir(T model);
        T Excluir(Guid id);
    }
}
