using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using OpenQuery.Core.Abstract.Clauses.Where;
using OpenQuery.Core.Reflection;

namespace OpenQuery.Core.Clauses;

class WhereClauseFactory : IWhereClauseFactory
{
    private readonly FunctionCallClauseFactory _functionCallClauseFactory;

    public WhereClauseFactory(FunctionCallClauseFactory functionCallClauseFactory)
    {
        _functionCallClauseFactory = functionCallClauseFactory;
    }

    public WhereExpression Literal<T>(T value)
    {
        return dialect => typeof(T) == typeof(string) ? dialect.QuoteValue(value.ToString()) : value.ToString();
    }

    public WhereExpression Function(string name, params string[] parameters)
    {
        return (dialect) => _functionCallClauseFactory.Build(dialect, name, parameters);
    }

    public WhereExpression Property<T, TProperty>(Expression<Func<T, TProperty>> property)
    {
        var name = PropertyExtractor.GetPropertyInfo(property).Name;
        Contract.Assert(
            QueryFieldsCache.GetProperties(typeof(T)).Contains(name), 
            $"Field {name} is not available for {typeof(T).Name}");
        return (dialect) => name;
    }
}