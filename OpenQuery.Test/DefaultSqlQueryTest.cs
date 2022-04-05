using NUnit.Framework;
using OpenQuery.Core;
using OpenQuery.Core.Abstract;
using OpenQuery.Core.Abstract.Query;
using OpenQuery.Core.Dialects;
using Assert = NUnit.Framework.Assert;
using OpenQuery.Core.Extensions;
using OpenQuery.SQLite;

namespace OpenQuery.Test
{
    [TestFixture]
    public class DefaultSqlQueryTest
    {
        private IQueryBase _query;
        
        [SetUp]
        public void SetUp()
        {
            _query = Query.With<Default>();
        }
        
        [Test]
        public void SelectAllFieldsTest()
        {
            var starSelect =_query.Select(x => x.Everything())
                .From(x => x.Default<Model>())
                .Build();
            var starListSelect = Query.With<SqLiteDialect>()
                .Select(x => x.Fields("*"))
                .From(x => x.Default<Model>())
                .Build();
            Assert.That(starSelect, 
                Is.EqualTo(starListSelect));
            Assert.That(starListSelect, 
                Is.EqualTo("SELECT * FROM Model"));
        }

        [Test]
        public void SelectListOfFieldsTest()
        {
            var defaultSelect = _query.Select(x => x.Fields("Id", "Name"))
                .From(x => x.Default<Model>())
                .Build();
            Assert.That(defaultSelect,
                Is.EqualTo("SELECT Id, Name FROM Model"));
        }
        
        [Test]
        public void TableNameShouldBeOverridable()
        {
            var defaultSelect = _query.Select(x => x.Everything())
                .From(x => x.WithTableName("anotherName"))
                .Build();
            Assert.That(defaultSelect,
                Is.EqualTo("SELECT * FROM anotherName"));
        }
        
        [Test]
        public void SelectListOfFieldsWithWrongFieldTest()
        {
            var defaultSelect = _query.Select(x => x.Fields("Id", "Name", "wrong_field"))
                .From(x => x.Default<Model>())
                .Build();
            Assert.That(defaultSelect,
                Is.EqualTo("SELECT Id, Name FROM Model"));
        }
        
        [Test]
        public void SelectWhereEqualsWithAliasSimpleTest()
        {
            var defaultSelect = _query.Select(x => x.Fields("Id", "Name"))
                .From(x => x.Default<Model>())
                .As("a")
                .Where()
                .AreEqual<Model>(x => x.Id, 1)
                .Build();
            Assert.That(defaultSelect, 
                Is.EqualTo("SELECT Id, Name FROM Model AS a WHERE Id = 1"));
        }
        
        [Test]
        public void SelectFromSubQuery()
        {
            var defaultSelect = _query.Select(x => x.Everything())
                .From((f) => f.From(Query.With<Default>().Select(x => x.Fields("Id")).From<Model>()))
                .As("a")
                .Where()
                .AreEqual<Model>(x => x.Id, 1)
                .Build();

            Assert.That(defaultSelect, 
                Is.EqualTo("SELECT * FROM (SELECT Id FROM Model) AS a WHERE Id = 1"));
        }
        [Test]
        public void SelectWhereEqualsSimpleTest()
        {
            var defaultSelect = _query.Select(x => x.Fields("Id", "Name"))
                .From<Model>()
                .Where()
                .AreEqual<Model, int>(x => x.Id, 1)
                .Build();
            Assert.That(defaultSelect, 
                Is.EqualTo("SELECT Id, Name FROM Model WHERE Id = 1"));
        }
        
        [Test]
        public void SelectWhereEqualsSimpleTest2()
        {
            var defaultSelect = _query.Select(x => x.Fields("Id", "Name"))
                .From<Model>()
                .Where()
                .AreEqual<Model>(x => x.Id, 1)
                .Build();
            Assert.That(defaultSelect, 
                Is.EqualTo("SELECT Id, Name FROM Model WHERE Id = 1"));
        }

        [Test]
        public void SelectWhereGreaterSimpleTest()
        {
            var defaultSelect = _query.Select(x => x.Fields("Id", "Name"))
                .From<Model>()
                .Where()
                .IsGreater<Model, int>(x => x.Id, 1)
                .Build();
            Assert.That(defaultSelect, 
                Is.EqualTo("SELECT Id, Name FROM Model WHERE Id > 1"));
        }
        
        [Test]
        public void FunctionCallShoudWork()
        {
            var defaultSelect = _query.Select(x => x.Function("TO_JSON", "t"))
                .From<Model>()
                .As("t")
                .Where()
                .IsGreater<Model, int>(x => x.Id, 1)
                .Build();
            Assert.That(defaultSelect, 
                Is.EqualTo("SELECT TO_JSON(t) FROM Model AS t WHERE Id > 1"));
        }

