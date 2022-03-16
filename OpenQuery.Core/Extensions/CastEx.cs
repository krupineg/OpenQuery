using OpenQuery.Core.Abstract;

namespace OpenQuery.Core.Extensions
{
    internal static class CastEx
    {
        public static T Cast<T>(this IQueryBase q) where T : class, IQueryBase
        {
            return (q as T)!;
        }
    }
}