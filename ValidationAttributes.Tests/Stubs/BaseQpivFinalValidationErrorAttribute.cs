using QPIV.ValidationAttributes.Base;
using QPIV.ValidationAttributes.Reflection;
using System.ComponentModel.DataAnnotations;

namespace QPIV.ValidationAttributes.Tests.Stubs
{
    internal class BaseQpivFinalValidationErrorAttribute : BaseQpivAttribute
    {
        public BaseQpivFinalValidationErrorAttribute(params string[] parameterNames) : base(parameterNames)
        {
        }

        protected override void BeforeEachValidation()
        {
        }

        protected override ValidationResult FinalValidation()
        {
            return new ValidationResult("");
        }

        protected override ParameterValidationResult ParameterValidation(object value, QpivParameterInfo parameterInfo)
        {
            return ParameterValidationResult.Wait();
        }
    }
}
