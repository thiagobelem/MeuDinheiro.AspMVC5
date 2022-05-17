﻿using System;
using MeuDinheiro.Bussiness.Core.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MeuDinheiro.Bussiness.Core.Data
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task Adicionar(TEntity entity);

        Task<TEntity> ObterPorId(Guid id);

        Task<List<TEntity>> ObterTodos();

        Task Atualizar(TEntity entity);

        Task Remover(Guid id);

        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);

        Task<int> SaveChanges();
    }
}
