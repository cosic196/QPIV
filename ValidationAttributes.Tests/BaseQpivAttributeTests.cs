using QPIV.ValidationAttributes.Tests.Stubs;
using System;
using Xunit;
using FluentAssertions;
using QPIV.ValidationAttributes.Tests.TestModels;
using System.Diagnostics;

namespace QPIV.ValidationAttributes.Tests
{
    public class BaseQpivAttributeTests
    {
        [Fact]
        public void Should_Throw_ArgumentNullException_When_Constructing_With_Null_ParameterName()
        {
            Action a = () => new BaseQpivParameterValidationSuccessAttribute(null);

            a.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Should_Throw_ArgumentNullException_When_Constructing_With_Empty_ParameterName()
        {
            Action a = () => new BaseQpivParameterValidationSuccessAttribute("");

            a.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Should_Throw_ArgumentNullException_When_Constructing_With_No_ParameterNames()
        {
            Action a = () => new BaseQpivParameterValidationSuccessAttribute();

            a.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Should_Throw_MemberAccessException_When_Validating_Object_With_None_Of_Given_ParameterNames()
        {
            var baseQpivAttribute = new BaseQpivParameterValidationSuccessAttribute("foo");
            var simpleModel = new SimpleModel();

            Action a = () => baseQpivAttribute.IsValid(simpleModel);

            a.Should().Throw<MemberAccessException>();
        }

        [Fact]
        public void Should_Return_True_When_Parameter_Validation_Succeeds()
        {
            var baseQpivAttribute = new BaseQpivParameterValidationSuccessAttribute(SimpleModel.Property1Name);
            var simpleModel = new SimpleModel();

            var result = baseQpivAttribute.IsValid(simpleModel);

            result.Should().BeTrue();
        }

        [Fact]
        public void Should_Return_False_When_Parameter_Validation_Fails()
        {
            var baseQpivAttribute = new BaseQpivParameterValidationErrorAttribute(SimpleModel.Property1Name);
            var simpleModel = new SimpleModel();

            var result = baseQpivAttribute.IsValid(simpleModel);

            result.Should().BeFalse();
        }

        [Fact]
        public void Should_Return_True_When_Final_Validation_Succeeds()
        {
            var baseQpivAttribute = new BaseQpivFinalValidationSuccessAttribute(SimpleModel.Property1Name);
            var simpleModel = new SimpleModel();

            var result = baseQpivAttribute.IsValid(simpleModel);

            result.Should().BeTrue();
        }

        [Fact]
        public void Should_Return_False_When_Final_Validation_Fails()
        {
            var baseQpivAttribute = new BaseQpivFinalValidationErrorAttribute(SimpleModel.Property1Name);
            var simpleModel = new SimpleModel();

            var result = baseQpivAttribute.IsValid(simpleModel);

            result.Should().BeFalse();
        }

        [Fact]
        public void Should_Be_Faster_On_Each_Consequtive_Validation_Because_Of_Caching()
        {
            var baseQpivAttribute = new BaseQpivFinalValidationSuccessAttribute(SimpleModel.Property1Name);
            var simpleModel = new SimpleModel();

            var watch = Stopwatch.StartNew();
            baseQpivAttribute.IsValid(simpleModel);
            watch.Stop();
            var firstIterationTime = watch.ElapsedTicks;
            watch.Restart();
            baseQpivAttribute.IsValid(simpleModel);
            watch.Stop();
            var secondIterationTime = watch.ElapsedTicks;
            watch.Restart();
            baseQpivAttribute.IsValid(simpleModel);
            watch.Stop();
            var thirdIterationTime = watch.ElapsedTicks;

            firstIterationTime.Should().BeGreaterThan(secondIterationTime);
            firstIterationTime.Should().BeGreaterThan(thirdIterationTime);
        }
    }
}
