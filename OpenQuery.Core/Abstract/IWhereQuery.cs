namespace OpenQuery.Core.Abstract
{
    public interface IWhereQuery : IQueryBase
    {
        IQuery Where(TokenBase where);
    }
}