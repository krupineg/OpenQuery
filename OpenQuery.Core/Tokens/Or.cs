using System.Text;
using OpenQuery.Core.Abstract;
using OpenQuery.Core.Abstract.Dialect;
using OpenQuery.Core.Abstract.Tokens;

namespace OpenQuery.Core.Tokens
{
    internal class Or : TokenBase
    {
        public override string Build()
        {
            return Dialect.Or;
        }

        public Or(ISqlDialect dialect) : base(dialect)
        {
        }
    }
}