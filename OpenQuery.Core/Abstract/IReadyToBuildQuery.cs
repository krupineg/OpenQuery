namespace OpenQuery.Core.Abstract
{
    public interface IReadyToBuildQuery:IQueryBase
    {
        string Query { get; }
        string Build();
        IReadyToBuildQuery Limit(long limit);
        IReadyToBuildQuery Offset(long limit);
    }
}