using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using Gamification.Core.Entities;
using Gamification.Core.Entities.Constraints;
using Gamification.Core.Entities.Triggers;

namespace Gamification.Data.EF.Contexts
{
    public class EfDbContext : DbContext
    {
        public EfDbContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer(new DefaultUserInitializer<EfDbContext>());
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
            //modelBuilder.Entity<BaseNumericBasedConstraint>()
            //    .Property(x => x.BooleanOperationId)
            //    .HasColumnName("BooleanOperation");
            //modelBuilder.Entity<BaseNumericBasedConstraint>()
            //    .Ignore(x => x.BooleanOperation);

            //modelBuilder.Entity<BaseStringCollectionConstraint>()
            //    .Ignore(x => x.CollectionEqualityOperation);
            //modelBuilder.Entity<BaseStringCollectionConstraint>()
            //    .Property(x => x.CollectionEqualityOperationId).HasColumnName("CollectionEqualityOperation");

            //modelBuilder.Entity<ChangePointsTrigger>()
            //    .Ignore(x => x.PointsOperation);
            //modelBuilder.Entity<ChangePointsTrigger>()
            //    .Property(x => x.PointsOperationId)
            //    .HasColumnName("PointsOperation");

            //modelBuilder.Entity<AchievementsTrigger>()
            //    .Ignore(x => x.ActionWithAchievement);
            //modelBuilder.Entity<AchievementsTrigger>()
            //    .Property(x => x.ActionWithAchievementId)
            //    .HasColumnName("ActionWithAchievement");
            EnumFiledMapping<BaseNumericBasedConstraint>(modelBuilder, x => x.BooleanOperation, x => x.BooleanOperationId);
            EnumFiledMapping<BaseStringCollectionConstraint>(modelBuilder, x => x.CollectionEqualityOperation, x => x.CollectionEqualityOperationId);
            EnumFiledMapping<ChangePointsTrigger>(modelBuilder, x => x.PointsOperation, x => x.PointsOperationId);
            EnumFiledMapping<AchievementsTrigger>(modelBuilder, x => x.ActionWithAchievement, x => x.ActionWithAchievementId);

            modelBuilder.Entity<Gamer>().HasRequired(x => x.Project);
        }

        private static void EnumFiledMapping<TEntity>(DbModelBuilder modelBuilder, Expression<Func<TEntity, object>> enumField, Expression<Func<TEntity, byte>> enumIdExpr)
            where TEntity : BaseEntity
        {
            var columnName = ExpressionHelper.GetExpressionText(enumField);
            modelBuilder.Entity<TEntity>()
                .Ignore(enumField);
            modelBuilder.Entity<TEntity>()
                .Property(enumIdExpr)
                .HasColumnName(columnName);
        }
    }
}
