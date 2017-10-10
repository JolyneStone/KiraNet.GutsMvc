using Microsoft.Extensions.Caching.Memory;
using System;
using System.IO;

namespace KiraNet.GutsMvc.View
{
    public abstract class RazorCompiler
    {
        public RazorPageGeneratorResult CompileRazorPage(string folderName, string viewName, Type modelType)
        {
            var targetPath = Path.Combine(ViewPath.Path, folderName, viewName + ".cshtml");
            var razorResult = Generate("KiraNet.GutsMvc.View", targetPath, modelType);

            return razorResult;
        }

        protected abstract RazorPageGeneratorResult Generate(string rootNamespace, string targetPath, Type modelType);
    }
}
