using System.Diagnostics.Contracts;
using System.Text;
using OpenQuery.Core.Abstract;
using OpenQuery.Core.Tokens;

namespace OpenQuery.Core
{
    internal class Q<TDialect> :
        IHaveWhereClause, IQuery, IFromQuery, IWhereQuery, IAvailableWhereQuery, IAvailableNewWhereClause, ISelectedQueryHidden, IQueryBaseHidden, IQueryHidden
        where TDialect: ISqlDialect, new()
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

        private readonly TDialect _dialect;
        private string _source = string.Empty;
        private readonly ICollection<TokenBase> _whereTokens = new List<TokenBase>();
        private readonly List<SelectExpression> _selectExpressions = new ();
        private readonly ISet<string> _availableFields = new HashSet<string>();
        private readonly ISet<string> _alias = new HashSet<string>();
        private readonly ISet<long> _limits = new HashSet<long>();
        private readonly ISet<long> _offsets = new HashSet<long>();
        private string _query;

        public Q()
        {
            _dialect = new TDialect();
            _availableFields.Add(_dialect.WildCard);
        }
        
        public ISelectedQuery Select(Func<SelectClauseFactory, SelectExpression> func)
        {
            _selectExpressions.Add(func(new SelectClauseFactory(_availableFields)));
            return this;
        }

        public IAvailableWhereQuery From(Func<IReadyToBuildQuery> subQuery)
        {
            _source = $"{_dialect.OpenSubquery}{subQuery().Build()}{_dialect.CloseSubquery}";
            return this;
        }

        public IAvailableWhereQuery From<T>()
        {
            return From<T>(Array.Empty<string>());
        }
        
        public IAvailableWhereQuery From<T>(params string[] domain)
        {
            var type = typeof (T);
            _source = string.Join(_dialect.DomainSeparator, domain.Append(type.Name));

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

        public IFromQuery As(string alias)
        {
            Contract.Assert(_alias.Count < 1, "there could be only one alias");
            _alias.Add(alias);
            return this;
        }


        public ISqlDialect Dialect => _dialect;

        public string Build()
        {
            var sb = new StringBuilder();
            sb.Append(_dialect.Select).Append(_dialect.WhiteSpace)
                .Append(_selectExpressions.Single().Invoke(_dialect))
                .Append(_dialect.WhiteSpace)
                .Append(_dialect.From).Append(_dialect.WhiteSpace)
                .Append(_source);
            
            Optional(_dialect.Alias, _alias);
            
            sb.Append(GetWhereStringBuilder());
            
            Optional(_dialect.Limit, _limits);
            Optional(_dialect.Offset, _offsets);

            void Optional<T>(string prefix, ISet<T> values)
            {
                foreach (var value in values)
                {
                    sb.Append($" {prefix} {value}");
                }
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

        public IReadyToBuildQuery Limit(long limit)
        {
            Contract.Assert(_limits.Count < 1, "there could be only one limit per query");
            _limits.Add(limit);
            return this;
        }

        public IReadyToBuildQuery Offset(long offset)
        {
            Contract.Assert(_offsets.Count < 1, "there could be only one offset per query");
            _offsets.Add(offset);
            return this;
        }
    }
}