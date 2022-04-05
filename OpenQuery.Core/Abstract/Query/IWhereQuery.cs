using OpenQuery.Core.Abstract.Tokens;

namespace OpenQuery.Core.Abstract.Query
{
    public interface IWhereQuery : IQueryBase
    {
        IQuery Where(TokenBase where);
    }
}