namespace OpenQuery.Core.Abstract
{
    public interface IQueryHidden : IQuery
    {
        ISelectedQuery Select(IList<string> fields);
        ISelectedQuery Select(params string[] fields);
    }
}