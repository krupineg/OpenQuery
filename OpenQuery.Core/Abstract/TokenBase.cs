using System.Text;

namespace OpenQuery.Core.Abstract
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