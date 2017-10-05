using KiraNet.GutsMVC.Metadata;
using System;

namespace KiraNet.GutsMVC
{
    public class AllowHtmlAttribute : Attribute, IMatedataAware
    {
        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.RequestValidationEnabled = false;
        }
    }
}
