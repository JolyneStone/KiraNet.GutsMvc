using System;
using System.Collections.Generic;
using System.Dynamic;

namespace KiraNet.GutsMVC
{
    public class DynamicViewBag : DynamicObject
    {
        private readonly ViewDataDictionary _viewData;

        public DynamicViewBag(ViewDataDictionary viewData)
        {
            _viewData = viewData ?? throw new ArgumentNullException(nameof(viewData));
        }

        private ViewDataDictionary ViewData => _viewData;
        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return ViewData.Keys;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (binder == null)
            {
                throw new ArgumentNullException(nameof(binder));
            }

            result = ViewData[binder.Name];

            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (binder == null)
            {
                throw new ArgumentNullException(nameof(binder));
            }

            ViewData[binder.Name] = value;

            return true;
        }
    }
}
