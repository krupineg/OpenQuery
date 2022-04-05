using System.Reflection;

namespace OpenQuery.Core.Reflection
{
    public static class QueryFieldsCache
    {
        private static readonly Dictionary<Type, ISet<string>> Props = new();
        private static readonly object Lock = new ();

        public static ISet<string> GetProperties(Type type)
        {
            lock (Lock)
            {
                if (!Props.ContainsKey(type))
                {
                    Props.Add(type, type.GetRuntimeProperties().Select(x => x.Name).ToHashSet());
                }
                return Props[type];
            }
        }
    }
}