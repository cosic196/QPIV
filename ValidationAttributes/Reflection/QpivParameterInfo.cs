using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Reflection;

namespace QPIV.ValidationAttributes.Reflection
{
    public class QpivParameterInfo
    {
        public string Name { get; }
        public string ModelName { get; }
        public Type ParameterType { get; }
        public FieldInfo FieldInfo { get; }
        public PropertyInfo PropertyInfo { get; }
        public bool IsField { get; }
        public bool IsProperty => !IsField;

        public QpivParameterInfo(FieldInfo fieldInfo)
        {
            Name = fieldInfo.Name;
            ParameterType = fieldInfo.FieldType;
            FieldInfo = fieldInfo;
            IsField = true;
            ModelName = GetModelName(fieldInfo);
        }

        public QpivParameterInfo(PropertyInfo propertyInfo)
        {
            Name = propertyInfo.Name;
            ParameterType = propertyInfo.PropertyType;
            PropertyInfo = propertyInfo;
            IsField = false;
            ModelName = GetModelName(propertyInfo);
        }

        public object GetValue(object obj)
        {
            if(IsField)
            {
                return FieldInfo.GetValue(obj);
            }
            return PropertyInfo.GetValue(obj);
        }

        private string GetModelName(MemberInfo memberInfo)
        {
            var attributes = memberInfo.GetCustomAttributes();
            foreach (var attribute in attributes)
            {
                if(attribute is IModelNameProvider)
                {
                    return (attribute as IModelNameProvider).Name;
                }
            }
            return memberInfo.Name;
        }
    }
}
