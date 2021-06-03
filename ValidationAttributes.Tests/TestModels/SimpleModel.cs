namespace QPIV.ValidationAttributes.Tests.TestModels
{
    internal class SimpleModel
    {
        public static string Property1Name => nameof(SimpleModelProperty1);
        public static string Property2Name => nameof(SimpleModelProperty2);
        public static string FieldName => nameof(simpleModelField);
        public int? SimpleModelProperty1 { get; set; }
        public double? SimpleModelProperty2 { get; set; }
        public string simpleModelField;
    }
}
