﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Gamification.Core.DataAccess;
using Gamification.Core.Entities;
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

        public TEntity GetById(int id)
        {
            return EntityPersister.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<TEntity> BySpec(Specification<TEntity> spec)
        {
            return GetAll().BySpec(spec);
        }

        public IEnumerable<TEntity> BySpecWithQuery(Specification<TEntity> spec)
        {
            return GetAll().BySpec(spec).ToList();
        }

        public TEntity FirstBySpec(Specification<TEntity> spec)
        {
            return GetAll().FirstBySpec(spec);
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

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> condition)
        {
            return GetAll().FirstOrDefault(condition);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> condition)
        {
            return GetAll().SingleOrDefault(condition);
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
    }
}