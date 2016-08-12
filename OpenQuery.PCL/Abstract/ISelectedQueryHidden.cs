namespace OpenQuery.PCL.Abstract
{
    internal interface ISelectedQueryHidden : ISelectedQuery
    {
        IAvailableWhereQuery From<T>();
    }
}