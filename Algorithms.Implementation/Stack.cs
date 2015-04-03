using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Implementation
{
    public class Stack
    {

        #region Min Stack - 2 stacks

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

        #endregion

        #region Min Stack - Constant Space

        public class MinStackConstantSpace
        {

            private Stack<int> stack = new Stack<int>();
            private int min = int.MaxValue;

            public void Push(int value)
            {
                stack.Push(value - min);
                if (value < min)
                {
                    min = value;
                }
            }

            public int Pop()
            {
                if (stack.Count == 0)
                {
                    throw new InvalidOperationException();
                }

                int currentMin = min;
                int diff = stack.Pop();

                if (diff < 0)
                {
                    min = currentMin - diff;
                }

                return diff + min;
            }

            public int GetMin()
            {
                if (stack.Count == 0)
                {
                    throw new InvalidOperationException();
                }
                return min;
            }

        }

        #endregion

        #region Balanced Parentheses

        /// <summary>
        /// Check is parentheses are balanced
        /// 
        /// ((())) is balanced
        /// )( is not balanced
        /// </summary>
        /// <param name="parantheses"></param>
        /// <returns></returns>
        public static bool BalancedParentheses(string parantheses)
        {
            Stack<char> tracker = new Stack<char>();

            foreach (char character in parantheses)
            {
                if (character == '(')
                {
                    tracker.Push(character);
                }
                else
                {
                    if (tracker.Count == 0)
                    {
                        return false;
                    }

                    char existingCharacter = tracker.Pop();

                    if (existingCharacter != '(')
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Check is parentheses are balanced
        /// 
        /// Support (, {, [
        /// </summary>
        /// <param name="parantheses"></param>
        /// <returns></returns>
        public static bool BalancedParenthesesMultiple(string parantheses)
        {
            Dictionary<char, char> bracketMatch = new Dictionary<char, char> {
                {')', '('},
                {']', '['},
                {'}', '{'},
            };

            Stack<char> tracker = new Stack<char>();

            foreach (char character in parantheses)
            {
                if ((character == '(') || (character == '[') || (character == '{'))
                {
                    tracker.Push(character);
                }
                else
                {
                    if (tracker.Count == 0)
                    {
                        return false;
                    }

                    char existingCharacter = tracker.Pop();

                    if (existingCharacter != bracketMatch[character])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        #endregion

    }
}
