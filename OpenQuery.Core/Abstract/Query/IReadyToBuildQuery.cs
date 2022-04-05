using OpenQuery.Core.Abstract.Clauses.From;

namespace OpenQuery.Core.Abstract.Query
{
    public interface IReadyToBuildQuery:IQueryBase
    {
        FromExpression Source { get; }
        string Build();
        IReadyToBuildQuery Limit(long limit);
        IReadyToBuildQuery Offset(long limit);
    }
}