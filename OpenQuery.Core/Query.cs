using OpenQuery.Core.Abstract;

    //[assembly: System.Reflection.AssemblyVersion("0.0.*")]
namespace OpenQuery.Core
{
    public static class Query
    {
        public static IQueryBase With<TImplementation>() where TImplementation : ISqlDialect, new()
        {
            return new Q<TImplementation>();
        }
    }
}