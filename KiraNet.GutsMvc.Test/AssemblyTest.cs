using KiraNet.GutsMVC.Helper;
using System;
using Xunit;

namespace KiraNet.GutsMVC.Test
{
    public class AssemblyTest
    { 
        [Fact]
        public void GetAssemblyTest()
        {
            var baseStr = AppContext.BaseDirectory;

            var assemblies = new AssemblyLocator().GetAssemblies();
            Assert.NotEmpty(assemblies);
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
