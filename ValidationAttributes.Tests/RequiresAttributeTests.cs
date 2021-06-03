using FluentAssertions;
using QPIV.ValidationAttributes.Tests.TestModels;
using System;
using Xunit;

namespace QPIV.ValidationAttributes.Tests
{
    public class RequiresAttributeTests
    {
        [Fact]
        public void Should_Return_True_When_Target_Parameter_Is_Not_Set()
        {
            var model = new SimpleModel()
            {
                SimpleModelProperty1 = null,
                SimpleModelProperty2 = null,
                simpleModelField = null
            };
            var attribute = new RequiresAttribute(SimpleModel.Property2Name, SimpleModel.Property1Name, SimpleModel.FieldName);

            var result = attribute.IsValid(model);

            result.Should().BeTrue();
        }

        [Fact]
        public void Should_Return_False_When_Target_Parameter_Is_Set_And_Required_Parameters_Are_Not_Set()
        {
            var model = new SimpleModel()
            {
                SimpleModelProperty1 = null,
                SimpleModelProperty2 = 1,
                simpleModelField = null
            };
            var attribute = new RequiresAttribute(SimpleModel.Property2Name, SimpleModel.Property1Name, SimpleModel.FieldName);

            var result = attribute.IsValid(model);

            result.Should().BeFalse();
        }

        [Fact]
        public void Should_Return_True_When_Target_Parameter_And_Required_Parameters_Are_Set()
        {
            var model = new SimpleModel()
            {
                SimpleModelProperty1 = 1,
                SimpleModelProperty2 = null,
                simpleModelField = "foo"
            };
            var attribute = new RequiresAttribute(SimpleModel.FieldName, SimpleModel.Property1Name);

            var result = attribute.IsValid(model);

            result.Should().BeTrue();
        }

        [Fact]
        public void Should_Throw_ArgumentException_When_Given_TargetParameterName_Within_RequiredParameterNames()
        {
            Action a = () => new RequiresAttribute(SimpleModel.Property1Name, SimpleModel.Property2Name, SimpleModel.Property1Name);

            a.Should().Throw<ArgumentException>();
        }
    }
}
