using System.Linq.Expressions;
using OpenQuery.Core.Abstract.Dialect;
using OpenQuery.Core.Abstract.Tokens;

namespace OpenQuery.Core.Abstract.Clauses.Where;

public interface IWhereClauseFactory
{
    WhereExpression Function(string name, params string[] parameters);
    WhereExpression Property<T, TProperty>(Expression<Func<T, TProperty>> property);
}

public delegate string WhereExpression(ISqlDialect dialect);

public delegate IToken WhereFactoryExpression(IWhereClauseFactory factory);