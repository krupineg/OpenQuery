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
        return (implementation) => implementation.WildCard;
    }

    public SelectExpression Fields(params string[] fields)
    {
        Contract.Assert(fields.Length > 0, "you should provide non-empty fields list");
        return (implementation) => implementation.JoinFields(_availableFields.Intersect(fields).ToList());
    }
    
    public SelectExpression Function(string function, params string[] arguments)
    {
        return (implementation) => new StringBuilder(function)
            .Append(implementation.OpenSubquery)
            .Append(implementation.JoinFields(arguments))
            .Append(implementation.CloseSubquery)
            .ToString();
    }
            
    public SelectExpression Count()
    {
        return (implementation) => $"{implementation.Count}{implementation.OpenSubquery}{implementation.WildCard}{implementation.CloseSubquery}";
    }
}