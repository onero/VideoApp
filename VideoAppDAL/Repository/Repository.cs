﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VideoAppDAL.Interfaces;

namespace VideoAppDAL.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        protected readonly DbContext Context;

        protected Repository(DbContext context)
        {
            Context = context;
        }

        public TEntity Create(TEntity entity)
        {
            return Context.Add(entity).Entity;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public TEntity GetById(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public bool Delete(int id)
        {
            var entityToRemove = GetById(id);
            if (entityToRemove == null) return false;
            Context.Remove(entityToRemove);
            return true;
        }

        public TEntity Update(TEntity entity)
        {
            return Context.Update(entity).Entity;
        }

        public void ClearAll()
        {
            foreach (var entity in Context.Set<TEntity>())
            {
                Context.Remove(entity);
            }
        }
    }
}