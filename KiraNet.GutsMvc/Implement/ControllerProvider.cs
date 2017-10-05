using KiraNet.GutsMVC.Helper;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace KiraNet.GutsMVC.Implement
{
    public class ControllerProvider : IControllerProvider
    {
        //private const string ControllerTypeNameSuffix = "Controller";

        //private static IAssemblyLocator _locator;
        private static Assembly[] _assemblies;
        private static KiraSpinLock _spinLock = new KiraSpinLock();
        private ControllerCollection _collection = new ControllerCollection();
        public ControllerProvider()
        {
            //if (_assemblies == null || _assemblies.Length == 0)
            //{
            //    _spinLock.Enter();
            //    if (_assemblies == null || _assemblies.Length == 0)
            //    {
            //        var locator = new AssemblyLocator();
            //        _assemblies = locator.GetAssemblies();
            //        foreach(var assemblies in _assemblies)
            //        {
            //            Populate(assemblies.DefinedTypes, _collection);
            //        }
            //    }
            //    _spinLock.Exit();
            //}
        }

        private void InitAssemblies()
        {
            if (_assemblies == null || _assemblies.Length == 0)
            {
                var locator = new AssemblyLocator();
                _assemblies = locator.GetAssemblies();
            }
        }

        private void InitControllerCollection()
        {
            _spinLock.Enter();

            InitAssemblies();
            foreach (var assemblies in _assemblies)
            {
                Populate(assemblies.ExportedTypes.Select(x => x.GetTypeInfo()), _collection);
            }

            _spinLock.Exit();
        }

        public ControllerCollection GetControllerCollection()
        {
            if (_collection == null || _collection.Count == 0)
                InitControllerCollection();

            return _collection;
        }


        public void Populate(IEnumerable<TypeInfo> collection, ControllerCollection controllers)
        {
            foreach (var typeInfo in collection)
            {
                if (IsController(typeInfo) &&
                    !controllers.Contains(typeInfo))
                {
                    controllers.Add(typeInfo);
                }
            }
        }

        protected virtual bool IsController(TypeInfo typeInfo)
        {
            if (!typeInfo.IsClass)
            {
                return false;
            }

            if (typeInfo.IsAbstract)
            {
                return false;
            }

            if (!typeInfo.IsPublic)
            {
                return false;
            }

            if (typeInfo.ContainsGenericParameters)
            {
                return false;
            }

            if (typeInfo.IsDefined(typeof(NonControllerAttribute)))
            {
                return false;
            }

            if (!typeof(Controller).GetTypeInfo().IsAssignableFrom(typeInfo)
                || typeInfo.IsDefined(typeof(NonControllerAttribute), true))
            {
                return false;
            }

            //if (!typeInfo.Name.EndsWith(ControllerTypeNameSuffix, StringComparison.OrdinalIgnoreCase) &&
            //    !typeInfo.IsDefined(typeof(ControllerAttribute)))
            //{
            //    return false;
            //}

            return true;
        }
    }
}
