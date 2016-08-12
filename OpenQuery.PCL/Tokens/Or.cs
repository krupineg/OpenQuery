using System.Text;
using OpenQuery.PCL.Abstract;

namespace OpenQuery.PCL.Tokens
{
    internal class Or : TokenBase
    {
        public override string Build()
        {
            return Implementation.Or;
        }

        internal override bool IsAvailable(string[] availableFilds, StringBuilder sb)
        {
            return sb.Length > 0;
        }

        public Or(ISqlImplementation implementation) : base(implementation)
        {
        }
    }
}