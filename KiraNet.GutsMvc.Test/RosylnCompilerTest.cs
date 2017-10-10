using KiraNet.GutsMvc.View;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit; 



namespace KiraNet.GutsMvc.Test
{
    public class RosylnCompilerTest
    {
        //[Fact]
        //public async void Compiler()
        //{
        //    var code = File.ReadAllText(@"D:\Code\KiraNet.GutsMvc\KiraNet.GutsMvc.Test\CompilerTestClass.cs");

        //    var compiler = new RazorRoslynCompiler();
        //    Type type = compiler.Compile(code);
        //    var instance = Activator.CreateInstance(type);
        //    var t = type.GetMethod("Execute", BindingFlags.Instance| BindingFlags.InvokeMethod| BindingFlags.Public).Invoke(instance, null);
        //    var s = await (t as Task<string>);
        //    var x = instance as CompilerTestBaseClass;
        //    Assert.True(x.Str == "Execute");
        //    Assert.Equal(s, "Execute");
        //}
    }
}
