namespace OpenQuery.Core.Abstract
{
    public interface IFromQuery: IReadyToBuildQuery
    {
        IFromQuery As(string alias);
    }
}