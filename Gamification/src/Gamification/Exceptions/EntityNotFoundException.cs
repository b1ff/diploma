using System;

namespace Gamification.Core.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entityName)
        {
            EntityName = entityName;
        }

        public string EntityName { get; set; }
    }
}
