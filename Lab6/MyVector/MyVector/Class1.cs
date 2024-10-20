using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace MyVector
{
    public class MyVector<T>
    {
        protected T[] elementData;
        protected int elementCount;
        protected int capacityIncrement;

        public MyVector(int initialCapacity, int InitialCapacityIncrement)
        {
            elementData = new T[initialCapacity];
            capacityIncrement = InitialCapacityIncrement;
            elementCount = 0;
        }
        public MyVector(int initialCapacity)
        {
            elementData = new T[initialCapacity];
            elementCount = 0;
            capacityIncrement = 0;
        }
        public MyVector()
        {
            elementData = new T[10];
            elementCount = 0;
            capacityIncrement = 0;
        }
        public MyVector(T[] a)
        {
            elementData = new T[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                elementData[i] = a[i];
            }
            elementCount = a.Length;
            capacityIncrement = 0;
        }
        public void Add(T e)
        {
            if (elementCount == elementData.Length)
            {
                T[] newElementData;
                if (capacityIncrement == 0)
                {
                    newElementData = new T[elementData.Length * 2];
                }
                else newElementData = new T[elementData.Length + capacityIncrement];
                for (int i = 0; i < elementCount; i++)
                    newElementData[i] = elementData[i];
                elementData = newElementData;
            }
            elementData[elementCount] = e;
            elementCount++;
        }
        public void AddAll(T[] a)
        {
            for (int i = 0; i < a.Length; i++)
                Add(a[i]);
        }
        public void Clear()
        {
            elementCount = 0;
        }
        public bool Contains(T e)
        {
            for (int i = 0; i < elementCount; i++)
                if (Equals(elementData[i], e)) return true;
            return false;
        }
        public bool ContainsAll(T[] a)
        {
            for (int i = 0; i < a.Length; i++)
                if (!Contains(a[i])) return false;
            return true;
        }
        public bool IsEmpty()
        {
            return elementCount == 0;
        }
        public void Remove(T e)
        {
            for (int i = 0; i < elementCount; i++)
                if (Equals(elementData[i], e))
                {
                    for (int j = i; j < elementCount - 1; j++)
                    {
                        elementData[i] = elementData[j + 1];
                    }
                    elementCount--;
                }
        }
        public void RemoveAll(T[] a)
        {
            for (int i = 0; i < a.Length; i++)
                Remove(a[i]);
        }
        public void RetainAll(T[] a)
        {
            int newElementCount = 0;
            T[] newElementData = new T[elementCount];
            for (int i = 0; i < a.Length; i++)
                for (int j = 0; j < elementCount; j++)
                    if (Equals(elementData[j], a[i]))
                    {
                        newElementData[newElementCount] = elementData[j];
                        newElementCount++;
                    }
            elementData = newElementData;
            elementCount = newElementCount;
        }
        public int Size()
        {
            return elementCount;
        }
        public T[] ToArray()
        {
            T[] array = new T[elementCount];
            for (int i = 0; i < elementCount; i++) array[i] = elementData[i];
            return array;
        }
        public void ToArray(ref T[] a)
        {
            if (a == null) a = ToArray();
            Array.Copy(elementData, a, elementCount);
        }
        public void Add(int index, T e)
        {
            if (index > elementCount) { Add(e); return; }
            T[] NewElementData= new T[elementData.Length];
            if (elementCount == elementData.Length) 
            { 
                if(capacityIncrement==0) NewElementData = new T[elementCount *2]; 
                else NewElementData = new T[elementCount + capacityIncrement];
            }
            for (int i = 0; i < index; i++)
            {
                NewElementData[i] = elementData[i];
            }
            NewElementData[index] = e;
            for (int i = index + 1; i < elementCount; i++)
            {
                NewElementData[i] = elementData[i - 1];
            }
            elementData = NewElementData;
            elementCount++;
        }
        public void AddAll(int index, T[] a)
        {
            if (index > elementCount) { AddAll(a); return; }
            T[] newElementData = new T[elementData.Length];
            while (newElementData.Length - a.Length < elementCount) 
            {
                if (capacityIncrement == 0) newElementData = new T[newElementData.Length * 2];
                else newElementData = new T[newElementData.Length + capacityIncrement];
            }
            for (int i = 0; i < index; i++)
            {
                newElementData[i] = elementData[i];
            }
            for (int i = 0; i < a.Length; i++)
            {
                newElementData[i + index] = a[i];
            }
            for (int i = index; i < elementCount; i++)
            {
                newElementData[i + index] = elementData[i];
            }
            elementData = newElementData;
            elementCount += a.Length;
        }
        public T Get(int index)
        {
            return elementData[index];
        }
        public int IndexOF(T e)
        {
            for (int i = 0; i < elementCount; i++)
                if (Equals(elementData[i], e)) return i;
            return -1;
        }
        public int LastIndexOf(object o)
        {
            for (int i = elementCount - 1; i >= 0; i--)
                if (Equals(elementData[i], o)) return i;
            return -1;
        }
        public T Remove(int index)
        {
            if (index < 0 || index > elementCount)
                throw new ArgumentOutOfRangeException("index");
            Remove(elementData[index]);
            return elementData[index];
        }
        public void Set(int index, T e)
        {
            if (index < 0 || index > elementCount)
                throw new ArgumentOutOfRangeException("index");
            elementData[index] = e;
        }
        public T[] SubList(int fromindex, int toindex)
        {
            if ((fromindex < 0 || fromindex > elementCount) || (toindex < 0 || toindex > elementCount))
                throw new ArgumentOutOfRangeException("fromindex", "toindex");
            T[] Result = new T[toindex - fromindex];
            for (int i = fromindex; i < toindex; i++)
                Result[i - fromindex] = elementData[i];
            return Result;
        }
        public T FirstElement() { return elementData[0]; }
        public T LastElement() { return elementData[elementCount-1]; }
        public void RemoveElementAt(int pos)
        {
            if (pos < 0 || pos > elementCount)
                throw new ArgumentOutOfRangeException("index");
            Remove(elementData[pos]);
        }
        public void RemoveRange(int begin, int end)
        {
            if ((begin < 0 || begin > elementCount) || (end < 0 || end > elementCount))
                throw new ArgumentOutOfRangeException("begin", "end");
            T[] newArray = new T[end - begin +1];
            int index = 0;
            for(int i = begin; i < end; i++)
            {
                newArray[index] = elementData[i];
                index++;
            }
            RemoveAll(newArray);
        }
    }
}
