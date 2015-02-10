using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Implementation
{
    public class MinStack
    {
        private Stack<int> stack = new Stack<int>();
        private Stack<int> minStack = new Stack<int>();

        public void Push(int value)
        {
            stack.Push(value);
            if (minStack.Count == 0 || value <= minStack.Peek())
            {
                minStack.Push(value);
            }
        }

        public int Pop()
        {
            if (stack.Count == 0)
            {
                throw new InvalidOperationException();
            }

            int value = stack.Pop();
            if (value == minStack.Peek())
            {
                minStack.Pop();
            }
            return value;
        }

        public int GetMin()
        {
            if (minStack.Count == 0)
            {
                throw new InvalidOperationException();
            }
            return minStack.Peek();
        }

    }
}
