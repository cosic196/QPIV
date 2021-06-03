using FluentAssertions;
using QPIV.ValidationAttributes.Tests.TestModels;
using Xunit;

namespace QPIV.ValidationAttributes.Tests
{
    public class ZeroOrOneAttributeTests
    {
        [Fact]
        public void Should_Return_False_When_More_Than_One_Parameter_Is_Set()
        {
            var model = new SimpleModel()
            {
                SimpleModelProperty1 = 1,
                SimpleModelProperty2 = 1,
                simpleModelField = null
            };
            var attribute = new ZeroOrOneAttribute(SimpleModel.Property1Name, SimpleModel.Property2Name, SimpleModel.FieldName);

            var result = attribute.IsValid(model);

            result.Should().BeFalse();
        }

        [Fact]
        public void Should_Return_True_When_None_Of_Parameters_Are_Set()
        {
            var model = new SimpleModel()
            {
                SimpleModelProperty1 = null,
                SimpleModelProperty2 = null,
                simpleModelField = null
            };
            var attribute = new ZeroOrOneAttribute(SimpleModel.Property1Name, SimpleModel.Property2Name, SimpleModel.FieldName);

            var result = attribute.IsValid(model);

            result.Should().BeTrue();
        }

        [Fact]
        public void Should_Return_True_When_One_Of_Parameters_Is_Set()
        {
            var model = new SimpleModel()
            {
                SimpleModelProperty1 = 1,
                SimpleModelProperty2 = null,
                simpleModelField = null
            };
            var attribute = new ZeroOrOneAttribute(SimpleModel.Property1Name, SimpleModel.Property2Name, SimpleModel.FieldName);

            var result = attribute.IsValid(model);

            result.Should().BeTrue();
        }
    }
}
