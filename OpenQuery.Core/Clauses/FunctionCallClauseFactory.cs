using System.Text;
using OpenQuery.Core.Abstract.Dialect;

namespace OpenQuery.Core.Clauses;

internal class FunctionCallClauseFactory
{
    public string Build(ISqlDialect dialect, string function, params string[] arguments)
    {
        return new StringBuilder(function)
            .Append(dialect.OpenSubquery)
            .Append(dialect.JoinFields(arguments))
            .Append(dialect.CloseSubquery)
            .ToString();
    }
}