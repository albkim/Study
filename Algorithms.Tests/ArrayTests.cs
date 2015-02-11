using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms.Implementation;
using System.Linq;
using System.Collections.Generic;

namespace Algorithms.Tests
{
    [TestClass]
    public class ArrayTests
    {

        #region FindKthSmallest

        [TestMethod]
        public void SmallestOne()
        {
            var result = Array.FindKthSmallest(new[] {3, 5, 7, 8, 6, 34, 3}, 3);

            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void SmallestTwo()
        {
            var result = Array.FindKthSmallest(new[] { 3, 5, 7, 8, 6, 34, 3 }, 5);

            Assert.AreEqual(7, result);
        }

        #endregion

        #region FindMissingElementSum

        [TestMethod]
        public void Works()
        {
            var result = Array.FindMissingElementSum(new[] { 1, 2, 3 }, new[] { 1, 2 });

            Assert.AreEqual(3, result);
        }

        #endregion

        #region FindMissingElementXor

        [TestMethod]
        public void WorksXor()
        {
            var result = Array.FindMissingElementXor(new[] { 1, 2, 3 }, new[] { 1, 2 });

            Assert.AreEqual(3, result);
        }

        #endregion

        #region GetNumberAddingUpToSum

        [TestMethod]
        public void EmptyListWhenNumbersIsNull()
        {
            var result = Array.GetNumberAddingUpToSum(null, 0);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void EmptyListWhenNoNumbersAddUpToSum()
        {
            var result = Array.GetNumberAddingUpToSum(new[] { 1, 2, 3 }, 10);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void FindOnePair()
        {
            var result = Array.GetNumberAddingUpToSum(new[] { 1, 2, 3 }, 3);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(1, result[0][0]);
            Assert.AreEqual(2, result[0][1]);
        }

        [TestMethod]
        public void FindMultiplePairs()
        {
            var result = Array.GetNumberAddingUpToSum(new[] { 1, 2, 5, 4 }, 6);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result[0][0]);
            Assert.AreEqual(5, result[0][1]);
            Assert.AreEqual(2, result[1][0]);
            Assert.AreEqual(4, result[1][1]);
        }

        [TestMethod]
        public void FindNegativePair()
        {
            var result = Array.GetNumberAddingUpToSum(new[] { 1, 2, 7, -3 }, 4);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(7, result[0][0]);
            Assert.AreEqual(-3, result[0][1]);
        }

        #endregion

        #region GetNumberAddingUpToSum

        [TestMethod]
        public void ZeroIsNullNumbers()
        {
            var result = Array.GetLargestContinuousSum(null);

            Assert.AreEqual(0, result);
        } 

        [TestMethod]
        public void AllPositiveNumbers()
        {
            var result = Array.GetLargestContinuousSum(new[] { 1, 2, 3 });

            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void AllNegativeShouldBeZero()
        {
            var result = Array.GetLargestContinuousSum(new[] { -1, -2, -3 });

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void ResetInTheMiddle()
        {
            var result = Array.GetLargestContinuousSum(new[] { 1, -2, 2 });

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void NegativeTheMiddle()
        {
            var result = Array.GetLargestContinuousSum(new[] { 5, -2, 5 });

            Assert.AreEqual(8, result);
        }

        #endregion
    }
}
