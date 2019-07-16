using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;

namespace Rice.SDK.Utils
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> FilterBy<T>(this IQueryable<T> queryable,
            FilterParameters filterParameters)
        {
            if (filterParameters == null)
                return queryable;

            return queryable
                .Where(filterParameters)
                .OrderBy(filterParameters)
                .Paginate(filterParameters);
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> queryable, 
            FilterParameters filterParameters)
        {
            return filterParameters?.OrderBy == null
                ? queryable 
                : queryable.OrderBy(filterParameters.OrderBy + " " + filterParameters.Orientation);
        }

        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, FilterParameters filterParameters)
        {
            return (filterParameters == null || filterParameters.Page == 0 || filterParameters.PageSize == 0)
                ? queryable
                : queryable.Skip(filterParameters.PageSize * (filterParameters.Page - 1))
                    .Take(filterParameters.PageSize);
        }

        public static IQueryable<T> Where<T>(this IQueryable<T> queryable, FilterParameters filterParameters)
        {
            if (filterParameters?.Clauses == null)
                return queryable;

            return filterParameters
                .Clauses
                .Aggregate(queryable,
                    (current, clause) => current.Where(clause));
        }
    }
}
