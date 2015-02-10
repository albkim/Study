using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Implementation
{
    
    public class MinStackConstantSpace {
        
        private Stack<int> stack = new Stack<int>();
        private int min = int.MaxValue;
        
        public void Push(int value) {
            stack.Push(value - min);
            if (value < min) {
                min = value;
            }
        }
        
        public int Pop() {
            if (stack.Count == 0) {
                throw new InvalidOperationException();
            }
            
            int currentMin = min;
            int diff = stack.Pop();
            
            if (diff < 0) {
                min = currentMin - diff;
            }
            
            return diff + min;
        }
        
        public int GetMin() {
            if (stack.Count == 0) {
                throw new InvalidOperationException();
            }
            return min;
        }
        
    }
}
