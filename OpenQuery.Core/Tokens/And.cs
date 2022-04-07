using System.Text;
using OpenQuery.Core.Abstract;
using OpenQuery.Core.Abstract.Dialect;
using OpenQuery.Core.Abstract.Tokens;

namespace OpenQuery.Core.Tokens
{
    internal class And: IToken
    {
        public string Build(ISqlDialect dialect)
        {
            return dialect.And;
        }
    }
}