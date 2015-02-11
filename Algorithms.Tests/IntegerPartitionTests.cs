using Algorithms.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Tests
{

    [TestClass]
    public class IntegerPartitionTests
    {
        
        [TestMethod]
        public void Simple()
        {
            var result = IntegerPartition.GetPartition(4);

            Assert.AreEqual(5, result.Count);

            Assert.AreEqual(4, result[0].Count);
            Assert.AreEqual(1, result[0][0]);
            Assert.AreEqual(1, result[0][1]);
            Assert.AreEqual(1, result[0][2]);
            Assert.AreEqual(1, result[0][3]);

            Assert.AreEqual(3, result[1].Count);
            Assert.AreEqual(2, result[1][0]);
            Assert.AreEqual(1, result[1][1]);
            Assert.AreEqual(1, result[1][2]);

            Assert.AreEqual(2, result[2].Count);
            Assert.AreEqual(2, result[2][0]);
            Assert.AreEqual(2, result[2][1]);

            Assert.AreEqual(2, result[3].Count);
            Assert.AreEqual(3, result[3][0]);
            Assert.AreEqual(1, result[3][1]);

            Assert.AreEqual(1, result[4].Count);
            Assert.AreEqual(4, result[3][0]);
        }

    }

}
