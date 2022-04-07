using System.Diagnostics.Contracts;
using OpenQuery.Core.Abstract.Clauses.Select;

namespace OpenQuery.Core.Clauses;

internal sealed class SelectClauseFactory : ISelectClauseFactory
{
    private readonly FunctionCallClauseFactory _functionCallClauseFactory;

    public SelectClauseFactory(FunctionCallClauseFactory functionCallClauseFactory)
    {
        _functionCallClauseFactory = functionCallClauseFactory;
    }
    
    public SelectExpression Everything()
    {
        return (dialect, _) => dialect.WildCard;
    }

    public SelectExpression Fields(params string[] fields)
    {
        Contract.Assert(fields.Length > 0, "you should provide non-empty fields list");
        return (dialect, availableFields) => 
            dialect.JoinFields(fields.Where(x =>  dialect.WildCard.Equals(x) || availableFields.Contains(x)).ToList());
    }

    public SelectExpression Function(string function, params string[] arguments)
    {
        return (dialect, _) => _functionCallClauseFactory.Build(dialect, function, arguments);
    }
            
    public SelectExpression Count()
    {
        return (dialect, _) => $"{dialect.Count}{dialect.OpenSubquery}{dialect.WildCard}{dialect.CloseSubquery}";
    }
}