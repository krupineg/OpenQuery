
using OpenQuery.Core.Abstract.Dialect;

namespace OpenQuery.Core.Abstract.Tokens
{
    public abstract class TokenBase
    {
        protected readonly ISqlDialect Dialect;

        protected TokenBase(ISqlDialect dialect)
        {
            Dialect = dialect;
        }
        public abstract string Build();
    }
}