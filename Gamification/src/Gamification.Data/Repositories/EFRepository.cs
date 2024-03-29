﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Gamification.Core.DataAccess;
using Gamification.Core.Entities;
using Gamification.Core.Exceptions;
using Gamification.Core.Extensions;
using Gamification.Data.EF.Contexts;
using LinqSpecs;

namespace Gamification.Data.EF.Repositories
{
    public class EfRepository<TEntity> : IRepository<TEntity> 
        where TEntity : BaseEntity
    {
        private readonly EfDbContext dbContext;
        
        public EfRepository(EfDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private DbSet<TEntity> EntityPersister
        {
            get { return dbContext.Set<TEntity>(); }
        } 

        public IQueryable<TEntity> GetAll()
        {
            return EntityPersister.AsQueryable();
        }

        public IQueryable<TEntity> QueryIncluding(params Expression<Func<TEntity, object>>[] includes)
        {
            var query = this.GetAll();
            foreach (var expression in includes)
            {
                query = query.Include(expression);
            }

            return query;
        }

        public TEntity GetById(int id, params Expression<Func<TEntity, object>>[] includes)
        {
            var entity = QueryIncluding(includes).FirstOrDefault(x => x.Id == id);
            return entity;
        }

        public TEntity StrictGetById(int id, params Expression<Func<TEntity, object>>[] includes)
        {
            var entity = this.GetById(id, includes);
            if (entity == null)
                throw new EntityNotFoundException(typeof(TEntity).Name);
            return entity;
        }

        public IQueryable<TEntity> BySpec(Specification<TEntity> spec, params Expression<Func<TEntity, object>>[] includes)
        {
            return QueryIncluding(includes).BySpec(spec);
        }

        public IEnumerable<TEntity> BySpecWihoutQuery(Specification<TEntity> spec)
        {
            return GetAll().BySpec(spec).ToList();
        }

        public TEntity FirstBySpec(Specification<TEntity> spec, params Expression<Func<TEntity, object>>[] includes)
        {
            return QueryIncluding(includes).FirstBySpec(spec);
        }

        public TEntity Add(TEntity entity)
        {
            return EntityPersister.Add(entity);
        }

        public TEntity AddPhysically(TEntity entity)
        {
            this.Add(entity);
            this.SaveChanges();
            return entity;
        }

        public void Delete(TEntity entity)
        {
            EntityPersister.Remove(entity);
        }

        public void DeletePhysically(TEntity entity)
        {
            this.Delete(entity);
            this.SaveChanges();
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> condition, params Expression<Func<TEntity, object>>[] includes)
        {
            return QueryIncluding(includes).FirstOrDefault(condition);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> condition, params Expression<Func<TEntity, object>>[] includes)
        {
            return QueryIncluding(includes).SingleOrDefault(condition);
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }

        public void ClearContext()
        {
            if (this.dbContext == null) return;

            var set = this.dbContext.Set<TEntity>();
            if (set == null || set.Local == null) return;

            foreach (var entity in set.Local.ToList())
            {
                var entry = this.dbContext.Entry(entity);
                if (entry != null && entry.State != EntityState.Deleted)
                {
                    entry.State = EntityState.Detached;
                }
            }
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return GetAll().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetAll().GetEnumerator();
        }

        public Expression Expression
        {
            get { return GetAll().Expression; }
        }

        public Type ElementType
        {
            get { return GetAll().ElementType; }
        }

        public IQueryProvider Provider
        {
            get { return GetAll().Provider; }
        }
    }
}
