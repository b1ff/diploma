using System.Web;
using Gamification.Core.Entities;

namespace Gamification.Web.Utils.Helpers
{
    public static class HttpExceptionsHelper
    {
        public static void ThrowNotFoundIfNull<TEntity>(this TEntity obj) 
            where TEntity : BaseEntity
        {
            if (obj == null)
                throw new HttpException(404, string.Format("Not found {0}", typeof(TEntity).Name));
        }
    }
}
