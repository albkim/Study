using Algorithms.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Tests
{
    
    [TestClass]
    public class DeepIteratorTests
    {

        [TestMethod]
        public void Empty() {
            DeepIterator<int> iterator = new DeepIterator<int>(new int[] { });

            Assert.IsFalse(iterator.MoveNext());
        }

        [TestMethod]
        public void Works()
        {
            DeepIterator<int> iterator = new DeepIterator<int>(new object[] { 0, new object[] { 1, 2 }, 3, new object[] { 4, new object[] { 5, 6 } } });

            Assert.AreEqual(0, iterator.Current);
            
            Assert.IsTrue(iterator.MoveNext());
            Assert.AreEqual(0, iterator.Current);

            Assert.IsTrue(iterator.MoveNext());
            Assert.AreEqual(1, iterator.Current);

            Assert.IsTrue(iterator.MoveNext());
            Assert.AreEqual(2, iterator.Current);

            Assert.IsTrue(iterator.MoveNext());
            Assert.AreEqual(3, iterator.Current);

            Assert.IsTrue(iterator.MoveNext());
            Assert.AreEqual(4, iterator.Current);

            Assert.IsTrue(iterator.MoveNext());
            Assert.AreEqual(5, iterator.Current);

            Assert.IsTrue(iterator.MoveNext());
            Assert.AreEqual(6, iterator.Current);
        }

    }

}
