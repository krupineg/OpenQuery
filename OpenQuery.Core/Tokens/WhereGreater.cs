using System.Text;
using OpenQuery.Core.Abstract;

namespace OpenQuery.Core.Tokens
{
    internal class WhereGreater<TSource, T> : WhereTokenBase<TSource, T>
    {
        internal WhereGreater(ISqlDialect dialect, string name, T val)
            : base(dialect, name, val)
        {
        }

        public override StringBuilder GetSign()
        {
            return new StringBuilder()
                .Append(Dialect.WhiteSpace)
                .Append(Dialect.Greater)
                .Append(Dialect.WhiteSpace);
        }
    }
}