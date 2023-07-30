using System;
using System.Linq;
using System.Linq.Expressions;

namespace Ecommerce.Repositories.Interfaces.Base
{
    public interface IRepositoryBase<TEntity>
    {
        IQueryable<TEntity> FindAll();

        IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression);

        IQueryable<TEntity> FindByConditionWithTracking(Expression<Func<TEntity, bool>> expression);

        void Create(TEntity entity);

        void Update(TEntity entity);

        void Detached(TEntity entity);

        void Delete(TEntity entity);
    }
}
