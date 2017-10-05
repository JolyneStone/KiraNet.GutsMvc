namespace KiraNet.GutsMVC.View
{
    public class ViewEngines
    {
        private static ViewEngineCollection _engines = new ViewEngineCollection() { new NVelocityViewEngine() };
        public static ViewEngineCollection Engines => _engines;
    }
}
