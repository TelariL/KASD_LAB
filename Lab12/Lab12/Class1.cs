using MyArrayList_Class;
namespace Lab11
{
    public class MyPriorityQueue<T> where T : IComparable<T>
    {
        private MyArrayList<T> queue;
        private int size;
        private Comparer<T> comparator;
        public MyPriorityQueue()
        {
            queue = new MyArrayList<T>(11);
            size = 0;
            comparator = Comparer<T>.Default;
        }
        public void Queuelify(int index)
        {
            int left = 2 * index + 1;
            int right = 2 * index + 2;
            int parent = index;

            if (left < size && comparator.Compare(queue.Get(left), queue.Get(parent)) > 0)
            {
                parent = left;
            }

            if (right < size && comparator.Compare(queue.Get(right), queue.Get(parent)) > 0)
            {
                parent = right;
            }

            if (parent != index)
            {
                Swap(index, parent);
                Queuelify(parent);
            }
        }
        public void Swap(int index1, int index2)
        {
            T temp1 = queue.Get(index1);
            T temp2 = queue.Get(index2);
            queue.Set(index2, temp1);
            queue.Set(index1, temp2);
        }
        public MyPriorityQueue(T[] a)
        {
            queue = new MyArrayList<T>(a.Length);
            size = a.Length;
            comparator = Comparer<T>.Default;
            for (int i = 0; i < size; i++)
            {
                queue.Add(a[i]);
            }
            for (int i = size / 2; i >= 0; i--)
            {
                Queuelify(i);
            }
        }
        public MyPriorityQueue(int initialCcapacity)
        {
            queue = new MyArrayList<T>(initialCcapacity);
            size = 0;
            comparator = Comparer<T>.Default;
        }
        public MyPriorityQueue(int initialCcapacity, Comparer<T> comparator)
        {
            queue = new MyArrayList<T>(initialCcapacity);
            size = initialCcapacity;
            this.comparator = comparator;
        }
        public MyPriorityQueue(MyPriorityQueue<T> c)
        {
            queue = new MyArrayList<T>(c.size);
            size = c.size;
            comparator = c.comparator;
            for (int i = 0; i < c.size; i++)
            {
                queue.Add(c.queue.Get(i));
            }
            size = c.size;

            for (int i = size / 2; i >= 0; i--)
            {
                Queuelify(i);
            }
        }
        public void Add(T e)
        {
            queue.Add(e);
            size++;
            for (int i = size / 2; i >= 0; i--)
            {
                Queuelify(i);
            }
        }
        public void AddAll(T[] a)
        {
            queue.AddAll(a);
            size += a.Length;
            for (int i = size / 2; i >= 0; i--)
            {
                Queuelify(i);
            }
        }
        public void Clear()
        {
            queue.Clear();
            size = 0;
        }
        public bool Contains(object o)
        {
            return queue.Contains(o);
        }
        public bool ContainsAll(T[] a)
        {
            return queue.ContainsAll(a);
        }

        public bool IsEmpty()
        {
            return queue.isEmpty();
        }
        public void Remove(object o)
        {
            queue.Remove(o);
            size--;
            for (int i = size / 2; i >= 0; i--)
            {
                Queuelify(i);
            }
        }
        public void RemoveAll(T[] a)
        {
            queue.RemoveAll(a);
            size -= a.Length;
            for (int i = size / 2; i >= 0; i--)
            {
                Queuelify(i);
            }
        }
        public void RetainAll(T[] a)
        {
            queue.RetainAll(a);
            size = a.Length;
            for (int i = size / 2; i >= 0; i--)
            {
                Queuelify(i);
            }
        }
        public int Size()
        {
            return queue.Size();
        }
        public T[] ToArray()
        {
            return queue.ToArray();
        }
        public T[] ToArray(ref T[] a)
        {
            return queue.ToArray(ref a);
        }
        public T Element()
        {
            return queue.Get(0);
        }
        public bool Offer(T obj)
        {
            queue.Add(obj);
            size++;
            for (int i = size / 2; i >= 0; i--)
            {
                Queuelify(i);
            }

            bool f = true;
            if (queue.Contains(obj)) return f;
            else return f == false;
        }
        public T Peek()
        {
            if (size == 0) throw new ArgumentOutOfRangeException();
            else return queue.Get(0);
        }
        public T Poll()
        {
            if (size == 0) throw new ArgumentOutOfRangeException();
            else
            {
                T element = queue.Get(0);
                queue.Remove(element);
                size--;
                for (int i = size / 2; i >= 0; i--)
                {
                    Queuelify(i);
                }
                return element;
            }
        }
        public void Print()
        {
            for (int i = 0; i < size; i++)
                Console.WriteLine(queue.Get(i));
            Console.WriteLine();
        }
    }
}