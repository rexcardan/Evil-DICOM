using EvilDICOM.Core.Element;
using EvilDICOM.Core.Wrapping;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace EvilDICOM.Core.Wrapping
{
    public class SequenceItemWrapper<T> : IList<T> where T : DICOMObjectWrapper, new ()
    {
        private DICOMData<DICOMObject> _seqItems;
        List<T> internalList;


        public SequenceItemWrapper(DICOMData<DICOMObject> seqItems)
        {
            _seqItems = seqItems;
            internalList = new List<T>();

            foreach (var item in _seqItems.MultipicityValue)
            {
                internalList.Add(new T() { DCM = item });
            }
        }


        public T this[int index]
        {
            get => internalList[index];
            set
            {
                internalList[index] = value;
                _seqItems.MultipicityValue[index] = value.DCM;
            }
        }

        public int Count => internalList.Count;

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            internalList.Add(item);
            _seqItems.MultipicityValue.Add(item.DCM);
        }

        public void Clear()
        {
            internalList.Clear();
            _seqItems.MultipicityValue.Clear();
        }

        public bool Contains(T item)
        {
            return internalList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            internalList.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return internalList.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return internalList.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            internalList.Insert(index, item);
            _seqItems.MultipicityValue.Insert(index, item.DCM);
        }

        public bool Remove(T item)
        {
            internalList.Remove(item);
            return _seqItems.MultipicityValue.Remove(item.DCM);
        }

        public void RemoveAt(int index)
        {
            internalList.RemoveAt(index);
            _seqItems.MultipicityValue.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return internalList.GetEnumerator();
        }
    }
}
