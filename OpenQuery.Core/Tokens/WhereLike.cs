using System.Text;
using OpenQuery.Core.Abstract;

namespace OpenQuery.Core.Tokens
{
    internal class WhereLike<T> : WhereTokenBase<T>
    {
        internal WhereLike(ISqlImplementation implementation, string name, T val)
            : base(implementation, name, val)
        {
        }

        public override StringBuilder GetSign()
        {
            return new StringBuilder()
                .Append(Implementation.WhiteSpace)
                .Append(Implementation.Like)
                .Append(Implementation.WhiteSpace);
        }
    }
}