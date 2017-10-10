using KiraNet.GutsMvc.Infrastructure;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;

namespace KiraNet.GutsMvc.View
{
    public abstract class RoslynCompiler
    {
        protected static IList<MetadataReference> _applicationReferences;
        public abstract RazorPageViewBase Compile(string code, string className, string[] namespaces);

        public RoslynCompiler()
        {
            // TODO: 这里注释掉了不知道是否会出错！
            RoslynCompiler.InitApplicationReferences(new AssemblyLocator());
        }

        public static void InitApplicationReferences(IAssemblyLocator assemblyLocator)
        {
            if (assemblyLocator == null)
            {
                throw new ArgumentNullException(nameof(assemblyLocator));
            }

            if (_applicationReferences != null && _applicationReferences.Any())
            {
                return;
            }

            var metadataReferences = new List<MetadataReference>();
            var entryAssembly = Assembly.GetEntryAssembly();

            string runtimePath = Path.GetDirectoryName(typeof(object).Assembly.Location);

            var mscorlibFile = Path.Combine(runtimePath, "mscorlib.dll");
            if (File.Exists(mscorlibFile))
                metadataReferences.Add(CreateMetadataFileReference(mscorlibFile));
            else
                metadataReferences.Add(CreateMetadataFileReference(Path.Combine(runtimePath, "mscorlib.ni.dll")));

            metadataReferences.Add(CreateMetadataFileReference(typeof(object).Assembly.Location));
            metadataReferences.Add(CreateMetadataFileReference(typeof(DynamicObject).Assembly.Location));
            metadataReferences.Add(CreateMetadataFileReference(entryAssembly.Location));

            var refAssemblies = assemblyLocator.ReferenceAssemblies();
            var razorAssembly = typeof(RoslynCompiler).GetTypeInfo().Assembly;
            var razorAssemblyName = razorAssembly.GetName().FullName;
            if (refAssemblies.FirstOrDefault(x => String.Equals(
                x.GetName().FullName, 
                razorAssemblyName, 
                StringComparison.InvariantCultureIgnoreCase)) == null)
            {
                metadataReferences.Add(CreateMetadataFileReference(razorAssembly.Location));
            }

            foreach (var refassembly in refAssemblies)
            {
                metadataReferences.Add(CreateMetadataFileReference(refassembly.Location));
            }

            _applicationReferences = metadataReferences;
        }

        private static MetadataReference CreateMetadataFileReference(string path)
        {
            using (var stream = File.OpenRead(path))
            {
                var moduleMetadata = ModuleMetadata.CreateFromStream(stream, PEStreamOptions.PrefetchMetadata);
                var assemblyMetadata = AssemblyMetadata.Create(moduleMetadata);
                return assemblyMetadata.GetReference(filePath: path);
            }
        }
    }
}
