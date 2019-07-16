using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rice.SDK.Business.Contract;
using Rice.SDK.Domain.Contract;
using Rice.SDK.Exceptions.Api;
using Rice.SDK.Repository.Contract;
using Rice.SDK.Utils;

namespace Rice.SDK.Business.Concrete
{
    public class BaseBusiness<T, TContext> : IBusiness<T>
        where T : class, IIdentifiableEntity
        where TContext : DbContext
    {
        protected readonly IRepository<T, TContext> Repository;
        protected readonly DbContext Context;

        public BaseBusiness(IRepository<T, TContext> repository, DbContext context)
        {
            Repository = repository;
            Context = context;
        }

        public virtual async Task<IEnumerable<T>> ListAll(ClaimsPrincipal user = null)
        {
            return await Repository.ListAll();
        }

        public virtual async Task<IEnumerable<T>> ListByExpression(Expression<Func<T, bool>> expression, 
            ClaimsPrincipal user = null)
        {
            return await Repository
                .ListByExpression(expression);
        }
        
        public virtual async Task<IEnumerable<T>> ListByExpression(Expression<Func<T, bool>> expression, 
            params Expression<Func<T, object>>[] includeProperties)
        {
            return await Repository
                .ListByExpression(expression, includeProperties);
        }
        

        public virtual async Task<IEnumerable<T>> ListByFilter(FilterParameters filterParameters, 
            ClaimsPrincipal user = null)
        {
            return await Repository
                .ListByFilter(filterParameters);
        }

        public virtual async Task<T> GetById(int id, 
            ClaimsPrincipal user = null)
        {
            return await Repository.GetById(id);
        }

        public virtual async Task<int> Save(T entity, 
            ClaimsPrincipal user = null)
        {
            return await Repository.Save(entity);
        }

        public virtual async Task<int> Delete(T entity, 
            ClaimsPrincipal user = null)
        {
            return await Repository.Delete(entity);
        }
        
        protected void ValidateModel(object model)
        {
            if (model == null)
                return;
            
            var results = new List<ValidationResult>();
            var context = new ValidationContext(model, null, null);

            if (!Validator.TryValidateObject(model, context, results, true))
                throw new BadRequestException(results);
        }
    }
}
