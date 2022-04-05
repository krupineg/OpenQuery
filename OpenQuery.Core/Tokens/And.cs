using System.Text;
using OpenQuery.Core.Abstract;
using OpenQuery.Core.Abstract.Dialect;
using OpenQuery.Core.Abstract.Tokens;

namespace OpenQuery.Core.Tokens
{
    internal class And: TokenBase
    {

        public override string Build()
        {
            return Dialect.And;
        }

        public And(ISqlDialect dialect) : base(dialect)
        {
        }
    }
}