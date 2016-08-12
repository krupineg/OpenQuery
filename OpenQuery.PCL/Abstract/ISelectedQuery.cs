namespace OpenQuery.PCL.Abstract
{
    public interface ISelectedQuery : IQueryBase
    {
    }

    internal interface ISelectedQueryHidden : ISelectedQuery
    {
        IAvailableWhereQuery From<T>();
    }
}