using AutoMapper;
using AutoMapper.QueryableExtensions;
using LowCostFligtsBrowser.Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LowCostFligtsBrowser.Application.Common.Mappings
{
    public static class MappingExtensions
    {
        public static PaginatedList<TDestination> PaginatedListOfData<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
         => PaginatedList<TDestination>.Create(queryable, pageNumber, pageSize);
        public static Task<PaginatedList<TDestination>> PaginatedListOfDataAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
            => PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize);

        public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, IConfigurationProvider configuration)
            => queryable.ProjectTo<TDestination>(configuration).ToListAsync();
    }
}
