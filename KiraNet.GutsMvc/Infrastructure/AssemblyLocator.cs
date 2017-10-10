using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace KiraNet.GutsMvc.Infrastructure
{
    public class AssemblyLocator : IAssemblyLocator
    {
        /// <summary>
        /// 本程序集作为根程序集
        /// </summary>
        private static readonly Assembly _assemblyRoot = typeof(AssemblyLocator).Assembly;
        /// <summary>
        /// 可执行文件
        /// </summary>
        private readonly Assembly _entryAssembly;
        /// <summary>
        /// 本程序集的依赖上下文
        /// </summary>
        private readonly DependencyContext _dependencyContext;

        private IEnumerable<Assembly> _dependencyAssemblies;
        private IEnumerable<Assembly> _referenceAssemblies;

        public AssemblyLocator()
        {
            _entryAssembly = Assembly.GetEntryAssembly();
            _dependencyContext = DependencyContext.Load(_entryAssembly);
        }

        /// <summary>
        /// 获取所有引用本程序集的程序集
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Assembly> DependencyAssemblies()
        {
            if (_dependencyContext == null)
                return new[] { _entryAssembly }; // 将当前程序集作为唯一的候选

            if (_dependencyAssemblies != null && _dependencyAssemblies.Any())
                return _dependencyAssemblies;

            // 详情请查看 → https://stackoverflow.com/questions/40908568/assembly-loading-in-net-core
            // 筛选出所有引用了本程序集的程序集
            _dependencyAssemblies = _dependencyContext.RuntimeLibraries
                .Where(library => IsCandidateLibrary(library))
                .SelectMany(library => library.GetDefaultAssemblyNames(_dependencyContext))
                .Select(assembly => Assembly.Load(new AssemblyName(assembly.Name))); // 如果程序集已经加载则不会重复加载

            return _dependencyAssemblies;
        }

        public IEnumerable<Assembly> ReferenceAssemblies()
        {
            if(_referenceAssemblies !=null && _referenceAssemblies.Any())
            {
                return _referenceAssemblies;
            }

            return _referenceAssemblies = _entryAssembly
                .GetReferencedAssemblies()
                .Select(assemblyName => Assembly.Load(assemblyName));
        }

        /// <summary>
        /// 判断运行时库所依赖的库是否包含了本程序集
        /// </summary>
        /// <param name="library"></param>
        /// <returns></returns>
        private bool IsCandidateLibrary(RuntimeLibrary library)
        {
            var assemblyRootName = _assemblyRoot.GetName().Name;
            return library.Dependencies.Any(dependency =>
                String.Equals(dependency.Name, assemblyRootName, StringComparison.OrdinalIgnoreCase)) ||
                String.Equals(library.Name, assemblyRootName, StringComparison.OrdinalIgnoreCase);
        }
    }
}
