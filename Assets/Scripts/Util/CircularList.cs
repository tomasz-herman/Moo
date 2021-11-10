using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Util
{
    public class CircularList<T>
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
            if (Curr == null)
            {
                Curr = new Node() { Val = val };
                Curr.Prev = Curr;
                Curr.Next = Curr;
            }
            else
            {
                var node = new Node() { Val = val };
                node.Prev = Curr.Prev;
                node.Next = Curr;
                Curr.Prev.Next = node;
                Curr.Prev = node;
                
            }
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
    }
}
