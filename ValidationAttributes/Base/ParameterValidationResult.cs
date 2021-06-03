using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QPIV.ValidationAttributes.Base
{
    public class ParameterValidationResult
    {
        public bool WaitForFinalValidation { get; }
        public ValidationResult ValidationResult { get; }

        private ParameterValidationResult(bool wait, ValidationResult result)
        {
            WaitForFinalValidation = wait;
            ValidationResult = result;
        }

        public static ParameterValidationResult Wait()
        {
            return new ParameterValidationResult(true, null);
        }

        public static ParameterValidationResult Success()
        {
            return new ParameterValidationResult(false, ValidationResult.Success);
        }

        public static ParameterValidationResult Error(string error, IEnumerable<string> memberNames = null)
        {
            return new ParameterValidationResult(false, new ValidationResult(error, memberNames));
        }
    }
}
