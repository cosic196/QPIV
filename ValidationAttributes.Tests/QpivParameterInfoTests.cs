using QPIV.ValidationAttributes.Tests.TestModels;
using QPIV.ValidationAttributes.Reflection;
using Xunit;
using FluentAssertions;

namespace QPIV.ValidationAttributes.Tests
{
    public class QpivParameterInfoTests
    {
        [Fact]
        public void Should_Get_ModelName_From_Attribute_With_IModelNameProvider_When_Constructed()
        {
            var propertyWithFromQuery = typeof(ModelWithIModelNameProvider).GetProperty(ModelWithIModelNameProvider.PropertyWithFromQueryName);
            var propertyWithFromHeader = typeof(ModelWithIModelNameProvider).GetProperty(ModelWithIModelNameProvider.PropertyWithFromHeaderName);

            var qpivParameterInfoWithFromQuery = new QpivParameterInfo(propertyWithFromQuery);
            var qpivParameterInfoWithFromHeader = new QpivParameterInfo(propertyWithFromHeader);

            qpivParameterInfoWithFromQuery.ModelName.Should().Be("FromQueryName");
            qpivParameterInfoWithFromQuery.Name.Should().Be(ModelWithIModelNameProvider.PropertyWithFromQueryName);
            qpivParameterInfoWithFromHeader.ModelName.Should().Be("FromHeaderName");
            qpivParameterInfoWithFromHeader.Name.Should().Be(ModelWithIModelNameProvider.PropertyWithFromHeaderName);
        }
    }
}
