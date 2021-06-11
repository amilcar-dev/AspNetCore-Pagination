using Pagination.Enums;
using Pagination.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Pagination.Extensions
{
    public static class PaginationExtension
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, PaginationFilter filter)
        {
            if (filter.OrderBy == OrderBy.Ascending)
                query = query.OrderBy(ReturnSortExpression<T>(filter.PropertyName));
            else
                query = query.OrderByDescending(ReturnSortExpression<T>(filter.PropertyName));

            query = query.SearchText(filter.SearchText);

            return query.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
        }

        private static Expression<Func<T, object>> ReturnSortExpression<T>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T), propertyName);

            return Expression.Lambda<Func<T, object>>(Expression.Convert(Expression.Property(parameter, propertyName), typeof(object)), parameter);
        }

        private static IQueryable<T> SearchText<T>(this IQueryable<T> source, string term)
        {
            if (string.IsNullOrEmpty(term)) { return source; }

            // Get all the string property names on this specific type.
            var stringProperties = typeof(T).GetProperties().Where(x => x.PropertyType == typeof(string));

            if (!stringProperties.Any()) { return source; }

            // Build the string expression
            string filterExpr = string.Join(" || ", stringProperties.Select(prp => $"{prp.Name}.ToLower().Contains(@0)"));

            return source.Where(filterExpr, term.ToLower());
        }
    }
}
