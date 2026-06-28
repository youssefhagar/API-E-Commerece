using E_Commerece.Domain.Contract;
using E_Commerece.Domain.Entites;
using E_Commerece.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerece.Infrastructure.Repository
{
    public class GenericRepository<TEntity, TKey>(StoreDbContext context) : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public void Add(TEntity entity)
            => context.Set<TEntity>().Add(entity);

        public void Delete(TEntity entity)
            => context.Set<TEntity>().Remove(entity);

        public async Task<IReadOnlyList<TEntity>?> GetAllAsync(CancellationToken ct = default!)
            => await context.Set<TEntity>().ToListAsync(ct);

        public async Task<TEntity?> GetByIdASync(int id, CancellationToken ct = default!)
            => await context.Set<TEntity>().FindAsync(id, ct);

        public void Update(TEntity entity)
           => context.Set<TEntity>().Update(entity);
    }
}
