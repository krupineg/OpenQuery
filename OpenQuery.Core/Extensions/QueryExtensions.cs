using System.Linq.Expressions;
using OpenQuery.Core.Abstract.Clauses.From;
using OpenQuery.Core.Abstract.Clauses.Select;
using OpenQuery.Core.Abstract.Clauses.Where;
using OpenQuery.Core.Abstract.Query;
using OpenQuery.Core.Tokens;

namespace OpenQuery.Core.Extensions
{
    public static class QueryExtensions
    {
        public static IAvailableWhereQuery Where(this IFromQuery query)
        {
            return (query as IAvailableWhereQuery)!;
        }

        public static IAvailableNewWhereClause AreEqual<TSource, TProperty>(this IAvailableWhereQuery query,
            Expression<Func<TSource, TProperty>> func, TProperty value)
        {
            query.Cast<IWhereQuery>().Where(factory => new WhereEqual<TProperty>(factory.Property(func), value));
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
        
        public static ISelectedQuery Select(this IQueryBase query, Func<ISelectClauseFactory, SelectExpression> func)
        {
            return query.Cast<IQueryHidden>().Select(func);
        }
        
        public static IFromQuery From(this ISelectedQuery query, Func<IFromClauseFactory, FromExpression> func)
        {
            return query.Cast<ISelectedQueryHidden>().From(func).Cast<IFromQuery>();
        }
        
        public static IFromQuery From<T>(this ISelectedQuery query)
        {
            return query.Cast<ISelectedQueryHidden>().From(x => x.Default<T>()).Cast<IFromQuery>();
        }
        
        public static IAvailableNewWhereClause IsGreater<TSource, TProperty>
            (this IAvailableWhereQuery query, Expression<Func<TSource, TProperty>> func, TProperty value)
        {
            query.Cast<IWhereQuery>()
                .Where(factory => new WhereGreater<TProperty>(factory.Property(func), value));
            return query.Cast<IAvailableNewWhereClause>();
        }

        public static IAvailableNewWhereClause IsLess<TSource, TProperty>
            (this IAvailableWhereQuery query, Expression<Func<TSource, TProperty>> func, TProperty value)
        {
            query.Cast<IWhereQuery>().Where(factory => new WhereLess<TProperty>(factory.Property(func), value));
            return query.Cast<IAvailableNewWhereClause>();
        }
        
        public static IAvailableNewWhereClause IsLess<TProperty>
            (this IAvailableWhereQuery query, Func<IWhereClauseFactory, WhereExpression> expression, TProperty value)
        {
            query.Cast<IWhereQuery>().Where(factory => new WhereLess<TProperty>(expression(factory), value));
            return query.Cast<IAvailableNewWhereClause>();
        }

        public static IAvailableNewWhereClause 
            IsIn<TSource, TProperty>(this IAvailableWhereQuery query,
                Expression<Func<TSource, TProperty>> func,
                params TProperty[] value)
        {
            query.Cast<IWhereQuery>().Where(factory => new WhereIn<TProperty>(factory.Property(func), value));
            return query.Cast<IAvailableNewWhereClause>();
        }

        public static IAvailableNewWhereClause IsNotIn<TSource, TProperty>
            (this IAvailableWhereQuery query, Expression<Func<TSource, TProperty>> func,
                params TProperty[] value)
        {
            query.Cast<IWhereQuery>().Where(factory => new WhereNotIn<TProperty>(factory.Property(func), value));
            return query.Cast<IAvailableNewWhereClause>();
        }

        public static IAvailableNewWhereClause IsLike<TSource>
            (this IAvailableWhereQuery query, Expression<Func<TSource, string>> func, string value)
        {
            query.Where(factory => new WhereLike(factory.Property(func), value));
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

        private static T Cast<T>(this IQueryBase q) where T : class, IQueryBase
        {
            return (q as T)!;
        }
    }
}