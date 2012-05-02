using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Gamification.Core.Entities;
using Gamification.Core.Entities.Constraints;
using Gamification.Core.Entities.Triggers;
using Gamification.Core.Extensions;
using Gamification.Core.ProjectSettings;
using Gamification.Web.Utils.CommonViewModels;
using IConfiguration = Gamification.Core.ProjectSettings.IConfiguration;

namespace Gamification.Web.AutoMapper.Mappings
{
    public class DataSourceMapingConfiguration : IConfiguration
    {
        public void Configure()
        {
            BuildDataSourceMappingFor<Project>(x => x.Id, x => x.Title);
            BuildDataSourceMappingFor<Achievement>(x => x.Id, x => x.Name);
            BuildDataSourceMappingFor<GamerStatus>(x => x.Id, x => x.StatusName);
            BuildDataSourceMappingFor<ActionTrigger>(x => x.Id, x => x.Title);
            BuildDataSourceMappingFor<BaseStringCollectionConstraint>(x => x.Id, x => x.Description);
            BuildDataSourceMappingFor<BaseNumericBasedConstraint>(x => x.Id, x => x.Description);
        }

        private static void BuildDataSourceMappingFor<T>(
            Expression<Func<T, int>> valExpr, Expression<Func<T, object>> textExpr)
        {
            // todo: add mapping for nullable datasource.
            Mapper.CreateMap<T, NumericSelectListItem>()
                .ForMember(x => x.Value, opt => opt.MapFrom(valExpr))
                .ForMember(x => x.Text, opt => opt.MapFrom(textExpr))
                .ForMember(x => x.Selected, opt => opt.Ignore())
                ;

            Mapper.CreateMap<IEnumerable<T>, DataSource>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .AfterMap((coll, dataSource) =>
                              {
                                  foreach (var item in coll)
                                  {
                                      dataSource.Add(Mapper.Map<T, NumericSelectListItem>(item));
                                  }

                                  if (dataSource.IsNotEmpty())
                                    dataSource.Id = dataSource.First().Value;
                              });

            Mapper.CreateMap<IEnumerable<T>, NullableDataSource>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .AfterMap((coll, dataSource) =>
                {
                    dataSource.Add(NumericSelectListItem.Empty);
                    foreach (var item in coll)
                    {
                        dataSource.Add(Mapper.Map<T, NumericSelectListItem>(item));
                    }

                    if (dataSource.IsNotEmpty())
                        dataSource.Id = dataSource.First().Value;
                });
        }
    }
}