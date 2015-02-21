using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Implementation
{
    
    public class DeepIterator<T>
    {

        private Stack<IEnumerator> stack = new Stack<IEnumerator>();
        private T current;

        public DeepIterator(IEnumerable enumerable) {
            stack.Push(enumerable.GetEnumerator());
        }

        public T Current
        {
            get
            {
                return this.current;
            }
        }

        public bool MoveNext()
        {
            while (this.stack.Count > 0)
            {
                if (this.stack.Peek().MoveNext())
                {
                    //if we run into another iterator we need to keep iterating
                    while (this.stack.Peek().Current is IEnumerable)
                    {
                        //need to put this on top of the current one;
                        IEnumerable enumerable = (IEnumerable)this.stack.Peek().Current;

                        //need to advance to the first element since current is null until first MoveNext
                        IEnumerator enumerator = enumerable.GetEnumerator();
                        enumerator.MoveNext();

                        this.stack.Push(enumerator);
                    }

                    //now current should be type T
                    this.current = (T)this.stack.Peek().Current;

                    return true;
                }
                else
                {
                    this.stack.Pop();
                }
            }

            return false;
        }

    }
}
