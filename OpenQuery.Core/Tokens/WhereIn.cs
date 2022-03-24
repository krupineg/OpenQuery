using System.Text;
using OpenQuery.Core.Abstract;

namespace OpenQuery.Core.Tokens
{
    internal class WhereIn<TSource, T> : WhereTokenBase<TSource, T>
    {
        internal WhereIn(ISqlDialect dialect, string name, T val)
            : base(dialect, name, val)
        {
        }

        public override StringBuilder GetSign()
        {
            return new StringBuilder()
                .Append(Dialect.WhiteSpace)
                .Append(Dialect.In)
                .Append(Dialect.WhiteSpace);
        }
    }
}