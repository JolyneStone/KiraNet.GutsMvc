//using System.Collections;
//using System.Collections.Concurrent;
//using System.Collections.Generic;

//namespace KiraNet.GutsMVC.View
//{
//    internal sealed class ViewCacheCollection:IEnumerable<KeyValuePair<ViewCacheKey, IView>>
//    {
//        private readonly static ConcurrentDictionary<ViewCacheKey, IView> _viewCaches = new ConcurrentDictionary<ViewCacheKey, IView>();

//        public void AddView(ViewCacheKey viewKey, IView view)
//        {
//            if(viewKey == null || view == null)
//            {
//                return;
//            }

//            _viewCaches.AddOrUpdate(viewKey, view, (k, v) => v);
//        }

//        public IEnumerator<KeyValuePair<ViewCacheKey, IView>> GetEnumerator()
//        {
//            return ((IEnumerable<KeyValuePair<ViewCacheKey, IView>>)_viewCaches).GetEnumerator();
//        }

//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return ((IEnumerable<KeyValuePair<ViewCacheKey, IView>>)_viewCaches).GetEnumerator();
//        }
//    }
//}
