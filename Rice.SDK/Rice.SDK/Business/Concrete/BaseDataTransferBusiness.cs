using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Rice.SDK.Business.Contract;
using Rice.SDK.Domain.Contract;
using Rice.SDK.Exceptions.Api;
using Rice.SDK.Repository.Contract;
using Rice.SDK.Utils;

namespace Rice.SDK.Business.Concrete
{
    /// <summary>
    /// A basic Business that returns DTOs based on some entity
    /// </summary>
    /// <typeparam name="TEntity">Base entity type</typeparam>
    /// <typeparam name="TDataTransfer">Data Transfer Object (DTO) based on the entity</typeparam>
    /// <typeparam name="TContext">Entity framework context</typeparam>
    public class
        BaseDataTransferBusiness<TDataTransfer, TEntity, TContext> : IDataTransferBusiness<TDataTransfer, TEntity>
        where TDataTransfer : IDataTransferEntity<TEntity>
        where TEntity : class, IIdentifiableEntity
        where TContext : DbContext
    {
        protected readonly DbContext Context;
        protected readonly IRepository<TEntity, TContext> Repository;
        protected readonly IMapper Mapper;

        public BaseDataTransferBusiness(IRepository<TEntity, TContext> repository, IMapper mapper, DbContext context)
        {
            Repository = repository;
            Mapper = mapper;
            Context = context;
        }

        public virtual async Task<IEnumerable<TDataTransfer>> ListAll(ClaimsPrincipal user = null)
        {
            var entityList = await Repository.ListAll();
            return Mapper.Map<IEnumerable<TDataTransfer>>(entityList);
        }
        
        public virtual async Task<IEnumerable<TDataTransfer>> ListByExpression(
            Expression<Func<TEntity, bool>> expression,
            ClaimsPrincipal user = null)
        {
            var entityList = await Repository.ListByExpression(expression);
            return Mapper.Map<IEnumerable<TDataTransfer>>(entityList);
        }

        public virtual async Task<IEnumerable<TDataTransfer>> ListByFilter(
            FilterParameters filterParameters,
            ClaimsPrincipal user = null)
        {
            var entityList = await Repository.ListByFilter(filterParameters);
            return Mapper.Map<IEnumerable<TDataTransfer>>(entityList);
        }

        public virtual async Task<TDataTransfer> GetById(int id, 
            ClaimsPrincipal user = null)
        {
            var entity = await Repository.GetById(id);
            return Mapper.Map<TDataTransfer>(entity);
        }

        public virtual async Task<int> Save(TDataTransfer dto, 
            ClaimsPrincipal user = null)
        {
            TEntity entity;
            if (dto.Id != 0)
            {
                entity = await Repository.GetById(dto.Id);
                entity = dto.Update(entity);
            }
            else
                entity = dto.Create();

            return await Repository.Save(entity);
        }

        public virtual async Task<int> Delete(TDataTransfer dto,
            ClaimsPrincipal user = null)
        {
            if (dto.Id == 0)
                throw new NotFoundException();

            var entity = dto.Create();
            return await Repository.Delete(entity);
        }
    }
}