using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace KiraNet.GutsMvc.View
{
    internal class RazorTemplateProvider : ITemplateProvider<RazorPageViewBase>
    {
        private string _folderName;
        private const string _extension = ".cshtml";

        public RazorTemplateProvider(string folderName)
        {
            _folderName = folderName;
        }

        public async Task<RazorPageViewBase> CompileTemplate(string viewName, ViewContext viewContext)
        {
            //var t = Task.Factory.StartNew<RazorPageViewBase>(() =>
            //{
            //    // 获取原始View代码
            //    var viewTemplate = LoadTemplateContent(viewName);
            //    var razorCompiler = new RazorCompilerProvider().GetRazorCompiler();

            //    // 编译Razor代码
            //    RazorPageGeneratorResult razorResult = razorCompiler.CompileRazorPage(_folderName, viewName, viewContext.ModelType);

            //    var (code, namespaces) = SubCodeContent(razorResult.GeneratedCode);

            //    // 编译C#代码
            //    var roslynCompiler = new RoslynCompilerProvider().GetRoslynCompiler();
            //    var viewInstance = roslynCompiler.Compile(code, razorResult.ClassName, namespaces);

            //    return viewInstance;
            //});

            //return t;

            // 获取原始View代码
            var viewTemplate = await LoadTemplateContentAsync(viewName);
            var razorCompiler = new RazorCompilerProvider().GetRazorCompiler();

            // 编译Razor代码
            RazorPageGeneratorResult razorResult = razorCompiler.CompileRazorPage(_folderName, viewName, viewContext.ModelType);

            var (code, namespaces) = SubCodeContent(razorResult.GeneratedCode);

            // 编译C#代码
            var roslynCompiler = new RoslynCompilerProvider().GetRoslynCompiler();
            var viewInstance = roslynCompiler.Compile(code, razorResult.ClassName, namespaces);

            return viewInstance;
        }

        private (string, string[]) SubCodeContent(string code)
        {
            // 由于我用生成C#代码采取的方式是直接用Roslyn编译脚本，因此需要删去Razor代码中有关命名空间的字符
            var start = code.IndexOf("internal");
            var end = code.LastIndexOf("}");
            var cSharpCode = code.Substring(start, end - start);

            // 获取Razor中using命名空间的字符
            var rawCode = code.Substring(0, start);
            var matches = Regex.Matches(rawCode, @"using[\b\s](?<namespace>[\w\.]+?);");
            var namespaces = new List<string>();
            foreach(Match match in matches)
            {
                var namespaceStr = match.Groups["namespace"]?.Value;
                if(!String.IsNullOrWhiteSpace(namespaceStr))
                {
                    namespaces.Add(namespaceStr);
                }
            }

            return (cSharpCode, namespaces.ToArray());
        }


        private string LoadTemplateContent(string viewName)
        {
            if (string.IsNullOrWhiteSpace(viewName))
            {
                throw new ArgumentNullException(nameof(viewName));
            }

            var path = Path.Combine(ViewPath.Path, _folderName, viewName + _extension);

            var file = new FileInfo(path);
            if (!file.Exists)
            {
                throw new FileNotFoundException("无法找到指定的视图文件");
            }

            using (var reader = new StreamReader(file.OpenRead(), Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        private async Task<string> LoadTemplateContentAsync(string viewName)
        {
            if (string.IsNullOrWhiteSpace(viewName))
            {
                throw new ArgumentNullException(nameof(viewName));
            }

            var path = Path.Combine(ViewPath.Path, _folderName, viewName + _extension);

            var file = new FileInfo(path);
            if (!file.Exists)
            {
                throw new FileNotFoundException("无法找到指定的视图文件");
            }

            using (var reader = new StreamReader(file.OpenRead(), Encoding.UTF8))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}