using System.Diagnostics.Contracts;
using OpenQuery.Core.Abstract.Clauses.From;
using OpenQuery.Core.Abstract.Query;
using OpenQuery.Core.Reflection;

namespace OpenQuery.Core.Clauses;

internal sealed class FromClauseFactory : IFromClauseFactory
{
    public FromExpression WithTableName(string alias)
    {
        return CreateExpression<Object>(alias, Array.Empty<string>());
    }
    
    public FromExpression Default<T>()
    {
        return CreateExpression<T>(typeof(T).Name, Array.Empty<string>());
    }
    
    public FromExpression WithDomain<T>(string domain, params string[] domains)
    {
        Contract.Assert(!string.IsNullOrEmpty(domain), "at least one domain should be provided");
        
        var total = domains.Prepend(domain).ToArray();
        return CreateExpression<T>(typeof(T).Name, total);
    }
    
    public FromExpression WithTableNameWithDomain(string alias, string[] domains)
    {
        Contract.Assert(domains.Length > 0, "you should provide at least one domain");
        return CreateExpression<Object>(alias, domains);
    }

    private FromExpression CreateExpression<T>(string tableName, string[] domain) 
    {
        return (dialect) => (
            string.Join(dialect.DomainSeparator, domain.Append(tableName)), 
            QueryFieldsCache.GetProperties(typeof(T)).ToArray()
        );
    }

    public FromExpression From(IReadyToBuildQuery subQuery)
    {
        return (dialect) => (
            $"{dialect.OpenSubquery}{subQuery.Build()}{dialect.CloseSubquery}", 
            subQuery.Source(dialect).Item2
        );
    }
}