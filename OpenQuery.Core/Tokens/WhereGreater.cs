using System.Text;
using OpenQuery.Core.Abstract.Clauses.Where;
using OpenQuery.Core.Abstract.Dialect;
using OpenQuery.Core.Abstract.Tokens;

namespace OpenQuery.Core.Tokens
{
    internal class WhereGreater<T> : WhereToken<T>
    {
        internal WhereGreater(WhereExpression whereExpression, T val)
            : base(whereExpression, val)
        {
        }

        protected override StringBuilder GetSign(ISqlDialect dialect)
        {
            return new StringBuilder()
                .Append(dialect.WhiteSpace)
                .Append(dialect.Greater)
                .Append(dialect.WhiteSpace);
        }
    }
}