using System;
using System.Globalization;

namespace KiraNet.GutsMVC.Infrastructure
{
    /// <summary>
    /// 用于封装针对IFormCollection的VauleProvider
    /// </summary>
    public class FormValueProvider : ValueProvider<FormCollectionValueProviderResultWrapper>
    {
        //private readonly Dictionary<string,  ValueProviderResultWrapper> _values = new Dictionary<string, FormCollectionValueProviderResultWrapper>(StringComparer.OrdinalIgnoreCase);
        public FormValueProvider(IFormCollection form):this(form, CultureInfo.CurrentCulture)
        {
        }

        public FormValueProvider(IFormCollection form, CultureInfo culture)
            :base(culture)
        {
           if(form == null)
            {
                throw new ArgumentNullException(nameof(form));
            }

           foreach(string key in form.Keys)
            {
                _values[key] = new FormCollectionValueProviderResultWrapper(key, form, culture);
            }
        }
    }
}
