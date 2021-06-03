using QPIV.ValidationAttributes.Base;
using QPIV.ValidationAttributes.Reflection;
using System.ComponentModel.DataAnnotations;

namespace QPIV.ValidationAttributes
{
    public class ZeroOrOneAttribute : BaseQpivAttribute
    {
        private bool _foundOne;

        public ZeroOrOneAttribute(params string[] parameterNames) : base(parameterNames)
        {
            string defaultError = "Zero or one of: '";
            foreach (var parameterName in parameterNames)
            {
                defaultError += $"{parameterName}, ";
            }
            defaultError = defaultError[0..^2] + "' required";
            ErrorMessage = defaultError;
        }

        protected override void BeforeEachValidation()
        {
            _foundOne = false;
        }

        protected override ParameterValidationResult ParameterValidation(object parameterValue, QpivParameterInfo parameterInfo)
        {
            if(parameterValue != null)
            {
                if (_foundOne)
                {
                    return ParameterValidationResult.Error(ErrorMessage);
                }
                _foundOne = true;
            }
            return ParameterValidationResult.Wait();
        }

        protected override ValidationResult FinalValidation()
        {
            return ValidationResult.Success;
        }
    }
}
