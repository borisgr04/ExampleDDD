using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Data.Base
{
    public interface IDbContext  {
        DbSet<T> Set<T>() where T : class;
        Action<string> Log { get; set; }
        DbEntityEntry Entry(object entity);
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        int SaveChanges();
    }
    
    public class DbContextBase : DbContext, IDbContext
    {
        public DbContextBase(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            
        }
        public DbContextBase(DbConnection connection)
          : base(connection, true)
        {
            Configuration.LazyLoadingEnabled = false;
        }
        public Action<string> Log {
            get {
                return this.Database.Log;
            }
            set {
                this.Database.Log = value;
            }
        }
    }
}
