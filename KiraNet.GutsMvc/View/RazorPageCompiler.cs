using KiraNet.GutsMvc.Helper;
using Microsoft.AspNetCore.Razor.Language;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace KiraNet.GutsMvc.View
{
    internal class RazorPageCompiler : RazorCompiler
    {
        object _sync = new object();
        protected override RazorPageGeneratorResult Generate(string rootNamespace, string targetPath, Type modelType)
        {
            var baseTypeName = "KiraNet.GutsMvc.View.RazorPageViewBase";
            if (modelType != null)
            {
                baseTypeName = $"{baseTypeName}<{TypeHelper.GetTypeName(modelType)}>";
            }

            var className = String.Empty;
            var razorEngine = RazorEngine.Create(builder =>
            {
                builder
                    .SetNamespace(rootNamespace)
                    .SetBaseType(baseTypeName)
                    .ConfigureClass((document, @class) =>
                    {
                        @class.ClassName = className = Path.GetFileNameWithoutExtension(document.Source.FilePath) + DateTime.Now.Ticks;
                        @class.Modifiers.Clear();
                        @class.Modifiers.Add("internal");
                    });

                builder.Features.Add(new SuppressChecksumOptionsFeature());
            });

            var razorProject = RazorProject.Create(ViewPath.Path);
            var templateEngine = new RazorTemplateEngine(razorEngine, razorProject);

            templateEngine.Options.DefaultImports = RazorSourceDocument.Create(@"
                @using System
                @using System.Threading.Tasks
                @using System.Collections.Generic
                @using System.Collections
                ", fileName: null);


            //templateEngine.Options.DefaultImports = RazorSourceDocument.Create("", null);

            var cshtmlFile = razorProject.GetItem(targetPath);
            var razorResult = GenerateCodeFile(templateEngine, cshtmlFile);
            razorResult.ClassName = className;
            return razorResult;
        }

        private RazorPageGeneratorResult GenerateCodeFile(RazorTemplateEngine templateEngine, RazorProjectItem projectItem)
        {
            var projectItemWrapper = new FileSystemRazorProjectItemWrapper(projectItem);
            var cSharpDocument = templateEngine.GenerateCode(projectItemWrapper);
            if (cSharpDocument.Diagnostics.Any())
            {
                var diagnostics = string.Join(Environment.NewLine, cSharpDocument.Diagnostics);
                throw new InvalidOperationException($"无法生成Razor页面代码，一个或多个错误发生:{diagnostics}");
            }

            var generatedCodeFilePath = Path.ChangeExtension(projectItem.PhysicalPath, ".Designer.gutscs");
            return new RazorPageGeneratorResult
            {
                FilePath = generatedCodeFilePath,
                GeneratedCode = cSharpDocument.GeneratedCode,
            };
        }



        private class SuppressChecksumOptionsFeature : RazorEngineFeatureBase, IConfigureRazorCodeGenerationOptionsFeature
        {
            public int Order { get; set; }

            public void Configure(RazorCodeGenerationOptionsBuilder options)
            {
                if (options == null)
                {
                    throw new ArgumentNullException(nameof(options));
                }

                options.SuppressChecksum = true;
            }
        }

        private class FileSystemRazorProjectItemWrapper : RazorProjectItem
        {
            private readonly RazorProjectItem _source;
            private readonly object _sync = new object();

            public FileSystemRazorProjectItemWrapper(RazorProjectItem item)
            {
                _source = item;
            }

            public override string BasePath => _source.BasePath;

            public override string FilePath => _source.FilePath;

            public override string PhysicalPath => _source.FileName;

            public override bool Exists => _source.Exists;

            public override Stream Read()
            {
                lock (_sync)
                {
                    var processedContent = ProcessFileIncludes();
                    return new MemoryStream(Encoding.UTF8.GetBytes(processedContent));
                }
            }

            private string ProcessFileIncludes()
            {
                var basePath = System.IO.Path.GetDirectoryName(_source.PhysicalPath);
                var cshtmlContent = File.ReadAllText(_source.PhysicalPath);

                var startMatch = "<%$ include: ";
                var endMatch = " %>";
                var startIndex = 0;
                while (startIndex < cshtmlContent.Length)
                {
                    startIndex = cshtmlContent.IndexOf(startMatch, startIndex);
                    if (startIndex == -1)
                    {
                        break;
                    }
                    var endIndex = cshtmlContent.IndexOf(endMatch, startIndex);
                    if (endIndex == -1)
                    {
                        throw new InvalidOperationException($"include 格式错误。 example: <%$ include: Index.cshtml %>");
                    }

                    var includeFileName = cshtmlContent.Substring(startIndex + startMatch.Length, endIndex - (startIndex + startMatch.Length));
                    var includeFileContent = File.ReadAllText(Path.Combine(basePath, includeFileName));
                    cshtmlContent = cshtmlContent.Substring(0, startIndex) + includeFileContent + cshtmlContent.Substring(endIndex + endMatch.Length);
                    startIndex = startIndex + includeFileContent.Length;
                }

                return cshtmlContent;
            }
        }
    }
}
