using QPIV.ValidationAttributes.Base;
using QPIV.ValidationAttributes.Reflection;
using System.ComponentModel.DataAnnotations;

namespace QPIV.ValidationAttributes.Tests.Stubs
{
    internal class BaseQpivFinalValidationSuccessAttribute : BaseQpivAttribute
    {
        public BaseQpivFinalValidationSuccessAttribute(params string[] parameterNames) : base(parameterNames)
        {
        }

        protected override void BeforeEachValidation()
        {
        }

        protected override ValidationResult FinalValidation()
        {
            return ValidationResult.Success;
        }

        protected override ParameterValidationResult ParameterValidation(object value, QpivParameterInfo parameterInfo)
        {
            return ParameterValidationResult.Wait();
        }
    }
}
