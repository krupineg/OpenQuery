using System.Linq.Expressions;
using System.Reflection;

namespace OpenQuery.Core.Reflection
{
    public static class PropertyExtractor
    {
        public static PropertyInfo GetPropertyInfo<TSource, TProperty>(
            Expression<Func<TSource, TProperty>> propertyLambda)
        {
            TypeInfo type = typeof(TSource).GetTypeInfo();

            MemberExpression member = propertyLambda.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    propertyLambda.ToString()));

            PropertyInfo propInfo = (member.Member as PropertyInfo);
            if (propInfo == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a field, not a property.",
                    propertyLambda.ToString()));

            if (type != propInfo.DeclaringType.GetTypeInfo() &&
                !type.IsSubclassOf(propInfo.DeclaringType))
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a property that is not from type {1}.",
                    propertyLambda.ToString(),
                    type));

            return propInfo;
        }
    }
}