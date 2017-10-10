using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Xunit;

namespace KiraNet.GutsMvc.Test
{
    public interface IT
    {
        int X { get; set; }
    }

    public class T : IT
    {
        public T() { X = 1; }
        public int X { get; set; }
    }

    public class TT
    {
        public int X { get; set; }
        public TT(IT t, int x)
        {
            X = x;
        }
    }

    public class TT2
    {
        public TT2(IT t)
        { }
    }

    public class DITest
    {
        [Fact]
        public void ObjectFactoryTest1()
        {
            var serviceProvider = new ServiceCollection()
                .AddScoped<TT, TT>()
                .AddScoped<IT, T>()
                .AddScoped<TT2, TT2>()
                .BuildServiceProvider();
            Func<Type, ObjectFactory> _createFactory =
                        (type) => ActivatorUtilities.CreateFactory(type, new Type[] { typeof(IT), typeof(int) });

            Assert.NotNull(_createFactory(typeof(TT))(serviceProvider, arguments: new object[] { null, 1 }));
            Assert.NotNull(ActivatorUtilities.CreateFactory(typeof(TT2), Type.EmptyTypes)(serviceProvider, null));
        }

        [Fact]
        public void ObjectFactoryTest2()
        {
            //var serviceProvider = new ServiceCollection()
            //    .AddScoped<IT, T>()
            //    //.AddScoped<TT2, TT2>()
            //    .BuildServiceProvider();

            //var t = serviceProvider.GetRequiredService<TT2>(); // 不注册注入的话，这里无法通过
            //Assert.NotNull(t);


            var serviceProvider = new ServiceCollection()
                .AddScoped<IT, T>()
                //.AddScoped<TT2, TT2>()
                .BuildServiceProvider();
            Func<Type, ObjectFactory> _createFactory =
                        (type) => ActivatorUtilities.CreateFactory(type, null);

            Assert.NotNull(_createFactory(typeof(TT2))(serviceProvider, arguments: null));
        }

        public void Test(ref int x, out int y)
        {
            y = 1;
            x = 1;
            return;
        }

        [Fact]
        public void O()
        {
            var x = this.GetType().GetMethod("Test").GetParameters().Count(y => y.IsOut || y.ParameterType.IsByRef);
            Assert.Equal(x, 2);
        }
    }
}
