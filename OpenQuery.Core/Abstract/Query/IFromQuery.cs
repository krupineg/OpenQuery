namespace OpenQuery.Core.Abstract.Query
{
    public interface IFromQuery: IReadyToBuildQuery
    {
        IFromQuery As(string alias);
    }
}