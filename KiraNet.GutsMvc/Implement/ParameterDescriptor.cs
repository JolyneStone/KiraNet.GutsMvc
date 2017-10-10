using System;
using System.Reflection;

namespace KiraNet.GutsMvc.Implement
{
    public class ParameterDescriptor
    {
        public string ParameterName { get; set; }

        public Type ParameterType { get; set; }

        public ParameterInfo ParameterInfo { get; set; }

        public object ParameterValue { get; set; }
    }
}