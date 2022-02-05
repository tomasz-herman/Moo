using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Util
{
    public class CircularList<T>: IEnumerable<T>
        where T : class
    {
        private class Node
        {
            public T Val;
            public Node Prev;
            public Node Next;
        }

        private Node Curr;

        public void Add(T val)
        {

            var node = new Node() { Val = val };
            if (Curr == null)
            {
                Curr = node;
                Curr.Prev = Curr;
                Curr.Next = Curr;
                return;
            }

            node.Prev = Curr.Prev;
            node.Next = Curr;
            Curr.Prev.Next = node;
            Curr.Prev = node;
        }
        public T Current() => Curr.Val;
        public T Next()
        {
            Curr = Curr.Next;
            return Curr.Val;
        }
        public T Prev()
        {
            Curr = Curr.Prev;
            return Curr.Val;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var cur = Curr;
            var ret = new List<T>();
            do
            {
                ret.Add(cur.Val);
                cur = cur.Next;
            }
            while (cur != Curr);
            return ret.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
