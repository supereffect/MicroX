using CQRSWebApiProject.DAL.Concrete.EntityFramework.Context;
using CQRSWebApiProject.Entity.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CQRSWebApiProject.DAL.Concrete.EntityFramework.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, IEntity, new()
    {
        private readonly ReadDbContext _readDbContext;
        private readonly WriteDbContext _writeDbContext;
        private readonly DbSet<TEntity> _writeEntities;
        private readonly DbSet<TEntity> _readEntities;

        public GenericRepository(ReadDbContext readDbContext, WriteDbContext writeDbContext)
        {
            _readDbContext = readDbContext;
            _writeDbContext = writeDbContext;
            _writeEntities = writeDbContext.Set<TEntity>();
            _readEntities = readDbContext.Set<TEntity>();
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            await _writeEntities.AddAsync(entity);
            return entity;
        }

        public async Task<TEntity> Find(Expression<Func<TEntity, bool>> conditions)
        {
            return await _readEntities.FirstOrDefaultAsync(conditions);
        }

        public void Delete(TEntity entity)
        {
            _writeEntities.Remove(entity);
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            if (entity == null) return null;

            TEntity exists = await _writeEntities.FindAsync(entity.Id);
            if (exists != null)
            {
                _writeDbContext.Entry(exists).CurrentValues.SetValues(entity);
            }
            return exists;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _writeDbContext.SaveChangesAsync();
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> expression)
        {
            return await _readEntities.FirstOrDefaultAsync(expression);
        }

        public async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null
                ? await _readEntities.ToListAsync()
                : await _readEntities.Where(expression).ToListAsync();
        }

        public IEnumerable<TEntity> GetQueryable()
        {
            return _readEntities.AsEnumerable();
        }
    }
}
