namespace OpenQuery.Core.Abstract
{
    public interface IQueryBaseHidden:IQueryBase
    {
        ISqlImplementation Implementation { get; }
    }
}