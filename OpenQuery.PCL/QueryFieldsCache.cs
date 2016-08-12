using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OpenQuery.PCL
{
    public class QueryFieldsCache
    {
        private static readonly Dictionary<Type, string[]> Props = new Dictionary<Type, string[]>();

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