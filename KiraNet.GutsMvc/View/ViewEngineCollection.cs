//using KiraNet.GutsMvc.Implement;
//using System.Collections;
//using System.Collections.Generic;

//namespace KiraNet.GutsMvc.View
//{
//    public class ViewEngineCollection : IViewEngine, ICollection<IViewEngine>
//    {
//        private List<IViewEngine> _engines = new List<IViewEngine>();

//        public IView FindView(ControllerContext controllerContext, string folderName, string viewName)
//        {
//            foreach (var engine in _engines)
//            {
//                var view = engine.FindView(controllerContext, folderName, viewName);
//                if (view != null)
//                {
//                    return view;
//                }
//            }

//            return null;
//        }


//        public int Count => ((ICollection<IViewEngine>)_engines).Count;

//        public bool IsReadOnly => ((ICollection<IViewEngine>)_engines).IsReadOnly;

//        public void Add(IViewEngine item)
//        {
//            ((ICollection<IViewEngine>)_engines).Add(item);
//        }

//        public void Clear()
//        {
//            ((ICollection<IViewEngine>)_engines).Clear();
//        }

//        public bool Contains(IViewEngine item)
//        {
//            return ((ICollection<IViewEngine>)_engines).Contains(item);
//        }

//        public void CopyTo(IViewEngine[] array, int arrayIndex)
//        {
//            ((ICollection<IViewEngine>)_engines).CopyTo(array, arrayIndex);
//        }

//        public IEnumerator<IViewEngine> GetEnumerator()
//        {
//            return ((ICollection<IViewEngine>)_engines).GetEnumerator();
//        }

//        public bool Remove(IViewEngine item)
//        {
//            return ((ICollection<IViewEngine>)_engines).Remove(item);
//        }

//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return ((ICollection<IViewEngine>)_engines).GetEnumerator();
//        }
//    }
//}
