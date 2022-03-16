using System.Text;
using OpenQuery.Core.Abstract;

namespace OpenQuery.Core.Tokens
{
    internal class WhereGreater<T> : WhereTokenBase<T>
    {
        internal WhereGreater(ISqlImplementation implementation, string name, T val)
            : base(implementation, name, val)
        {
        }

        public override StringBuilder GetSign()
        {
            return new StringBuilder()
                .Append(Implementation.WhiteSpace)
                .Append(Implementation.Greater)
                .Append(Implementation.WhiteSpace);
        }
    }
}