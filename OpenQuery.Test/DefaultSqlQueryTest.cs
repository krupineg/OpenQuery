﻿using NUnit.Framework;
using OpenQuery.Core;
using Assert = NUnit.Framework.Assert;
using OpenQuery.Core.Extensions;
using OpenQuery.SQLite;

namespace OpenQuery.Test
{
    [TestFixture]
    public class DefaultSqlQueryTest
    {
        [Test]
        public void SelectAllFieldsTest()
        {
            var starSelect = Query.With<SqLiteDialect>()
                .Select(x => x.Everything())
                .From<Model>()
                .Build();
            var starListSelect = Query.With<SqLiteDialect>()
                .Select(x => x.Fields("*"))
                .From<Model>()
                .Build();
            Assert.That(starSelect, 
                Is.EqualTo(starListSelect));
            Assert.That(starListSelect, 
                Is.EqualTo("SELECT * FROM Model"));
        }



        [Test]
        public void QueryEqualsBuildResult()
        {
            var starSelect = Query.With<SqLiteDialect>()
                .Select(x => x.Everything())
                .From<Model>();
            Assert.That(starSelect.Query,
                Is.EqualTo(starSelect.Build()));
        }

        [Test]
        public void SelectListOfFieldsTest()
        {
            var defaultSelect = Query.With<SqLiteDialect>()
                .Select(x => x.Fields("Id", "Name"))
                .From<Model>()
                .Build();
            Assert.That(defaultSelect,
                Is.EqualTo("SELECT Id, Name FROM Model"));
        }

        [Test]
        public void SelectListOfFieldsWithWrongFieldTest()
        {
            var defaultSelect = Query.With<SqLiteDialect>()
                .Select(x => x.Fields("Id", "Name", "wrong_field"))
                .From<Model>()
                .Build();
            Assert.That(defaultSelect,
                Is.EqualTo("SELECT Id, Name FROM Model"));
        }
        
        [Test]
        public void SelectWhereEqualsWithAliasSimpleTest()
        {
            var defaultSelect = Query.With<SqLiteDialect>()
                .Select(x => x.Fields("Id", "Name"))
                .From<Model>()
                .Where()
                .AreEqual<Model>(x => x.Id, 1)
                .As("a")
                .Build();
            Assert.That(defaultSelect, 
                Is.EqualTo("SELECT Id, Name FROM Model WHERE Id = 1 as a"));
        }
        [Test]
        public void SelectWhereEqualsSimpleTest()
        {
            var defaultSelect = Query.With<SqLiteDialect>()
                .Select(x => x.Fields("Id", "Name"))
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
            var defaultSelect = Query.With<SqLiteDialect>()
                .Select(x => x.Fields("Id", "Name"))
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
            var defaultSelect = Query.With<SqLiteDialect>()
                .Select(x => x.Fields("Id", "Name"))
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
            var defaultSelect = Query.With<SqLiteDialect>()
                .Select(x => x.Function("TO_JSON", "t"))
                .From<Model>()
                .Where()
                .IsGreater<Model, int>(x => x.Id, 1)
                .As("t")
                .Build();
            Assert.That(defaultSelect, 
                Is.EqualTo("SELECT TO_JSON(t) FROM Model WHERE Id > 1 as t"));
        }

        [Test]
        public void SelectWhereLessThanSimpleTest()
        {
            var defaultSelect = Query.With<SqLiteDialect>()
                .Select(x => x.Fields("Id", "Name"))
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
            var defaultSelect = Query.With<SqLiteDialect>()
                .Select(x => x.Fields("Id", "Name"))
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
            var defaultSelect = Query.With<SqLiteDialect>()
                .Select(x => x.Fields("Id", "Name"))
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
            var defaultSelect = Query.With<SqLiteDialect>()
                .Select(x => x.Fields("Id", "Name"))
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
            var defaultSelect = Query.With<SqLiteDialect>()
                .Select(x => x.Fields("Id", "Name"))
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
            var defaultSelect = Query.With<SqLiteDialect>()
                .Select(x => x.Fields("Id", "Name"))
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
            var defaultSelect = Query.With<SqLiteDialect>()
                .Select(x => x.Count())
                .From<Model>()
                .Where()
                .IsNotIn<Model, int>(x => x.Id, 1, 2, 3)
                .Build();
            Assert.That(defaultSelect,
                Is.EqualTo("SELECT COUNT(*) FROM Model WHERE Id NOT IN (1, 2, 3)"));
        }
        
        [Test]
        public void SelectWithDomain()
        {
            var defaultSelect = Query.With<SqLiteDialect>()
                .Select(x => x.Everything())
                .From<Model>("domain")
                .Where()
                .IsNotIn<Model, int>(x => x.Id, 1, 2, 3)
                .Build();
            Assert.That(defaultSelect,
                Is.EqualTo("SELECT * FROM domain.Model WHERE Id NOT IN (1, 2, 3)"));
        }

    }
}
