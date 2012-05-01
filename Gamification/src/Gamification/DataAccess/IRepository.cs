using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Gamification.Core.Entities;
using LinqSpecs;

namespace Gamification.Core.DataAccess
{
    public interface IRepository<TEntity> : IQueryable<TEntity>
        where TEntity : BaseEntity
    {
        IQueryable<TEntity> QueryIncluding(params Expression<Func<TEntity, object>>[] includes);
        TEntity GetById(int id);
        TEntity GetByIdIncluding(int id, params Expression<Func<TEntity, object>>[] includes);
        TEntity StrictGetById(int id);
        IQueryable<TEntity> BySpec(Specification<TEntity> spec, params Expression<Func<TEntity, object>>[] includes); 
        IEnumerable<TEntity> BySpecWihoutQuery(Specification<TEntity> spec);
        TEntity FirstBySpec(Specification<TEntity> spec, params Expression<Func<TEntity, object>>[] includes); 
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
