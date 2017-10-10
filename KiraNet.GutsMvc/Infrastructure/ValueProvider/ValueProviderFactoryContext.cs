using KiraNet.GutsMvc.Implement;
using System;
using System.Collections.Generic;

namespace KiraNet.GutsMvc.Infrastructure
{
    public class ValueProviderFactoryContext
    {
        public ValueProviderFactoryContext(ActionContext context)
        {
            ActionContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public ActionContext ActionContext { get; }


        public IList<IValueProvider> ValueProviders { get; } = new List<IValueProvider>();

    }
}