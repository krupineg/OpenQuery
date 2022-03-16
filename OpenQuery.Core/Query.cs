using OpenQuery.Core.Abstract;

namespace OpenQuery.Core
{
    public static class Query
    {
        public static IQueryBase With<TImplementation>() where TImplementation : ISqlImplementation, new()
        {
            return new Q<TImplementation>();
        }
    }
}