using System.Text;
using OpenQuery.Core.Abstract;

namespace OpenQuery.Core.Tokens
{
    internal class WhereEqual<T> : WhereTokenBase<T>
    {
        internal WhereEqual(ISqlImplementation implementation, string name, T val) : base(implementation, name, val)
        {
        }

        public override StringBuilder GetSign()
        {
            return new StringBuilder()
                .Append(Implementation.WhiteSpace)
                .Append(Implementation.IsEqual)
                .Append(Implementation.WhiteSpace);
        }
    }
}