
using OpenQuery.Core.Abstract.Dialect;

namespace OpenQuery.Core.Abstract.Tokens
{
    public interface IToken
    {
        public string Build(ISqlDialect dialect);
    }
}