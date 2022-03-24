namespace OpenQuery.Core.Abstract
{
    internal interface ISelectedQueryHidden : ISelectedQuery
    {
        IAvailableWhereQuery From<T>();
        IAvailableWhereQuery From<T>(params string[] domain);
    }
}