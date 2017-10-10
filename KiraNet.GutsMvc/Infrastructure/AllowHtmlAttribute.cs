using KiraNet.GutsMvc.Metadata;
using System;

namespace KiraNet.GutsMvc
{
    public class AllowHtmlAttribute : Attribute, IMatedataAware
    {
        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.RequestValidationEnabled = false;
        }
    }
}
