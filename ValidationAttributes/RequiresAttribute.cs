using QPIV.ValidationAttributes.Base;
using QPIV.ValidationAttributes.Reflection;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace QPIV.ValidationAttributes
{
    public class RequiresAttribute : BaseQpivAttribute
    {
        private readonly string _targetParameterName;
        private bool _foundOne;

        public RequiresAttribute(string targetParameterName, params string[] requiredParameterNames) : base(requiredParameterNames.Prepend(targetParameterName).ToArray())
        {
            if(requiredParameterNames.Contains(targetParameterName))
            {
                throw new ArgumentException($"Can't use parameter '{targetParameterName}' as both targetParameter and of required parameters.");
            }

            string defaultError = "At least one of: '";
            foreach (var parameterName in requiredParameterNames)
            {
                defaultError += $"{parameterName}, ";
            }
            defaultError = defaultError[0..^2] + $"' required if {targetParameterName} is set.";
            ErrorMessage = defaultError;
            _targetParameterName = targetParameterName;
        }

        protected override void BeforeEachValidation()
        {
            _foundOne = false;
        }

        protected override ParameterValidationResult ParameterValidation(object parameterValue, QpivParameterInfo parameterInfo)
        {
            if(parameterInfo.Name == _targetParameterName)
            {
                if(parameterValue == null)
                {
                    return ParameterValidationResult.Success();
                }
                return ParameterValidationResult.Wait();
            }
            if(parameterValue != null)
            {
                _foundOne = true;
            }
            return ParameterValidationResult.Wait();
        }

        protected override ValidationResult FinalValidation()
        {
            if(_foundOne)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(ErrorMessage, _parameterNames);
        }
    }
}