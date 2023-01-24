using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApplication
{
    public class LimitedSizeStack<T>
    {
        LinkedList<T> list = new LinkedList<T>();
        int limit;

        public LimitedSizeStack(int limit)
        {
            this.limit = limit;
        }

        public void Push(T item)
        {
            list.AddLast(item);
            if (list.Count > limit)
            {
                list.RemoveFirst();
            }
        }

        public T Pop()
        {
            var lastVal = list.Last.Value;
            list.RemoveLast();
            return lastVal;
        }

        public int Count
        {
            get
            {
                return list.Count;
            }
        }
    }
}

