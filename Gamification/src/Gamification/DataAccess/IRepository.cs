using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Gamification.Core.Entities;
using LinqSpecs;

namespace Gamification.Core.DataAccess
{
    public interface IRepository<TEntity> 
        where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetAll();
        TEntity GetById(int id);
        IQueryable<TEntity> BySpec(Specification<TEntity> spec); 
        IEnumerable<TEntity> BySpecWithQuery(Specification<TEntity> spec); 
        TEntity FirstBySpec(Specification<TEntity> spec); 
        TEntity Add(TEntity entity);
        TEntity AddPhysically(TEntity entity);
        void Delete(TEntity entity);
        void DeletePhysically(TEntity entity);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> condition);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> condition);
        void SaveChanges();
        void ClearContext();
    }
}
