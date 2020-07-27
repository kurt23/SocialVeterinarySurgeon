using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using SocialVeterinarySurgeon.DAL;
using SocialVeterinarySurgeon.Domain.Entities;

namespace SocialVeterinarySurgeon.BL.Services
{
    public abstract class BaseService<TEntity> where TEntity : BaseEntity
    {
        protected readonly SocialVeterinarySurgeonDbContext _db;
        private readonly IMapper _mapper;
        private readonly ILogger<BaseService<TEntity>> _logger;

        protected BaseService(SocialVeterinarySurgeonDbContext db,
            IMapper mapper,
            ILogger<BaseService<TEntity>> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);

            _db.Set<TEntity>().Attach(entity);
            _db.Set<TEntity>().Remove(entity);
            await _db.SaveChangesAsync();
            _logger.LogInformation("{Entity} with {id} was successfully deleted",typeof(TEntity).ShortDisplayName(), id);
        }

        public virtual async Task<TEntity> Upsert(TEntity entity)
        {
            TEntity fromDb = null;
            
            if (entity.Id == default)
            {
                _db.Set<TEntity>().Add(entity);
            }

            else
            {
                fromDb = await GetById(entity.Id);
                _mapper.Map(entity, fromDb);
                _db.Set<TEntity>().Update(fromDb);
            }

            await _db.SaveChangesAsync();

            _logger.LogInformation(fromDb == null ? "new {entity} was added with {id}" : "new {entity} with {id} was changed",
                typeof(TEntity).ShortDisplayName(), entity.Id);

            return fromDb;
        }
        
        public virtual async Task<TEntity> GetById(int id)
        {
            var entity = await _db.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);

            if (entity == null)
            {
                _logger.LogError("There is no {entity} with {id!}", typeof(TEntity).ShortDisplayName(), id);
                throw new Exception($"There is no {typeof(TEntity).ShortDisplayName()} with id: {id}");
            }

            return entity;
        }

        public virtual async Task<IEnumerable<TEntity>> GetList()
        {
            return await _db.Set<TEntity>()
                .OrderBy(e => e.Id)
                .ToListAsync();
        }
    }
}
