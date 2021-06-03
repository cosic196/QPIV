using System;
using System.Reflection;

namespace QPIV.ValidationAttributes.Reflection
{
    public class QpivParameterInfo
    {
        public string Name { get; }
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
        }

        public QpivParameterInfo(PropertyInfo propertyInfo)
        {
            Name = propertyInfo.Name;
            ParameterType = propertyInfo.PropertyType;
            PropertyInfo = propertyInfo;
            IsField = false;
        }

        public object GetValue(object obj)
        {
            if(IsField)
            {
                return FieldInfo.GetValue(obj);
            }
            return PropertyInfo.GetValue(obj);
        }
    }
}
