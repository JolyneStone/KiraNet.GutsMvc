namespace KiraNet.GutsMVC.Infrastructure
{
    public static class ValueProviderFactories
    {
        private static readonly ValueProviderFactoryCollection _factories = new ValueProviderFactoryCollection()
        {
            //new ChildActionValueProviderFactory(),
            new QueryStringValueProviderFactory(),
            new FormValueProviderFactory(),
            new JsonValueProviderFactory(),
            //new RouteDataValueProviderFactory(),
            new FileCollectionValueProviderFactory(),
        };

        public static ValueProviderFactoryCollection Factories { get => _factories; }
    }
}
