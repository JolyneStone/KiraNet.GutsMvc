namespace KiraNet.GutsMVC
{
    public class TempDataDictionary : ViewDataDictionary
    {
        public TempDataDictionary() : base()
        {
        }

        public TempDataDictionary(object model) : base(model)
        {
        }

        public TempDataDictionary(TempDataDictionary tempDataDictionary) : base(tempDataDictionary)
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
            if(result)
            {
                this.Remove(key);
            }

            return result;
        }
    }
}
