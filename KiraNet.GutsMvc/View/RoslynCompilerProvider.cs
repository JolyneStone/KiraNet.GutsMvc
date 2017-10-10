namespace KiraNet.GutsMvc.View
{
    public class RoslynCompilerProvider : IRoslynCompilerProvider
    {
        private static RoslynCompiler _roslynCompiler;
        public RoslynCompiler GetRoslynCompiler()
        {
            if (_roslynCompiler == null)
            {
                _roslynCompiler = new RazorRoslynCompiler();
            }

            return _roslynCompiler;
        }

        public void SetCompiler(RoslynCompiler roslynCompiler)
        {
            if (roslynCompiler != null && _roslynCompiler == null)
            {
                _roslynCompiler = roslynCompiler;
            }
        }
    }
}
