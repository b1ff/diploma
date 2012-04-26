using System;
using System.Linq.Expressions;
using Gamification.Core.Entities;
using LinqSpecs;

namespace Gamification.Core.Specifications
{
    public class UserByEmailSpec : Specification<User>
    {
        private readonly string email;

        public UserByEmailSpec(string email)
        {
            this.email = email;
        }

        public override Expression<Func<User, bool>> IsSatisfiedBy()
        {
            return x => x.Email == this.email;
        }
    }
}