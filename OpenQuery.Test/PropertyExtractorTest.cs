using NUnit.Framework;
using OpenQuery.Core;
using OpenQuery.Core.Extensions;
using OpenQuery.SQLite;

namespace OpenQuery.Test
{
    [TestFixture]
    public class PropertyExtractorTest
    {
        [Test]
        public void ExtractTest()
        {
            Assert.That("Id",
                Is.EqualTo(PropertyExtractor.GetPropertyInfo<Model, int>(x => x.Id).Name));
        }

        [Test]
        public void ExtractWithFieldShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Query.With<SqLiteDialect>()
                    .Select(x => x.Fields("Id", "Name"))
                    .From<Model>()
                    .Where()
                    .IsNotIn<Model, int>(x => x.Field, 1, 2, 3)
                    .Build();
            }, "Expression 'x => x.Field' refers to a field, not a property.");
        }

        [Test]
        public void ExtractWithMethodShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Query.With<SqLiteDialect>()
                    .Select(x => x.Fields("Id", "Name"))
                    .From<Model>()
                    .Where()
                    .IsNotIn<Model, int>(x => new DifferentModel().Method(), 1, 2, 3)
                    .Build();
            }, "Expression 'x => new DifferentModel().Method()' refers to a method, not a property.");
        }

        [Test]
        public void ExtractWithPropertyOfWrongClassShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
                {
                    Query.With<SqLiteDialect>()
                        .Select(x => x.Fields("Id", "Name"))
                        .From<Model>()
                        .Where()
                        .IsNotIn<Model, int>(x => new DifferentModel().AnotherProperty, 1, 2, 3)
                        .Build();
                },
                "Expression 'x => new DifferentModel().AnotherProperty' refers to a property that is not from type OpenQuery.Test.Model.");
        }
    }
}