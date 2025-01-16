using System.Diagnostics;

namespace CustomUnityLibrary
{
    public class ObjectPool<T>
    {
        private Stack<T> availableObjs;
        private HashSet<T> usedObjs;

        private int count;
        public int Count { get => count; }

        public ObjectPool()
        {
            availableObjs = new Stack<T>();
            usedObjs = new HashSet<T>();

            count = 0;
        }

        public ObjectPool(T[] args)
        {
            availableObjs = new Stack<T>();
            usedObjs = new HashSet<T>();

            count = 0;
            foreach (T obj in args)
            {
                availableObjs.Push(obj);
                count++;
            }
        }

        public void Add(T obj)
        {
            availableObjs.Push(obj);
            count++;
        }

        public void Remove()
        {
            availableObjs.Pop();
            count--;
        }

        public T UseObject()
        {

            try
            {
                T obj = availableObjs.Pop();
                usedObjs.Add(obj);

                return obj;
            }
            catch
            {
                return default(T);
            }
        }

        public void RecycleObject(T obj)
        {
            try
            {
                T temp;
                usedObjs.TryGetValue(obj, out temp);
                availableObjs.Push(temp);
                usedObjs.Remove(obj);
            }
            catch
            {
                Debug.Fail("Object to recycle was not found!");
            }
        }
    }

}
