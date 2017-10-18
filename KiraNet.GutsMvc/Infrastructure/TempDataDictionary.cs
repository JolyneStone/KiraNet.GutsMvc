using System;

namespace KiraNet.GutsMvc
{
    public class TempDataDictionary : ViewDataDictionary
    {
        public TempDataDictionary(IServiceProvider services) : base(services)
        {
        }

        public TempDataDictionary(IServiceProvider services, object model) : base(services, model)
        {
        }

        public TempDataDictionary(IServiceProvider services, Type modelType) : base(services, modelType)
        {
        }

        public TempDataDictionary(ViewDataDictionary tempDataDictionary) : base(tempDataDictionary)
        {
        }

        public override object this[string key]
        {
            get
            {
                var result = base[key];
                this.Remove(key);

                return result;
            }
            set => base[key] = value;
        }

        public override bool TryGetValue(string key, out object value)
        {
            var result = base.TryGetValue(key, out value);
            if (result)
            {
                this.Remove(key);
            }

            return result;
        }
    }
}
