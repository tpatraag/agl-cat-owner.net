using System;
using System.Collections.Generic;
using System.Linq;

namespace AGL.CatOwner.Utility
{
    public static class LinQUtility
    {
        public static IEnumerable<TResult> SelectManyExceptNull<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> handler)
        {
            return source.Select(handler)
                .Where(item => item != null)
                .SelectMany(item => item);
        }
    }
}
