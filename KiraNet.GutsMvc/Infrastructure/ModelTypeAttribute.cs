using System;

namespace KiraNet.GutsMvc
{
    [AttributeUsage(
        AttributeTargets.Method,
        AllowMultiple = false,
        Inherited =true)]
    public class ModelTypeAttribute:Attribute
    {
        public Type ModelType { get; set; }

        public ModelTypeAttribute(Type modelType)
        {
            if(modelType!=null)
            {
                ModelType = modelType;
            }
        }
    }
}
