namespace OpenQuery.Core.Abstract
{
    internal interface ISelectedQueryHidden : ISelectedQuery
    {   
        IAvailableWhereQuery From(Func<IReadyToBuildQuery> subQuery);
        IAvailableWhereQuery From<T>();
        IAvailableWhereQuery From<T>(params string[] domain);
    }
}