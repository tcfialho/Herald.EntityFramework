using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Herald.EntityFramework.Extensions
{
    public enum SortDirection
    {
        Ascending,
        Descending
    }

    public static class PagedAndSortedExtension
    {
        public static IEnumerable<T> ToPaged<T>(this IEnumerable<T> context, int page, int size, out int totalSize)
        {
            int startIndex = page * size;

            totalSize = context.AsQueryable().Count();

            return context.AsQueryable().Skip(startIndex).Take(size).ToList();
        }

        public static IEnumerable<T> ToPaged<T>(this IEnumerable<T> context, int page, int size, bool includeUnloadeditems = false)
        {
            int startIndex = page * size;
            int endIndex = startIndex + size;

            int totalSize = context.AsQueryable().Count();
            var pagedList = context.AsQueryable().Skip(startIndex).Take(size);

            if (includeUnloadeditems)
            {
                for (int i = 0; i < startIndex; i++)
                {
                    yield return default;
                }
            }

            foreach (var item in pagedList)
            {
                yield return item;
            }

            if (includeUnloadeditems)
            {
                for (int i = endIndex; i < totalSize; i++)
                {
                    yield return default;
                }
            }
        }

        public static IEnumerable<T> ToPagedAndSorted<T, TKey>(this IEnumerable<T> context, int page, int size, out int totalSize, Expression<Func<T, TKey>> sortExpression, SortDirection sortDirection = SortDirection.Ascending)
        {
            var pagedList = (sortDirection == SortDirection.Ascending) ?
                context.AsQueryable().OrderBy(sortExpression).ToPaged(page, size, out totalSize) :
                context.AsQueryable().OrderByDescending(sortExpression).ToPaged(page, size, out totalSize);

            return pagedList.ToList();
        }

        public static IEnumerable<T> ToPagedAndSorted<T, TKey>(this IEnumerable<T> context, int page, int size, Expression<Func<T, TKey>> sortExpression, SortDirection sortDirection = SortDirection.Ascending, bool includeUnloadeditems = true)
        {
            var pagedList = (sortDirection == SortDirection.Ascending) ?
                context.AsQueryable().OrderBy(sortExpression).ToPaged(page, size, includeUnloadeditems) :
                context.AsQueryable().OrderByDescending(sortExpression).ToPaged(page, size, includeUnloadeditems);

            return pagedList.ToList();
        }
    }
}
