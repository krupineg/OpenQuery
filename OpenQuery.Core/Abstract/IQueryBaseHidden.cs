namespace OpenQuery.Core.Abstract
{
    public interface IQueryBaseHidden:IQueryBase
    {
        ISqlDialect Dialect { get; }
    }
}