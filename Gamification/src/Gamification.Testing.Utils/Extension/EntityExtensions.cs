using System;
using Gamification.Core.Entities;

namespace Gamification.Testing.Utils.Extension
{
    public static class EntityExtensions
    {
        public static TEntity WithId<TEntity>(this TEntity entity, int idToAssign = 1)
            where TEntity : BaseEntity
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            var idProp = typeof (BaseEntity).GetProperty("Id");
            idProp.SetValue(entity, idToAssign, null);
            return entity;
        }
    }
}
