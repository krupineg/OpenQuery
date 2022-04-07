using System.Linq.Expressions;
using System.Reflection;

namespace OpenQuery.Core.Reflection
{
    public static class PropertyExtractor
    {
        public static PropertyInfo GetPropertyInfo<TSource, TProperty>(
            Expression<Func<TSource, TProperty>> propertyLambda)
        {
            var type = typeof(TSource).GetTypeInfo();

            var member = propertyLambda.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException($"Expression '{propertyLambda}' refers to a method, not a property.");

            var propInfo = (member.Member as PropertyInfo);
            if (propInfo == null)
                throw new ArgumentException($"Expression '{propertyLambda}' refers to a field, not a property.");

            if (type != propInfo.DeclaringType.GetTypeInfo() &&
                !type.IsSubclassOf(propInfo.DeclaringType))
                throw new ArgumentException($"Expression '{propertyLambda}' refers to a property that is not from type {type}.");

            return propInfo;
        }
    }
}