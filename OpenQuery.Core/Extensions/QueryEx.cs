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
                .Where(new WhereEqual<TProperty>(
                    query.Cast<IQueryBaseHidden>().Implementation, 
                    PropertyExtractor.GetPropertyInfo(func).Name, 
                    value));
            return query.Cast<IAvailableNewWhereClause>();
        }

        public static ISelectedQuery Select(this IQueryBase query, params  string[] fields)
        {
            return query.Cast<IQueryHidden>().Select(fields);
        }

        public static ISelectedQuery Select(this IQueryBase query, List<string> fields)
        {
            return Select(query, fields.ToArray());
        }

        public static IFromQuery From<T>(this ISelectedQuery query)
        {
            return query.Cast<ISelectedQueryHidden>().From<T>().Cast<IFromQuery>();
        }

        public static IAvailableNewWhereClause IsGreater<TSource, TProperty>
            (this IAvailableWhereQuery query, Expression<Func<TSource, TProperty>> func, TProperty value)
        {
            query.Cast<IWhereQuery>()
                .Where(new WhereGreater<TProperty>(query.Cast<IQueryBaseHidden>().Implementation,
                    PropertyExtractor.GetPropertyInfo(func).Name,
                    value));
            return query.Cast<IAvailableNewWhereClause>();
        }

        public static IAvailableNewWhereClause IsLesser<TSource, TProperty>
            (this IAvailableWhereQuery query, Expression<Func<TSource, TProperty>> func, TProperty value)
        {
            query.Cast<IWhereQuery>()
                .Where(new WhereLesser<TProperty>(query.Cast<IQueryBaseHidden>().Implementation,
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
                query.Cast<IQueryBaseHidden>().Implementation.CreateIn(value));
            var right = sb.ToString();
            query.Cast<IWhereQuery>()
                .Where(new WhereIn<string>(query.Cast<IQueryBaseHidden>().Implementation,
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
                query.Cast<IQueryBaseHidden>().Implementation.CreateIn(value));
            var right = sb.ToString();
            query.Cast<IWhereQuery>()
                .Where(new WhereNotIn<string>(query.Cast<IQueryBaseHidden>().Implementation,
                    PropertyExtractor.GetPropertyInfo(func).Name, 
                    right));
            return query.Cast<IAvailableNewWhereClause>();
        }

        public static IAvailableNewWhereClause IsLike<TSource, TProperty>
            (this IAvailableWhereQuery query, Expression<Func<TSource, TProperty>> func, TProperty value)
        {
            query.Cast<IWhereQuery>()
                .Where(new WhereLike<TProperty>(query.Cast<IQueryBaseHidden>().Implementation,
                    PropertyExtractor.GetPropertyInfo(func).Name,
                    value));
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