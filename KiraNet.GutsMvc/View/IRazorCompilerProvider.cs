namespace KiraNet.GutsMvc.View
{
    public interface IRazorCompilerProvider
    {
        void SetRazorCompiler(RazorCompiler razorCompiler);
        RazorCompiler GetRazorCompiler();
    }
}
