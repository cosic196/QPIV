using QPIV.ValidationAttributes.Base;
using QPIV.ValidationAttributes.Reflection;
using System.ComponentModel.DataAnnotations;

namespace QPIV.ValidationAttributes
{
    /// <summary>
    /// Specifies that, given a set of parameters, one or more of them must be included.
    /// </summary>
    public class OrAttribute : BaseQpivAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrAttribute"/> class.
        /// </summary>
        /// <param name="parameterNames">Names of given parameters.</param>
        public OrAttribute(params string[] parameterNames) : base(parameterNames)
        {
            string defaultError = "At least one of: '";
            foreach (var parameterName in parameterNames)
            {
                defaultError += $"{parameterName}, ";
            }
            defaultError = defaultError[0..^2] + "' required";
            ErrorMessage = defaultError;
        }

        protected override void BeforeEachValidation()
        {
        }

        protected override ParameterValidationResult ParameterValidation(object parameterValue, QpivParameterInfo parameterInfo)
        {
            if(parameterValue != null)
            {
                return ParameterValidationResult.Success();
            }
            return ParameterValidationResult.Wait();
        }

        protected override ValidationResult FinalValidation()
        {
            return new ValidationResult(ErrorMessage, _parameterNames);
        }
    }
}
