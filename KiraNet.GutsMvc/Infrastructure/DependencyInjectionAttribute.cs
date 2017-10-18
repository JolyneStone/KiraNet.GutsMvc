using System;

namespace KiraNet.GutsMvc
{
    public enum DependencyType
    {
        Transient,
        Scoped,
        Singleton
    }

    [AttributeUsage(
        AttributeTargets.Class |
        AttributeTargets.Struct,
        AllowMultiple = false,
        Inherited = true)]
    public class DependencyInjectionAttribute : Attribute
    {
        private Type _serviceType;
        public Type ServiceType
        {
            get
            {
                if(_serviceType == null)
                {
                    _serviceType = ImplementType;
                }

                return _serviceType;
            }
            set => _serviceType = value;
        }
        public Type ImplementType { get; set; }

        public DependencyType Dependency { get; set; }

        public DependencyInjectionAttribute(DependencyType dependencyType)
        {
            Dependency = dependencyType;
        }

        public DependencyInjectionAttribute(Type implementType, DependencyType dependencyType)
        {
            ImplementType = implementType ?? throw new ArgumentNullException(nameof(implementType));
            Dependency = dependencyType;
        }

        public DependencyInjectionAttribute(Type serviceType, Type implemtType, DependencyType dependencyType)
        {
            ImplementType = implemtType ?? throw new ArgumentNullException(nameof(implemtType));
            ServiceType = serviceType ?? implemtType;
            Dependency = dependencyType;
        }
    }

    [AttributeUsage(
        AttributeTargets.Class |
        AttributeTargets.Struct,
        AllowMultiple = false,
        Inherited = true)]
    public class TransientDependencyAttribute : DependencyInjectionAttribute
    {
        public TransientDependencyAttribute():base(DependencyType.Transient)
        {
        }

        public TransientDependencyAttribute(Type implementType) : base(implementType, DependencyType.Transient)
        {
        }

        public TransientDependencyAttribute(Type serviceType, Type implemtType) : base(serviceType, implemtType, DependencyType.Transient)
        {
        }
    }

    [AttributeUsage(
        AttributeTargets.Class |
        AttributeTargets.Struct,
        AllowMultiple = false,
        Inherited = true)]
    public class ScopedDependencyAttribute : DependencyInjectionAttribute
    {
        public ScopedDependencyAttribute() : base(DependencyType.Scoped)
        {
        }
        public ScopedDependencyAttribute(Type implementType) : base(implementType, DependencyType.Scoped)
        {
        }

        public ScopedDependencyAttribute(Type serviceType, Type implemtType) : base(serviceType, implemtType, DependencyType.Scoped)
        {
        }
    }

    [AttributeUsage(
        AttributeTargets.Class |
        AttributeTargets.Struct,
        AllowMultiple = false,
        Inherited = true)]
    public class SingletonDependencyAttribute : DependencyInjectionAttribute
    {
        public SingletonDependencyAttribute():base(DependencyType.Singleton)
        {
        }
        public SingletonDependencyAttribute(Type implementType) : base(implementType, DependencyType.Singleton)
        {
        }

        public SingletonDependencyAttribute(Type serviceType, Type implemtType) : base(serviceType, implemtType, DependencyType.Singleton)
        {
        }
    }
}
