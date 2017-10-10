//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Text;

//namespace KiraNet.GutsMvc.ModelBinder
//{
//    public class ModelBinderProviderCollection : Collection<IModelBinderProvider>
//    {
//        private IModelBinderProvider[] _combinedItems;
//        public ModelBinderProviderCollection()
//        {
//        }

//        public ModelBinderProviderCollection(IList<IModelBinderProvider> list)
//            : base(list)
//        {
//        }

//        internal IModelBinderProvider[] CombinedItems
//        {
//            get
//            {
//                IModelBinderProvider[] combinedItems = _combinedItems;
//                if (combinedItems == null)
//                {
//                    combinedItems = MultiServiceResolver.GetCombined<IModelBinderProvider>(Items, _dependencyResolver);
//                    _combinedItems = combinedItems;
//                }
//                return combinedItems;
//            }
//        }

//        protected override void ClearItems()
//        {
//            _combinedItems = null;
//            base.ClearItems();
//        }

//        protected override void InsertItem(int index, IModelBinderProvider item)
//        {
//            if (item == null)
//            {
//                throw new ArgumentNullException("item");
//            }
//            _combinedItems = null;
//            base.InsertItem(index, item);
//        }

//        protected override void RemoveItem(int index)
//        {
//            _combinedItems = null;
//            base.RemoveItem(index);
//        }

//        protected override void SetItem(int index, IModelBinderProvider item)
//        {
//            if (item == null)
//            {
//                throw new ArgumentNullException("item");
//            }
//            _combinedItems = null;
//            base.SetItem(index, item);
//        }

//        public IModelBinder GetBinder(Type modelType)
//        {
//            if (modelType == null)
//            {
//                throw new ArgumentNullException("modelType");
//            }

//            IModelBinderProvider[] providers = CombinedItems;
//            for (int i = 0; i < providers.Length; i++)
//            {
//                IModelBinder binder = providers[i].GetBinder(modelType);
//                if (binder != null)
//                {
//                    return binder;
//                }
//            }
//            return null;
//        }
//    }
//}
