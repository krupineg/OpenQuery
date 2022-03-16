namespace OpenQuery.Core.Abstract
{
    internal interface ISelectedQueryHidden : ISelectedQuery
    {
        IAvailableWhereQuery From<T>();
    }
}