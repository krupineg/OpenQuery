using NUnit.Framework;
using OpenQuery.PCL;
using Assert = NUnit.Framework.Assert;
using System.Collections.Generic;
using OpenQuery.PCL.Extensions;
using OpenQuery.PCL.SQLite;

namespace OpenQuery.Test
{
    [TestFixture]
    public class SqliteQueryTest
    {
        [Test]
        public void SelectAllFieldsTest()
        {
            var starSelect = Query.With<SqLiteImplementation>()
                .Select("*")
                .From<Model>()
                .Build();
            var starListSelect = Query.With<SqLiteImplementation>()
                .Select(new List<string> { "*" })
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
            var starSelect = Query.With<SqLiteImplementation>()
                .Select()
                .From<Model>();
            Assert.That(starSelect.Query,
                Is.EqualTo(starSelect.Build()));
        }

        [Test]
        public void SelectListOfFieldsTest()
        {
            var defaultSelect = Query.With<SqLiteImplementation>()
                .Select("Id", "Name")
                .From<Model>()
                .Build();
            var starSelect = Query.With<SqLiteImplementation>()
                .Select(new List<string> { "Id", "Name" })
                .From<Model>()
                .Build();
            Assert.That(defaultSelect, 
                Is.EqualTo(starSelect));
            Assert.That(defaultSelect,
                Is.EqualTo("SELECT Id, Name FROM Model"));
        }

        [Test]
        public void SelectListOfFieldsWithWrongFieldTest()
        {
            var defaultSelect = Query.With<SqLiteImplementation>()
                .Select("Id", "Name", "wrong_field")
                .From<Model>()
                .Build();
            var starSelect = Query.With<SqLiteImplementation>()
                .Select(new List<string> { "Id", "Name", "wrong_field" })
                .From<Model>()
                .Build();
            Assert.That(defaultSelect, 
                Is.EqualTo(starSelect));
            Assert.That(defaultSelect,
                Is.EqualTo("SELECT Id, Name FROM Model"));
        }

        [Test]
        public void SelectWhereEqualsSimpleTest()
        {
            var defaultSelect = Query.With<SqLiteImplementation>()
                .Select("Id", "Name")
                .From<Model>()
                .Where()
                .AreEqual<Model, int>(x => x.Id, 1)
                .Build();
            Assert.That(defaultSelect, 
                Is.EqualTo("SELECT Id, Name FROM Model WHERE Id = 1"));
        }

        [Test]
        public void SelectWhereGreaterSimpleTest()
        {
            var defaultSelect = Query.With<SqLiteImplementation>()
                .Select("Id", "Name")
                .From<Model>()
                .Where()
                .IsGreater<Model, int>(x => x.Id, 1)
                .Build();
            Assert.That(defaultSelect, 
                Is.EqualTo("SELECT Id, Name FROM Model WHERE Id > 1"));
        }

        [Test]
        public void SelectWhereLesserSimpleTest()
        {
            var defaultSelect = Query.With<SqLiteImplementation>()
                .Select("Id", "Name")
                .From<Model>()
                .Where()
                .IsLesser<Model, int>(x => x.Id, 1)
                .Build();
            Assert.That(defaultSelect, 
                Is.EqualTo("SELECT Id, Name FROM Model WHERE Id < 1"));
        }

        [Test]
        public void SelectWhereLikeSimpleTest()
        {
            var defaultSelect = Query.With<SqLiteImplementation>()
                .Select("Id", "Name")
                .From<Model>()
                .Where()
                .IsLike<Model, int>(x => x.Id, 1)
                .Build();
            Assert.That(defaultSelect,
                Is.EqualTo("SELECT Id, Name FROM Model WHERE Id LIKE 1"));
        }

        [Test]
        public void SelectWhereInSimpleTest()
        {
            var defaultSelect = Query.With<SqLiteImplementation>()
                .Select("Id", "Name")
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
            var defaultSelect = Query.With<SqLiteImplementation>()
                .Select("Id", "Name")
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
            var defaultSelect = Query.With<SqLiteImplementation>()
                .Select("Id", "Name")
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
            var defaultSelect = Query.With<SqLiteImplementation>()
                .Select("Id", "Name")
                .From<Model>()
                .Where().AreEqual<Model, string>(x => x.Name, "'somename'")
                .And()
                .IsLesser<Model, int>(x => x.Id, 2)
                .Build();
            Assert.That(defaultSelect, 
                Is.EqualTo("SELECT Id, Name FROM Model WHERE Name = 'somename' AND Id < 2"));
        }
    }
}
