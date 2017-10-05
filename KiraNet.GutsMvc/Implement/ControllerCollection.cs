using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace KiraNet.GutsMVC.Implement
{
    /// <summary>
    /// 用于缓存Controller类型，免得每次都要去查找Controller类型
    /// </summary>
    public class ControllerCollection : IList<TypeInfo>
    {
        private IList<TypeInfo> _controllers = new List<TypeInfo>();
        public TypeInfo this[int index] { get => _controllers[index]; set => _controllers[index] = value; }

        public int Count => _controllers.Count;

        public bool IsReadOnly => _controllers.IsReadOnly;

        public void Add(TypeInfo item)
        {
            _controllers.Add(item);
        }

        public void Clear()
        {
            _controllers.Clear();
        }

        public bool Contains(TypeInfo item)
        {
            return _controllers.Contains(item);
        }

        public void CopyTo(TypeInfo[] array, int arrayIndex)
        {
            _controllers.CopyTo(array, arrayIndex);
        }

        public IEnumerator<TypeInfo> GetEnumerator()
        {
            return _controllers.GetEnumerator();
        }

        public int IndexOf(TypeInfo item)
        {
            return _controllers.IndexOf(item);
        }

        public void Insert(int index, TypeInfo item)
        {
            _controllers.Insert(index, item);
        }

        public bool Remove(TypeInfo item)
        {
            return _controllers.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _controllers.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _controllers.GetEnumerator();
        }
    }
}
