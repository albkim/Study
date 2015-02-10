using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms.Implementation;

namespace Algorithms.Tests
{
    [TestClass]
    public class MinStackTests
    {
        
        [TestMethod]
        public void PushAndMin()
        {
            MinStack stack = new MinStack();
            stack.Push(10);
            Assert.AreEqual(10, stack.GetMin());
        }

        [TestMethod]
        public void MultiplePushAndMin()
        {
            MinStack stack = new MinStack();
            stack.Push(10);
            stack.Push(9);
            stack.Push(8);
            Assert.AreEqual(8, stack.GetMin());
        }

        [TestMethod]
        public void PopAndMin()
        {
            MinStack stack = new MinStack();
            stack.Push(10);
            stack.Push(9);
            stack.Push(8);
            Assert.AreEqual(8, stack.Pop());
            Assert.AreEqual(9, stack.GetMin());
        }

        [TestMethod]
        public void PopAndMinAfterPop()
        {
            MinStack stack = new MinStack();
            stack.Push(10);
            stack.Push(9);
            stack.Push(8);
            stack.Pop();
            stack.Push(11);
            Assert.AreEqual(9, stack.GetMin());
            Assert.AreEqual(11, stack.Pop());
        }

    }
}
