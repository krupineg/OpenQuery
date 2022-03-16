namespace OpenQuery.Core.Abstract
{
    public interface IReadyToBuildQuery:IQueryBase
    {
        string Query { get; }
        string Build();
    }
}