using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms.Implementation;

namespace Algorithms.Tests
{
    [TestClass]
    public class BitsTests
    {

        #region Addition

        [TestMethod]
        public void Add()
        {
            Assert.AreEqual(4, Bits.Add(1, 3));
            Assert.AreEqual(12, Bits.Add(10, 2));
        }

        #endregion

    }
}

