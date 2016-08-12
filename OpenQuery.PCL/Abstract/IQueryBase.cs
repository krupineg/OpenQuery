namespace OpenQuery.PCL.Abstract
{
    public interface IQueryBase
    {
        string Build();
    }

    public interface IQueryBaseHidden:IQueryBase
    {
        ISqlImplementation Implementation { get; }
    }
}