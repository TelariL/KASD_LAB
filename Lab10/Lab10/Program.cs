using MyArrayList_Class;

namespace Lab10
{
    public class Heap<T> where T : IComparable<T>
    {
        private int size;
        private MyArrayList<T> heap = new MyArrayList<T>(10);

        public Heap(T[] array)
        {
            size = array.Length;
            for (int i = 0; i < size; i++)
            {
                heap.Add(array[i]);
            }

            for (int i = (size / 2) - 1; i >= 0; i--)
            {
                Heapify(i);
            }
        }

        public void Heapify(int index)
        {
            int left = 2 * index + 1;
            int right = 2 * index + 2;
            int parent = index;

            if (left < size && heap.Get(left).CompareTo(heap.Get(parent)) > 0)
            {
                parent = left;
            }

            if (right < size && heap.Get(right).CompareTo(heap.Get(parent)) > 0)
            {
                parent = right;
            }

            if (parent != index)
            {
                Swap(index, parent);
                Heapify(parent);
            }
        }

        public void Swap(int index1, int index2)
        {
            T temp1 = heap.Get(index1);
            T temp2 = heap.Get(index2);
            heap.Set(index2, temp1);
            heap.Set(index1, temp2);
        }

        public T Search()
        {
            return heap.Get(0);
        }

        public T Extract()
        {
            T exElement = Search();
            heap.Remove(0);
            size--;
            Heapify(0);
            return exElement;
        }

        public void IncreaseKey(int index, T newKey)
        {
            if (index < 0 || index >= size)
            {
                throw new IndexOutOfRangeException("Index");
            }

            heap.Set(index, newKey);
            for (int i = size / 2; i >= 0; i--)
            {
                Heapify(i);
            }
        }

        public void AddElement(T element)
        {
            heap.Add(element);
            size++;

            for (int i = size / 2; i >= 0; i--)
            {
                Heapify(i);
            }
        }

        public void MergeHeaps(Heap<T> newHeap)
        {
            while (newHeap.size > 0)
            {
                T element = newHeap.Search();
                AddElement(element);
            }

            for (int i = size / 2; i >= 0; i--)
            {
                Heapify(i);
            }
        }

        public void Print()
        {
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine(heap.Get(i));
            }
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int[] array1 = { 100, 19, 36, 17, 3, 25, 1, 2, 7 };
            int[] array2 = { 10, 14, 11, 18, 20 };
            Heap<int> heap1 = new Heap<int>(array1);
            Heap<int> heap2 = new Heap<int>(array2);
            heap1.Print();
            heap2.Print();
            Console.WriteLine($"{heap1.Search()}");
            Console.WriteLine($"\n{heap2.Extract()}\n");
            heap2.Print();
            heap1.AddElement(52);
            heap1.Print();
        }
    }
}