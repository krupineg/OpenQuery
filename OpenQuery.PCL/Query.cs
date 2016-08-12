using OpenQuery.PCL.Abstract;

namespace OpenQuery.PCL
{
    public static class Query
    {
        public static IQueryBase With<TImplementation>() where TImplementation : ISqlImplementation, new()
        {
            return new Q<TImplementation>();
        }
    }
}