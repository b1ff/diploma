using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;

namespace Gamification.Web.AutoMapper.Extensions
{
    public static class MappingExtensions
    {
        public static IEnumerable<TDest> MapEnumerable<TSource, TDest>(
            this Controller controller, IEnumerable<TSource> source)
        {
            return Mapper.Map<IEnumerable<TSource>, IEnumerable<TDest>>(source);
        }
    }
}