using System.Text;
using OpenQuery.Core.Abstract;
using OpenQuery.Core.Abstract.Dialect;
using OpenQuery.Core.Abstract.Tokens;

namespace OpenQuery.Core.Tokens
{
    internal class WhereNotIn<TSource, T> : WhereTokenBase<TSource, T>
    {
        internal WhereNotIn(ISqlDialect dialect, string name, T val)
            : base(dialect, name, val)
        {
        }

        public override StringBuilder GetSign()
        {
            return new StringBuilder()
                .Append(Dialect.WhiteSpace)
                .Append(Dialect.NotIn)
                .Append(Dialect.WhiteSpace);
        }
    }
}