using System.Threading.Tasks;
using System;

//namespace KiraNet.GutsMvc.Test
//{
    public class CompilerTestClass : KiraNet.GutsMvc.View.CompilerTestBaseClass
    {
        public override async Task<string> Execute()
        {
            await Task.CompletedTask;
            Str = "Execute";
            return "Execute";
        }
    }
//}
