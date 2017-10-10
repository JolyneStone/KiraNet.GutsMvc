using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

namespace KiraNet.GutsMvc.Metadata
{
    public class DefaultModelMetadataProvider : ModelMetadataProvider
    {
        public DefaultModelMetadataProvider(IMemoryCache cache) : base(cache)
        {
            Current = this;
        }


        protected override ModelMetadata CreateMetadataFromPrototype(ModelMetadata prototype, Func<object> modelAccessor)
        {
            if (prototype is DefaultModelMetadata p)
                return new DefaultModelMetadata(p, modelAccessor);

            return null;
        }

        protected override ModelMetadata CreateMetadataPrototype(IEnumerable<Attribute> attributes, Type containerType, Type modelType, string propetyName)
        {
            return new DefaultModelMetadata(this, containerType, modelType, propetyName, attributes);
        }
    }
}