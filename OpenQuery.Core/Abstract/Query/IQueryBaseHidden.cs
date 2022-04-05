using OpenQuery.Core.Abstract.Dialect;

namespace OpenQuery.Core.Abstract.Query
{
    public interface IQueryBaseHidden: IQueryBase
    {
        ISqlDialect Dialect { get; }
    }
}