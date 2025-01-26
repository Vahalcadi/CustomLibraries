namespace CustomUnityLibrary
{
    public class ObjectPool<T>
    {
        private Stack<T> availableObjs;
        private HashSet<T> usedObjs;

        private int totalObjsCount;
        private int usedObjsCount;

        public int TotalObjsCount { get => totalObjsCount; }
        public int UsedObjsCount { get => usedObjsCount; }

        public ObjectPool()
        {
            availableObjs = new Stack<T>();
            usedObjs = new HashSet<T>();

            totalObjsCount = 0;
        }

        public ObjectPool(T[] args)
        {
            availableObjs = new Stack<T>();
            usedObjs = new HashSet<T>();

            totalObjsCount = 0;
            foreach (T obj in args)
            {
                availableObjs.Push(obj);
                totalObjsCount++;
            }
        }

        public void Add(T obj)
        {
            availableObjs.Push(obj);
            totalObjsCount++;
        }

        public void Remove()
        {
            availableObjs.Pop();
            totalObjsCount--;
        }

        public T UseObject()
        {

            try
            {
                T obj = availableObjs.Pop();
                usedObjs.Add(obj);
                usedObjsCount = usedObjs.Count;
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
                usedObjsCount = usedObjs.Count;
            }
            catch
            {

            }
        }
    }

}
