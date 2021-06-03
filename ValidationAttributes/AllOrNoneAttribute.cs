using QPIV.ValidationAttributes.Base;
using QPIV.ValidationAttributes.Reflection;
using System.ComponentModel.DataAnnotations;

namespace QPIV.ValidationAttributes
{
    public class AllOrNoneAttribute : BaseQpivAttribute
    {
        private bool _foundOne;
        private int _foundCount;

        public AllOrNoneAttribute(params string[] parameterNames) : base(parameterNames)
        {
            string defaultError = "All or none of: '";
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
            _foundCount = 0;
        }

        protected override ParameterValidationResult ParameterValidation(object parameterValue, QpivParameterInfo parameterInfo)
        {
            if (parameterValue == null)
            {
                if(_foundOne)
                {
                    return ParameterValidationResult.Error(ErrorMessage, _parameterNames);
                }
            }
            else
            {
                _foundOne = true;
                _foundCount++;
            }
            return ParameterValidationResult.Wait();
        }

        protected override ValidationResult FinalValidation()
        {
            if(_foundCount != 0 && _foundCount < _parameterNames.Length)
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}