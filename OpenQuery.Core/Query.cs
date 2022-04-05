using OpenQuery.Core.Abstract;
using OpenQuery.Core.Abstract.Dialect;
using OpenQuery.Core.Abstract.Query;

//[assembly: System.Reflection.AssemblyVersion("0.0.*")]
namespace OpenQuery.Core
{
    public static class Query
    {
        public static IQueryBase With<TDialect>() where TDialect : ISqlDialect, new()
        {
            return new Q<TDialect>();
        }
    }
}