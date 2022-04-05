using OpenQuery.Core.Abstract.Dialect;

namespace OpenQuery.Core.Abstract.Clauses.Select;

public interface ISelectClauseFactory
{
    SelectExpression Everything();
    SelectExpression Fields(params string[] fields);
    SelectExpression Function(string function, params string[] arguments);
    SelectExpression Count();
}

public delegate string SelectExpression(ISqlDialect dialect, IReadOnlyCollection<string> availableFields);