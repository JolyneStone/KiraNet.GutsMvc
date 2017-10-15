using System.Collections;
using System.Collections.Generic;

namespace KiraNet.GutsMvc.ModelValid
{
    public class ValidFailureCollection : ICollection<ValidFailure>
    {
        private List<ValidFailure> _validResultList = new List<ValidFailure>();

        public int Count => ((ICollection<ValidFailure>)_validResultList).Count;

        public bool IsReadOnly => ((ICollection<ValidFailure>)_validResultList).IsReadOnly;

        public void Add(ValidFailure item)
        {
            ((ICollection<ValidFailure>)_validResultList).Add(item);
        }

        public void Clear()
        {
            ((ICollection<ValidFailure>)_validResultList).Clear();
        }

        public bool Contains(ValidFailure item)
        {
            return ((ICollection<ValidFailure>)_validResultList).Contains(item);
        }

        public void CopyTo(ValidFailure[] array, int arrayIndex)
        {
            ((ICollection<ValidFailure>)_validResultList).CopyTo(array, arrayIndex);
        }

        public IEnumerator<ValidFailure> GetEnumerator()
        {
            return ((ICollection<ValidFailure>)_validResultList).GetEnumerator();
        }

        public bool Remove(ValidFailure item)
        {
            return ((ICollection<ValidFailure>)_validResultList).Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((ICollection<ValidFailure>)_validResultList).GetEnumerator();
        }
    }
}
