namespace KiraNet.GutsMvc.View
{
    public class RazorCompilerProvider : IRazorCompilerProvider
    {
        private RazorCompiler _razorCompiler;

        public RazorCompiler GetRazorCompiler()
        {
            if (_razorCompiler == null)
            {
                _razorCompiler = new RazorPageCompiler();
            }

            return _razorCompiler;
        }

        public void SetRazorCompiler(RazorCompiler razorCompiler)
        {
            if (razorCompiler != null && _razorCompiler==null)
            {
                _razorCompiler = razorCompiler;
            }
        }
    }
}
