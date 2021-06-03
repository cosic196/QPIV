using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace QPIV.ValidationAttributes.Reflection
{
    public static class ReflectionExtensions
    {
        public static Dictionary<string, QpivParameterInfo> GetQpivParameters<T>(this T obj, params string[] parameterNames) where T : class
        {
            var parameters = new Dictionary<string, QpivParameterInfo>();
            var type = obj.GetType();
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;
            var fields = type.GetFields(bindingFlags);
            var properties = type.GetProperties(bindingFlags);

            foreach (var parameterName in parameterNames)
            {
                var field = fields.FirstOrDefault(x => x.Name == parameterName);
                var property = properties.FirstOrDefault(x => x.Name == parameterName);
                if (field != null)
                {
                    parameters.Add(field.Name, new QpivParameterInfo(field));
                }
                else if (property != null)
                {
                    parameters.Add(property.Name, new QpivParameterInfo(property));
                }
                else
                {
                    throw new MemberAccessException($"'{parameterName}' parameter not found in '{type}'");
                }
            }
            return parameters;
        }
    }
}
