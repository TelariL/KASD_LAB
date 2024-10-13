using MyVector;

namespace MyStack
{
    public class MyStack<T> : MyVector<T>
    {
        private int elementCount;
        private int capacityIncrement;
        private MyVector<T> stack;
        public MyStack()
        {
            stack = new MyVector<T>(10);
        }
        public void Push(T item)
        {
            stack.Add(item);
        }
        public void Pop()
        {
            if (elementCount == 0) throw new ArgumentOutOfRangeException("Stack is empty");
            stack.Remove(elementCount - 1);
        }
        public T Peek()
        {
            if(elementCount == 0)throw new ArgumentOutOfRangeException("Stack is empty");
            return stack.Get(elementCount-1);
        }
        public bool Empty()
        {
            if(elementCount==0) return true;
            return false;
        }
        public int Search(T item)
        {
            if(stack.IndexOF(item) == -1) return -1; 
            return elementCount - stack.IndexOF(item);
        }
    }
}
