using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Util
{
    public class CircularList<T>: IEnumerable<T>
        where T : class
    {
        private readonly List<T> list = new List<T> {null};
        private int current;

        public void Add(T val)
        {
            if(val == null) return;

            if (Current() == null)
            {
                list[0] = val;
                return;
            }

            list.Add(val);
        }

        public void Sort(Comparison<T> comparer)
        {
            list.Sort(comparer);
        }

        public void SetIndex(int i)
        {
            current = i;
        }
        public T Current() => list[current];
        public T Next()
        {
            current++;
            if (current >= list.Count) current = 0;
            return Current();
        }
        public T Prev()
        {
            current--;
            if (current < 0) current = list.Count - 1;
            return Current();
        }

        public IEnumerator<T> GetEnumerator()
        {
            var first = Current();
            yield return first;
            while (Next() != first)
            {
                yield return Current();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
