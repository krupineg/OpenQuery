using System;
using NUnit.Framework;
using OpenQuery.PCL;
using OpenQuery.PCL.Extensions;
using OpenQuery.PCL.SQLite;

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
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Expression 'x => x.Field' refers to a field, not a property.")]
        public void ExtractWithFieldShouldThrowException()
        {
            var defaultSelect = Query.With<SqLiteImplementation>()
                .Select("Id", "Name")
                .From<Model>()
                .Where()
                .IsNotIn<Model, int>(x => x.Field, 1, 2, 3)
                .Build();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Expression 'x => new DifferentModel().Method()' refers to a method, not a property.")]
        public void ExtractWithMethodShouldThrowException()
        {
            var defaultSelect = Query.With<SqLiteImplementation>()
                .Select("Id", "Name")
                .From<Model>()
                .Where()
                .IsNotIn<Model, int>(x => new DifferentModel().Method(), 1, 2, 3)
                .Build();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Expression 'x => new DifferentModel().AnotherProperty' refers to a property that is not from type OpenQuery.Test.Model.")]
        public void ExtractWithPropertyOfWrongClassShouldThrowException()
        {
            var defaultSelect = Query.With<SqLiteImplementation>()
                .Select("Id", "Name")
                .From<Model>()
                .Where()
                .IsNotIn<Model, int>(x => new DifferentModel().AnotherProperty, 1, 2, 3)
                .Build();
        }
    }
}