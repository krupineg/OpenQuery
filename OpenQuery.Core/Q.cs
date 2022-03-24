using System.Collections.Immutable;
using System.Data.Common;
using System.Diagnostics.Contracts;
using System.Text;
using OpenQuery.Core.Abstract;
using OpenQuery.Core.Tokens;

namespace OpenQuery.Core
{
    internal class Q<TImplementation> :
        IHaveWhereClause, IQuery, IFromQuery, IWhereQuery, IAvailableWhereQuery, IAvailableNewWhereClause, ISelectedQueryHidden, IQueryBaseHidden, IQueryHidden
        where TImplementation: ISqlDialect, new()
    {
        public string Query
        {
            get
            {
                if (string.IsNullOrEmpty(_query))
                {
                    _query = Build();
                }
                return _query;
            }
        }

        private readonly TImplementation _dialect;
        private string _table = string.Empty;
        private readonly ICollection<TokenBase> _whereTokens = new List<TokenBase>();
        private readonly List<SelectExpression> _selectExpressions = new ();
        private readonly ISet<string> _availableFields = new HashSet<string>();
        private readonly ISet<string> _alias = new HashSet<string>();
        private string _query;

        public Q()
        {
            _dialect = new TImplementation();
            _availableFields.Add(_dialect.WildCard);
        }
        
        public ISelectedQuery Select(Func<SelectClauseFactory, SelectExpression> func)
        {
            _selectExpressions.Add(func(new SelectClauseFactory(_availableFields)));
            return this;
        }

        public IAvailableWhereQuery From<T>()
        {
            return From<T>(Array.Empty<string>());
        }
        
        public IAvailableWhereQuery From<T>(params string[] domain)
        {
            var type = typeof (T);
            _table = string.Join('.', domain.Append(type.Name));

            foreach (var field in QueryFieldsCache.GetProperties(type))
            {
                _availableFields.Add(field);
            }
            
            return this;
        }

        public IQuery Where(TokenBase where)
        {
            _whereTokens.Add(where);
            return this;
        }

        public IQuery AndWhere()
        {
            _whereTokens.Add(new And(_dialect));
            return this;
        }

        public IQuery OrWhere()
        {
            _whereTokens.Add(new Or(_dialect));
            return this;
        }

        public IReadyToBuildQuery As(string alias)
        {
            Contract.Assert(_alias.Count < 1, "there could be only one alias");
            _alias.Add(alias);
            return this;
        }


        public ISqlDialect Dialect => _dialect;

        public string Build()
        {
            var sb = new StringBuilder();
            sb
                .Append(_dialect.Select).Append(_dialect.WhiteSpace)
                .Append(_selectExpressions.Single().Invoke(_dialect))
                .Append(_dialect.WhiteSpace)
                .Append(_dialect.From).Append(_dialect.WhiteSpace)
                .Append(_table);
            sb.Append(GetWhereStringBuilder());

            foreach (var alias in _alias)
            {
                sb.Append($" as {alias}");
            }
            
            _query = sb.ToString();
            return Query;
        }

        private StringBuilder GetWhereStringBuilder()
        {
            StringBuilder sb = new StringBuilder();
            if (_whereTokens.Any())
            {
                foreach (var whereToken in _whereTokens)
                {
                    sb.Append(_dialect.WhiteSpace).Append(whereToken.Build());
                }
            }
            if (sb.Length > 0)
            {
                sb.Insert(0, _dialect.Where).Insert(0, _dialect.WhiteSpace);
            }
            return sb;
        }
    }
}