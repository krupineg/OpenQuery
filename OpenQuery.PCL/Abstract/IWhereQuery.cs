namespace OpenQuery.PCL.Abstract
{
    public interface IWhereQuery : IQueryBase
    {
        IQuery Where(TokenBase where);
    }
}