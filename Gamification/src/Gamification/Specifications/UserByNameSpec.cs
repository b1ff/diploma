using System;
using System.Linq.Expressions;
using Gamification.Core.Entities;
using LinqSpecs;

namespace Gamification.Core.Specifications
{
    public class UserByNameSpec : Specification<User>
    {
        private readonly string username;

        public UserByNameSpec(string username)
        {
            this.username = username;
        }

        public override Expression<Func<User, bool>> IsSatisfiedBy()
        {
            return x => x.Username == username;
        }
    }
}
