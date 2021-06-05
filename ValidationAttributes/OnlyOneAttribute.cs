using QPIV.ValidationAttributes.Base;
using QPIV.ValidationAttributes.Reflection;
using System.ComponentModel.DataAnnotations;

namespace QPIV.ValidationAttributes
{
    /// <summary>
    /// Specifies that, given a set of parameters, one and only one of them is required. 
    /// </summary>
    public class OnlyOneAttribute : BaseQpivAttribute
    {
        private bool _foundOne;

        /// <summary>
        /// Initializes a new instance of the <see cref="OnlyOneAttribute"/> class.
        /// </summary>
        /// <param name="parameterNames">Names of given parameters.</param>
        public OnlyOneAttribute(params string[] parameterNames) : base(parameterNames)
        {
            string defaultError = "Only one of: '";
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
            if (parameterValue != null)
            {
                if (_foundOne)
                {
                    return ParameterValidationResult.Error(ErrorMessage, ValidationResultMemberNames);
                }
                _foundOne = true;
            }
            return ParameterValidationResult.Wait();
        }

        protected override ValidationResult FinalValidation()
        {
            if (!_foundOne)
            {
                return new ValidationResult(ErrorMessage, ValidationResultMemberNames);
            }
            return ValidationResult.Success;
        }
    }
}
