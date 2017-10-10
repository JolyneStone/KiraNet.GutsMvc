using KiraNet.GutsMvc.Metadata;
using System;

namespace KiraNet.GutsMvc
{
    /// <summary>
    /// 用于为Model元数据附加一些信息
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Property,
        AllowMultiple = false,
        Inherited = true)]
    public class AdditionalMetadataAttribute : Attribute, IMatedataAware
    {
        public string Name { get; }
        public object Value { get; }

        public AdditionalMetadataAttribute(string name, object value)
        {
            if(String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
            Value = value;
        }
        public void OnMetadataCreated(ModelMetadata metadata)
        {
            if(metadata == null)
            {
                throw new ArgumentNullException(nameof(metadata));
            }

            metadata.AdditionalValues[Name] = Value;
        }
    }
}
