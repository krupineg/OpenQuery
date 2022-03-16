using System.Reflection;

namespace OpenQuery.Core
{
    public static class QueryFieldsCache
    {
        private static readonly Dictionary<Type, string[]> Props = new();

        public static string[] GetProperties(Type type)
        {
            lock (Props)
            {
                if (!Props.ContainsKey(type))
                {
                    Props.Add(type, type.GetRuntimeProperties().Select(x => x.Name).ToArray());
                }
                return Props[type];
            }
        }
    }
}