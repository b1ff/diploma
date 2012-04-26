using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using Gamification.Core.Entities;
using Gamification.Core.Entities.Constraints;
using Gamification.Core.Entities.Triggers;

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
            OverrideMappings(modelBuilder);
        }

        private static void RegisterAllEntities(DbModelBuilder modelBuilder, Assembly assembly, IEnumerable<Type> baseTypes)
        {
            var method = typeof(DbModelBuilder).GetMethod("Entity");

            assembly
                .GetTypes()
                .Where(t => baseTypes.Any(ty => ty.IsAssignableFrom(t) && !t.IsAbstract))
                .ToList()
                .ForEach(t => method.MakeGenericMethod(t).Invoke(modelBuilder, null));
        }

        private static void OverrideMappings(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaseQuantityBasedConstraint>()
                .Property(x => x.BooleanOperationId)
                .HasColumnName("BooleanOperation");
            modelBuilder.Entity<BaseQuantityBasedConstraint>()
                .Ignore(x => x.BooleanOperation);

            modelBuilder.Entity<BaseStringCollectionConstraint>()
                .Ignore(x => x.CollectionEqualityOperation);
            modelBuilder.Entity<BaseStringCollectionConstraint>()
                .Property(x => x.CollectionEqualityOperationId).HasColumnName("CollectionEqualityOperation");

            modelBuilder.Entity<PointsTrigger>()
                .Ignore(x => x.PointsOperation);
            modelBuilder.Entity<PointsTrigger>()
                .Property(x => x.PointsOperationId)
                .HasColumnName("PointsOperation");
        }
    }
}
