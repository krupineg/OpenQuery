using System.Linq.Expressions;
using System.Text;
using OpenQuery.Core.Abstract;
using OpenQuery.Core.Tokens;

namespace OpenQuery.Core.Extensions
{
    public static class QueryEx
    {
        public static IAvailableWhereQuery Where(this IFromQuery query)
        {
            return (query as IAvailableWhereQuery)!;
        }

        public static IAvailableNewWhereClause AreEqual<TSource, TProperty>(this IAvailableWhereQuery query,
            Expression<Func<TSource, TProperty>> func, TProperty value)
        {
            query.Cast<IWhereQuery>()
                .Where(new WhereEqual<TSource, TProperty>(
                    query.Cast<IQueryBaseHidden>().Dialect, 
                    PropertyExtractor.GetPropertyInfo(func).Name, 
                    value));
            return query.Cast<IAvailableNewWhereClause>();
        }
        
        public static IAvailableNewWhereClause AreEqual<TSource>(this IAvailableWhereQuery query,
            Expression<Func<TSource, string>> func, string value)
        {
            return AreEqual<TSource, string>(query, func, $"'{value}'");
        }
        
        public static IAvailableNewWhereClause AreEqual<TSource>(this IAvailableWhereQuery query,
            Expression<Func<TSource, int>> func, int value)
        {
            return AreEqual<TSource, int>(query, func, value);
        }

        public static IAvailableNewWhereClause AreEqual<TSource>(this IAvailableWhereQuery query,
            Expression<Func<TSource, long>> func, long value)
        {
            return AreEqual<TSource, long>(query, func, value);
        }
        public static IReadyToBuildQuery As(this IAvailableNewWhereClause query,
            string alias)
        {
            return query.Cast<IHaveWhereClause>().As(alias);
        }
        
        public static ISelectedQuery Select(this IQueryBase query, Func<SelectClauseFactory, SelectExpression> func)
        {
            return query.Cast<IQueryHidden>().Select(func);
        }

        public static IFromQuery From<T>(this ISelectedQuery query)
        {
            return query.Cast<ISelectedQueryHidden>().From<T>().Cast<IFromQuery>();
        }
        
        public static IFromQuery From<T>(this ISelectedQuery query, string domain)
        {
            return query.Cast<ISelectedQueryHidden>().From<T>(domain).Cast<IFromQuery>();
        }

        public static IAvailableNewWhereClause IsGreater<TSource, TProperty>
            (this IAvailableWhereQuery query, Expression<Func<TSource, TProperty>> func, TProperty value)
        {
            query.Cast<IWhereQuery>()
                .Where(new WhereGreater<TSource, TProperty>(query.Cast<IQueryBaseHidden>().Dialect,
                    PropertyExtractor.GetPropertyInfo(func).Name,
                    value));
            return query.Cast<IAvailableNewWhereClause>();
        }

        public static IAvailableNewWhereClause IsLess<TSource, TProperty>
            (this IAvailableWhereQuery query, Expression<Func<TSource, TProperty>> func, TProperty value)
        {
            query.Cast<IWhereQuery>()
                .Where(new WhereLess<TSource, TProperty>(query.Cast<IQueryBaseHidden>().Dialect,
                    PropertyExtractor.GetPropertyInfo(func).Name, 
                    value));
            return query.Cast<IAvailableNewWhereClause>();
        }

        public static IAvailableNewWhereClause 
            IsIn<TSource, TProperty>(this IAvailableWhereQuery query,
                Expression<Func<TSource, TProperty>> func,
                params TProperty[] value)
        {
            var sb = new StringBuilder();
            sb.Append(
                query.Cast<IQueryBaseHidden>().Dialect.CreateIn(value));
            var right = sb.ToString();
            query.Cast<IWhereQuery>()
                .Where(new WhereIn<TSource, string>(query.Cast<IQueryBaseHidden>().Dialect,
                    PropertyExtractor.GetPropertyInfo(func).Name,
                    right));
            return query.Cast<IAvailableNewWhereClause>();
        }

        public static IAvailableNewWhereClause IsNotIn<TSource, TProperty>
            (this IAvailableWhereQuery query, Expression<Func<TSource, TProperty>> func,
                params TProperty[] value)
        {
            var sb = new StringBuilder();
            sb.Append(
                query.Cast<IQueryBaseHidden>().Dialect.CreateIn(value));
            var right = sb.ToString();
            query.Cast<IWhereQuery>()
                .Where(new WhereNotIn<TSource, string>(query.Cast<IQueryBaseHidden>().Dialect,
                    PropertyExtractor.GetPropertyInfo(func).Name, 
                    right));
            return query.Cast<IAvailableNewWhereClause>();
        }

        public static IAvailableNewWhereClause IsLike<TSource>
            (this IAvailableWhereQuery query, Expression<Func<TSource, string>> func, string value)
        {
            query.Cast<IWhereQuery>()
                .Where(new WhereLike<TSource, string>(query.Cast<IQueryBaseHidden>().Dialect,
                    PropertyExtractor.GetPropertyInfo(func).Name,
                    $"'{value}'"));
            return query.Cast<IAvailableNewWhereClause>();
        }
        
        public static IAvailableWhereQuery Or(this IAvailableNewWhereClause query)
        {            
            query.Cast<IHaveWhereClause>()
                .OrWhere();
            return query.Cast<IAvailableWhereQuery>();
        }

        public static IAvailableWhereQuery And(this IAvailableNewWhereClause query)
        {
            query.Cast<IHaveWhereClause>()
                .AndWhere();
            return query.Cast<IAvailableWhereQuery>(); 
        }
    }
}