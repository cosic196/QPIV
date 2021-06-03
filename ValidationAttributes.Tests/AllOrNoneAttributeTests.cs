using FluentAssertions;
using QPIV.ValidationAttributes.Tests.TestModels;
using Xunit;

namespace QPIV.ValidationAttributes.Tests
{
    public class AllOrNoneAttributeTests
    {
        [Fact]
        public void Should_Return_True_When_All_Required_Parameters_Are_Set()
        {
            var model = new SimpleModel()
            {
                SimpleModelProperty1 = 1,
                SimpleModelProperty2 = 1,
                simpleModelField = "foo"
            };
            var attribute = new AllOrNoneAttribute(SimpleModel.Property1Name, SimpleModel.Property2Name, SimpleModel.FieldName);

            var result = attribute.IsValid(model);

            result.Should().BeTrue();
        }

        [Fact]
        public void Should_Return_True_When_None_Of_Required_Parameters_Are_Set()
        {
            var model = new SimpleModel()
            {
                SimpleModelProperty1 = null,
                SimpleModelProperty2 = null,
                simpleModelField = null
            };
            var attribute = new AllOrNoneAttribute(SimpleModel.Property1Name, SimpleModel.Property2Name, SimpleModel.FieldName);

            var result = attribute.IsValid(model);

            result.Should().BeTrue();
        }

        [Fact]
        public void Should_Return_False_When_Some_Of_Required_Parameters_Are_Set()
        {
            var model = new SimpleModel()
            {
                SimpleModelProperty1 = 1,
                SimpleModelProperty2 = null,
                simpleModelField = "foo"
            };
            var attribute = new AllOrNoneAttribute(SimpleModel.Property1Name, SimpleModel.Property2Name, SimpleModel.FieldName);

            var result = attribute.IsValid(model);

            result.Should().BeFalse();
        }
    }
}
