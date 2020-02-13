using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using SynelTask.Core.Kendo;

namespace SynelTask.Core.Kendo
{
    public static class Extensions
    {
        /// <summary>
        /// Applies data processing (paging, sorting and filtering) over IQueryable using Dynamic Linq.
        /// </summary>
        /// <typeparam name="T">The type of the IQueryable</typeparam>
        /// <param name="queryable">The IQueryable which should be processed.</param>
        /// <param name="take">Specifies how many items to take. Configurable via the pageSize setting of the Kendo DataSource.</param>
        /// <param name="skip">Specifies how many items to skip.</param>
        /// <param name="sort">Specifies the current sort order.</param>
        /// <param name="filter">Specifies the current filter.</param>
        /// <param name="all">Specifies the current All.</param>
        /// <returns>A DataSourceResult object populated from the processed IQueryable.</returns>
        public static DataSourceResult<T> ToDataSourceResult<T>(this IQueryable<T> queryable, int take, int skip, IEnumerable<Sort> sort, Filter filter, bool all = false)
        {
            // Filter the data first
            queryable = Filter(queryable, filter);

            // Calculate the total number of records (needed for paging)
            var total = queryable.Count();

            // Sort the data
            queryable = Sort(queryable, sort);

            // Finally page the data
            if (!all)
                queryable = Page(queryable, take, skip);
            var res = queryable.ToDynamicList<T>();
            return new DataSourceResult<T>
            {
                Data = res,
                Total = total
            };
        }

        private static IQueryable<T> Filter<T>(IQueryable<T> queryable, Filter filter)
        {
            if (filter != null)
            {
                if (filter.Filters.Count() > 0)
                {
                    // Collect a flat list of all filters
                    var filters = filter.All();

                    // Get all filter values as array (needed by the Where method of Dynamic Linq)
                    var values = filters.Select(f => f.Value).ToArray();

                    // Create a predicate expression e.g. Field1 = @0 And Field2 > @1
                    string predicate = filter.ToExpression(filters);

                    // Use the Where method of Dynamic Linq to filter the data
                    queryable = queryable.Where(predicate, values);
                }
            }
            return queryable;
        }

        private static IQueryable<T> Sort<T>(IQueryable<T> queryable, IEnumerable<Sort> sort)
        {
            if (sort != null && sort.Any())
            {
                // Create ordering expression e.g. Field1 asc, Field2 desc
                var ordering = String.Join(",", sort.Select(s => s.ToExpression()));

                // Use the OrderBy method of Dynamic Linq to sort the data
                return queryable.OrderBy(ordering);
            }

            return queryable;
        }

        private static IQueryable<T> Page<T>(IQueryable<T> queryable, int take, int skip)
        {
            return queryable.Skip(skip).Take(take);
        }
    }

}
