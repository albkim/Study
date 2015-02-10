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
            MinStackConstantSpace stackCS = new MinStackConstantSpace();
            MinStack stack = new MinStack();

            stackCS.Push(10);
            stack.Push(10);

            Assert.AreEqual(10, stackCS.GetMin());
            Assert.AreEqual(10, stack.GetMin());
        }

        [TestMethod]
        public void MultiplePushAndMin()
        {
            MinStackConstantSpace stackCS = new MinStackConstantSpace();
            MinStack stack = new MinStack();

            stackCS.Push(10);
            stackCS.Push(9);
            stackCS.Push(8);
            stack.Push(10);
            stack.Push(9);
            stack.Push(8);

            Assert.AreEqual(8, stackCS.GetMin());
            Assert.AreEqual(8, stack.GetMin());
        }

        [TestMethod]
        public void PopAndMin()
        {
            MinStackConstantSpace stackCS = new MinStackConstantSpace();
            MinStack stack = new MinStack();

            stackCS.Push(10);
            stackCS.Push(9);
            stackCS.Push(8);
            stack.Push(10);
            stack.Push(9);
            stack.Push(8);

            Assert.AreEqual(8, stackCS.Pop());
            Assert.AreEqual(9, stackCS.GetMin());
            Assert.AreEqual(8, stack.Pop());
            Assert.AreEqual(9, stack.GetMin());
        }

        [TestMethod]
        public void PopAndMinAfterPop()
        {
            MinStackConstantSpace stackCS = new MinStackConstantSpace();
            MinStack stack = new MinStack();

            stackCS.Push(10);
            stackCS.Push(9);
            stackCS.Push(8);
            stackCS.Pop();
            stackCS.Push(11);

            stack.Push(10);
            stack.Push(9);
            stack.Push(8);
            stack.Pop();
            stack.Push(11);

            Assert.AreEqual(9, stackCS.GetMin());
            Assert.AreEqual(11, stackCS.Pop());
            Assert.AreEqual(9, stack.GetMin());
            Assert.AreEqual(11, stack.Pop());
        }

        [TestMethod]
        public void Negative()
        {
            MinStackConstantSpace stackCS = new MinStackConstantSpace();
            MinStackConstantSpace stack = new MinStackConstantSpace();

            stackCS.Push(1);
            stackCS.Push(-2);
            stackCS.Push(3);
            Assert.AreEqual(-2, stackCS.GetMin());
            stackCS.Push(-3);
            Assert.AreEqual(-3, stackCS.GetMin());
            stackCS.Pop();
            Assert.AreEqual(-2, stackCS.GetMin());
            stackCS.Push(11);
            Assert.AreEqual(11, stackCS.Pop());
            Assert.AreEqual(3, stackCS.Pop());
            Assert.AreEqual(-2, stackCS.Pop());
            Assert.AreEqual(1, stackCS.Pop());

            stack.Push(1);
            stack.Push(-2);
            stack.Push(3);
            Assert.AreEqual(-2, stack.GetMin());
            stack.Push(-3);
            Assert.AreEqual(-3, stack.GetMin());
            stack.Pop();
            Assert.AreEqual(-2, stack.GetMin());
            stack.Push(11);
            Assert.AreEqual(11, stack.Pop());
            Assert.AreEqual(3, stack.Pop());
            Assert.AreEqual(-2, stack.Pop());
            Assert.AreEqual(1, stack.Pop());
        }

    }
}
