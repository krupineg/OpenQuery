using System.Text;
using OpenQuery.Core.Abstract;

namespace OpenQuery.Core.Tokens
{
    internal class And: TokenBase
    {

        public override string Build()
        {
            return Implementation.And;
        }

        internal override bool IsAvailable(string[] availableFilds, StringBuilder sb)
        {
            return sb.Length > 0;
        }

        public And(ISqlImplementation implementation) : base(implementation)
        {

        }
    }
}