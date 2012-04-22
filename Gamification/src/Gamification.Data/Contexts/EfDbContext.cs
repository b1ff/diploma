using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using Gamification.Core.Entities;

namespace Gamification.Data.EF.Contexts
{
    public class EfDbContext : DbContext
    {
        public EfDbContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EfDbContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            RegisterAllEntities(modelBuilder, typeof(BaseEntity).Assembly, new[] { typeof(BaseEntity) });
        }

        public static void RegisterAllEntities(DbModelBuilder modelBuilder, Assembly assembly, IEnumerable<Type> baseTypes)
        {
            var method = typeof(DbModelBuilder).GetMethod("Entity");

            assembly
                .GetTypes()
                .Where(t => baseTypes.Any(ty => ty.IsAssignableFrom(t) && !t.IsAbstract))
                .ToList()
                .ForEach(t => method.MakeGenericMethod(t).Invoke(modelBuilder, new object[0]));
        }
    }
}
