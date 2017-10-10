using KiraNet.GutsMvc.Helper;
using System;
using Xunit;
using KiraNet.GutsMvc.Infrastructure;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Microsoft.CodeAnalysis;
using System.Reflection.PortableExecutable;
using System.Dynamic;

namespace KiraNet.GutsMvc.Test
{
    public class AssemblyTest
    {
        [Fact]
        public void GetAssemblyTest()
        {
            var baseStr = AppContext.BaseDirectory;

            var assemblies = new AssemblyLocator().DependencyAssemblies();
            Assert.NotEmpty(assemblies);

           // RoslynCompiler.InitApplicationReferences(new AssemblyLocator());
        }

        [Fact]
        public void ReflectedTest()
        {
            var type = typeof(AssemblyTest).GetMethod("ReflectedTest").ReflectedType;
            Assert.True(type != typeof(void));
            Assert.True(type == typeof(AssemblyTest));
        }
    }
}
