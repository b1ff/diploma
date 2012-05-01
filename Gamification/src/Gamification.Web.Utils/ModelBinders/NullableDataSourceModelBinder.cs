using Gamification.Web.Utils.CommonViewModels;

namespace Gamification.Web.Utils.ModelBinders
{
    public class NullableDataSourceModelBinder : DataSourceModelBinder
    {
        protected override DataSource CreateDataSource(int id)
        {
            if (id < 0)
                return null;
            return new NullableDataSource(id);
        }
    }
}