        [Test]
        public void SelectWhereLessThanSimpleTest()
        {
            var defaultSelect = _query.Select(x => x.Fields("Id", "Name"))
                .From<Model>()
                .Where()
                .IsLess<Model, int>(x => x.Id, 1)
                .Build();
            Assert.That(defaultSelect, 
                Is.EqualTo("SELECT Id, Name FROM Model WHERE Id < 1"));
        }

        [Test]
        public void SelectWhereLikeSimpleTest()
        {
            var defaultSelect = _query.Select(x => x.Fields("Id", "Name"))
                .From<Model>()
                .Where()
                .IsLike<Model>(x => x.Name, "1")
                .Build();
            Assert.That(defaultSelect,
                Is.EqualTo("SELECT Id, Name FROM Model WHERE Name LIKE '1'"));
        }

        [Test]
        public void SelectWhereInSimpleTest()
        {
            var defaultSelect = _query.Select(x => x.Fields("Id", "Name"))
                .From<Model>()
                .Where()
                .IsIn<Model, int>(x => x.Id, 1, 2, 3)
                .Build();
            Assert.That(defaultSelect,
                Is.EqualTo("SELECT Id, Name FROM Model WHERE Id IN (1, 2, 3)"));
        }

        [Test]
        public void SelectWhereNotInSimpleTest()
        {
            var defaultSelect = _query.Select(x => x.Fields("Id", "Name"))
                .From<Model>()
                .Where()
                .IsNotIn<Model, int>(x => x.Id, 1, 2, 3)
                .Build();
            Assert.That(defaultSelect,
                Is.EqualTo("SELECT Id, Name FROM Model WHERE Id NOT IN (1, 2, 3)"));
        }

        [Test]
        public void SelectWhereEqualOrEqualSimpleTest()
        {
            var defaultSelect = _query.Select(x => x.Fields("Id", "Name"))
                .From<Model>()
                .Where()
                .AreEqual<Model, int>(x => x.Id, 1)
                .Or()
                .AreEqual<Model, int>(x => x.Id, 2)
                .Build();
            Assert.That(defaultSelect, 
                Is.EqualTo("SELECT Id, Name FROM Model WHERE Id = 1 OR Id = 2"));
        }

        [Test]
        public void SelectWhereEqualAndLessSimpleTest()
        {
            var defaultSelect = _query.Select(x => x.Fields("Id", "Name"))
                .From<Model>()
                .Where().AreEqual<Model>(x => x.Name, "somename")
                .And()
                .IsLess<Model, int>(x => x.Id, 2)
                .Build();
            Assert.That(defaultSelect, 
                Is.EqualTo("SELECT Id, Name FROM Model WHERE Name = 'somename' AND Id < 2"));
        }
        
        [Test]
        public void SelectCount()
        {
            var defaultSelect = _query.Select(x => x.Count())
                .From<Model>()
                .Where()
                .IsNotIn<Model, int>(x => x.Id, 1, 2, 3)
                .Build();
            Assert.That(defaultSelect,
                Is.EqualTo("SELECT COUNT(*) FROM Model WHERE Id NOT IN (1, 2, 3)"));
        }
        
        [Test]
        public void LimitOffset()
        {
            var defaultSelect = _query.Select(x => x.Everything())
                .From<Model>()
                .Where()
                .IsNotIn<Model, int>(x => x.Id, 1, 2, 3)
                .Limit(1)
                .Offset(2)
                .Build();
            Assert.That(defaultSelect,
                Is.EqualTo("SELECT * FROM Model WHERE Id NOT IN (1, 2, 3) LIMIT 1 OFFSET 2"));
        }

         
        [Test]
        public void LimitOffsetSimple()
        {
            var defaultSelect = _query.Select(x => x.Everything())
                .From<Model>()
                .Limit(1)
                .Offset(2)
                .Build();
            Assert.That(defaultSelect,
                Is.EqualTo("SELECT * FROM Model LIMIT 1 OFFSET 2"));
        }
        
        [Test]
        public void SelectWithDomain()
        {
            var defaultSelect = _query.Select(x => x.Everything())
                .From(a => a.WithDomain<Model>("domain"))
                .Where()
                .IsNotIn<Model, int>(x => x.Id, 1, 2, 3)
                .Build();
            Assert.That(defaultSelect,
                Is.EqualTo("SELECT * FROM domain.Model WHERE Id NOT IN (1, 2, 3)"));
        }
    }
}
