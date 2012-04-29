using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Gamification.Core.Entities;
using Gamification.Web.Utils.CommonViewModels;

namespace Gamification.Web.AutoMapper.Mappings
{
    public class SelectListItemViewModelConfiguration : IAutomapperConfiguration
    {
        public void BuildMappings()
        {
            BuildSelectMappingFor<Project>(x => x.Id, x => x.Title);
            BuildSelectMappingFor<Achievement>(x => x.Id, x => x.Name);
            BuildSelectMappingFor<GamerStatus>(x => x.Id, x => x.StatusName);
        }

        private static void BuildSelectMappingFor<T>(
            Expression<Func<T, int>> valExpr, Expression<Func<T, object>> textExpr)
        {
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

                                  dataSource.Id = dataSource.First().Value;
                              });
        }
    }
}