namespace OpenQuery.PCL.Abstract
{
    public interface IReadyToBuildQuery:IQueryBase
    {
        string Query { get; }
        string Build();
    }
}