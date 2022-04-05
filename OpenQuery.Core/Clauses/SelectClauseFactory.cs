using System.Diagnostics.Contracts;
using System.Text;
using OpenQuery.Core.Abstract.Clauses.Select;

namespace OpenQuery.Core.Clauses;

internal sealed class SelectClauseFactory : ISelectClauseFactory
{
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
        return (dialect, _) => new StringBuilder(function)
            .Append(dialect.OpenSubquery)
            .Append(dialect.JoinFields(arguments))
            .Append(dialect.CloseSubquery)
            .ToString();
    }
            
    public SelectExpression Count()
    {
        return (dialect, _) => $"{dialect.Count}{dialect.OpenSubquery}{dialect.WildCard}{dialect.CloseSubquery}";
    }
}