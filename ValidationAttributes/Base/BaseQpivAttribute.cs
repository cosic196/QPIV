using QPIV.ValidationAttributes.Reflection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace QPIV.ValidationAttributes.Base
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public abstract class BaseQpivAttribute : ValidationAttribute
    {
        private Dictionary<string, QpivParameterInfo> _parameters;
        protected readonly string[] _parameterNames;
        protected IEnumerable<string> ValidationResultMemberNames { get; private set; }

        public BaseQpivAttribute(params string[] parameterNames)
        {
            if(parameterNames == null || parameterNames.Length == 0)
            {
                throw new ArgumentNullException("Can't use null or empty string for parameter names.");
            }
            parameterNames = parameterNames.Distinct().ToArray();
            foreach (var parameter in parameterNames)
            {
                if (string.IsNullOrEmpty(parameter))
                {
                    throw new ArgumentNullException("Can't use null or empty string for parameter names.");
                }
            }
            _parameterNames = parameterNames;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            BeforeEachValidation();
            if (value == null)
            {
                return ValidationResult.Success;
            }

            if(_parameters == null)
            {
                _parameters = value.GetQpivParameters(_parameterNames);
                ValidationResultMemberNames = _parameters.Select(x => x.Value.ModelName);
            }

            foreach (var parameterName in _parameterNames)
            {
                var parameterInfo = _parameters[parameterName];
                var paramRes = ParameterValidation(parameterInfo.GetValue(value), parameterInfo);
                if (!paramRes.WaitForFinalValidation)
                {
                    return paramRes.ValidationResult;
                }
            }
            return FinalValidation();
        }

        protected abstract void BeforeEachValidation();
        protected abstract ParameterValidationResult ParameterValidation(object parameterValue, QpivParameterInfo parameterInfo);
        protected abstract ValidationResult FinalValidation();
    }
}
