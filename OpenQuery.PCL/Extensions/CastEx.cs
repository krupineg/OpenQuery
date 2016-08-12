using OpenQuery.PCL.Abstract;

namespace OpenQuery.PCL.Extensions
{
    internal static class CastEx
    {
        public static T Cast<T>(this IQueryBase q) where T : class, IQueryBase
        {
            return q as T;
        }
    }
}