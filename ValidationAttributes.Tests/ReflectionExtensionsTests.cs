using QPIV.ValidationAttributes.Tests.TestModels;
using QPIV.ValidationAttributes.Reflection;
using Xunit;
using FluentAssertions;

namespace QPIV.ValidationAttributes.Tests
{
    public class ReflectionExtensionsTests
    {
        [Fact]
        public void Should_Get_Fields_And_Properties_From_Class_Instance_As_QpivParameterInfo_Dictionary_Values()
        {
            var simpleModel = new SimpleModel();

            var result = simpleModel.GetQpivParameters(SimpleModel.Property1Name, SimpleModel.Property2Name, SimpleModel.FieldName);

            result.Should().HaveCount(3);
            result.Should().ContainKey(SimpleModel.Property1Name);
            result.Should().ContainKey(SimpleModel.Property2Name);
            result.Should().ContainKey(SimpleModel.FieldName);
            result[SimpleModel.Property1Name].IsProperty.Should().BeTrue();
            result[SimpleModel.Property2Name].IsProperty.Should().BeTrue();
            result[SimpleModel.FieldName].IsProperty.Should().BeFalse();
            result[SimpleModel.Property1Name].IsField.Should().BeFalse();
            result[SimpleModel.Property2Name].IsField.Should().BeFalse();
            result[SimpleModel.FieldName].IsField.Should().BeTrue();
            result[SimpleModel.Property1Name].Name.Should().Be(SimpleModel.Property1Name);
            result[SimpleModel.Property2Name].Name.Should().Be(SimpleModel.Property2Name);
            result[SimpleModel.FieldName].Name.Should().Be(SimpleModel.FieldName);
            result[SimpleModel.Property1Name].FieldInfo.Should().BeNull();
            result[SimpleModel.Property2Name].FieldInfo.Should().BeNull();
            result[SimpleModel.FieldName].FieldInfo.Should().NotBeNull();
            result[SimpleModel.Property1Name].PropertyInfo.Should().NotBeNull();
            result[SimpleModel.Property2Name].PropertyInfo.Should().NotBeNull();
            result[SimpleModel.FieldName].PropertyInfo.Should().BeNull();
        }
    }
}
