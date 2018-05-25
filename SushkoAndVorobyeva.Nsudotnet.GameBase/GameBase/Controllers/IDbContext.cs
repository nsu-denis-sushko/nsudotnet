using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace GameBase.Controllers
{
    public interface IDbContext
    {
        DbEntityEntry Entry(Object entity);
        Int32 SaveChanges();
        DbSet Set(Type entityType);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        void Dispose();
    }
}
