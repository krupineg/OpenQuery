using System.Diagnostics.Contracts;
using System.Text;

namespace OpenQuery.Core.Abstract;

public delegate string SelectExpression(ISqlDialect dialect);
public class SelectClauseFactory
{
    private readonly ISet<string> _availableFields;

    internal SelectClauseFactory(ISet<string> availableFields)
    {
        _availableFields = availableFields;
    }
        
    public SelectExpression Everything()
    {
        return (dialect) => dialect.WildCard;
    }

    public SelectExpression Fields(params string[] fields)
    {
        Contract.Assert(fields.Length > 0, "you should provide non-empty fields list");
        return (dialect) => dialect.JoinFields(_availableFields.Intersect(fields).ToList());
    }
    
    public SelectExpression Function(string function, params string[] arguments)
    {
        return (dialect) => new StringBuilder(function)
            .Append(dialect.OpenSubquery)
            .Append(dialect.JoinFields(arguments))
            .Append(dialect.CloseSubquery)
            .ToString();
    }
            
    public SelectExpression Count()
    {
        return (dialect) => $"{dialect.Count}{dialect.OpenSubquery}{dialect.WildCard}{dialect.CloseSubquery}";
    }
}