using KiraNet.GutsMVC.Implement;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace KiraNet.GutsMVC.Infrastructure
{
    public class ValueProviderFactoryCollection : Collection<IValueProviderFactory>
    {
        public ValueProviderFactoryCollection()
        {
        }

        public ValueProviderFactoryCollection(IList<IValueProviderFactory> factories) : base(factories)
        {
        }

        public IValueProvider GetValueProvider(ControllerContext controllerContext)
        {

            List<IValueProvider> providers = new List<IValueProvider>(this.Count);
            foreach (IValueProviderFactory factory in this)
            {
                IValueProvider provider = factory.CreateValueProvider(controllerContext);
                if (provider != null)
                {
                    providers.Add(provider);
                }
            }

            return new ValueProviderCollection(providers);
        }

    }
}