using System;
using System.Linq;
using System.Linq.Expressions;
using HelpDesk.Entities.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Entities.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected HelpDeskContext HelpDeskContext { get; set; }

        public RepositoryBase(HelpDeskContext helpDeskContext)
        {
            this.HelpDeskContext = helpDeskContext;
        }

        public void Create(T entity)
        {
            this.HelpDeskContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            this.HelpDeskContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll()
        {
            return this.HelpDeskContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.HelpDeskContext.Set<T>().Where(expression);
        }

        public void Update(T entity)
        {
            this.HelpDeskContext.Set<T>().Update(entity);
        }
    }
}
