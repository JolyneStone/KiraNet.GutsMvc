namespace KiraNet.GutsMvc.View
{
    public interface IRoslynCompilerProvider
    {
        void SetCompiler(RoslynCompiler roslynCompiler);
        RoslynCompiler GetRoslynCompiler();
    }
}
