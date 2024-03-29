using OpenQuery.Core.Abstract.Dialect;
using OpenQuery.Core.Abstract.Query;

namespace OpenQuery.Core.Abstract.Clauses.From;

public delegate (string, string[]) FromExpression(ISqlDialect dialect);

public interface IFromClauseFactory
{
    FromExpression WithTableName<T>(string alias);
    FromExpression Default<T>();
    FromExpression WithDomain<T>(string domain, params string[] domains);
    FromExpression WithTableNameWithDomain<T>(string alias, string[] domains);
    FromExpression From(IReadyToBuildQuery subQuery);
}