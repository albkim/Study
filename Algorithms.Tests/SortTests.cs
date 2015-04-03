using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms.Implementation;
using System.Linq;
using System.Collections.Generic;

namespace Algorithms.Tests
{
    [TestClass]
    public class SortTests
    {

        #region QuickSort

        [TestMethod]
        public void QuickSortSimple()
        {
            int[] array = new int[] { 3, 2, 56, 7, 7, 4, 2, 2 };
            Sort.QuickSort(array);

            Assert.AreEqual(2, array[0]);
            Assert.AreEqual(2, array[1]);
            Assert.AreEqual(2, array[2]);
            Assert.AreEqual(3, array[3]);
            Assert.AreEqual(4, array[4]);
            Assert.AreEqual(7, array[5]);
            Assert.AreEqual(7, array[6]);
            Assert.AreEqual(56, array[7]);
        }

        [TestMethod]
        public void ThreeWayPartition()
        {
            int[] array = new int[] { 0, 1, 2, 1, 1, 0, 0, 2, 2 };
            Sort.ThreeWayPartition(array, 1);

            Assert.AreEqual(0, array[0]);
            Assert.AreEqual(0, array[1]);
            Assert.AreEqual(0, array[2]);
            Assert.AreEqual(1, array[3]);
            Assert.AreEqual(1, array[4]);
            Assert.AreEqual(1, array[5]);
            Assert.AreEqual(2, array[6]);
            Assert.AreEqual(2, array[7]);
            Assert.AreEqual(2, array[8]);
        }
        
        #endregion

    }
}
