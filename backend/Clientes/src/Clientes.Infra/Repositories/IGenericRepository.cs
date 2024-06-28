using Clientes.Domain.Data;
using Clientes.Domain.DomainObjects;
using System.Linq.Expressions;

namespace Clientes.Infra.Repositories;

public interface IGenericRepository<TEntity> : IRepository where TEntity : IEntity
{
    IQueryable<TEntity> Get(Expression<Func<TEntity, bool>>? predicate = null);
    void Add(TEntity entity);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    void Update(TEntity entity);
    void Remove(Guid id);
    void Remove(TEntity entity);
}