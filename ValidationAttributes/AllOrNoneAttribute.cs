using QPIV.ValidationAttributes.Base;
using QPIV.ValidationAttributes.Reflection;
using System.ComponentModel.DataAnnotations;

namespace QPIV.ValidationAttributes
{
    /// <summary>
    /// Specifies that, given a set of parameters, either all of them are provided or none.
    /// </summary>
    public class AllOrNoneAttribute : BaseQpivAttribute
    {
        private bool _foundOne;
        private int _foundCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="AllOrNoneAttribute"/> class.
        /// </summary>
        /// <param name="parameterNames">Names of given parameters.</param>
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
                    return ParameterValidationResult.Error(ErrorMessage, ValidationResultMemberNames);
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
                return new ValidationResult(ErrorMessage, ValidationResultMemberNames);
            }
            return ValidationResult.Success;
        }
    }
}