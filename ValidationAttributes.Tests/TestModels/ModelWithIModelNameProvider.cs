using Microsoft.AspNetCore.Mvc;

namespace QPIV.ValidationAttributes.Tests.TestModels
{
    public class ModelWithIModelNameProvider
    {
        public static string PropertyWithFromQueryName => nameof(PropertyWithFromQuery);
        public static string PropertyWithFromHeaderName => nameof(PropertyWithFromHeader);
        [FromQuery(Name = "FromQueryName")]
        public string PropertyWithFromQuery { get; set; }
        [FromHeader(Name = "FromHeaderName")]
        public string PropertyWithFromHeader { get; set; }
    }
}
