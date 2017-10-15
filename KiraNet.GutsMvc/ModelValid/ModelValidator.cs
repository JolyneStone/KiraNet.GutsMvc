using System;

namespace KiraNet.GutsMvc.ModelValid
{
    public abstract class ModelValidator
    {
        public abstract bool IsRequired { get; set; }
        public abstract ModelValidDictionary Validate(object model, Type modelType, string modelName);
    }
}
