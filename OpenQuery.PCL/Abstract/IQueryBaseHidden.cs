namespace OpenQuery.PCL.Abstract
{
    public interface IQueryBaseHidden:IQueryBase
    {
        ISqlImplementation Implementation { get; }
    }
}