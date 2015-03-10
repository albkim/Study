using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Algorithms.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithms.Tests
{
    [TestClass]
    public class DynamicProgrammingTests
    {

        #region Knapsack

        [TestMethod]
        public void Simple()
        {
            List<int> choices = new List<int>();
            var result = DynamicProgramming.Knapsack(5, new int[] { 2, 3, 4, 5 }, new int[] { 3, 7, 2, 9 }, choices);

            Assert.AreEqual(10, result);
            Assert.AreEqual(2, choices.Count());
            Assert.AreEqual(3, choices[0]);
            Assert.AreEqual(2, choices[1]);
        }
        
        #endregion

        #region Edit Distance

        [TestMethod]
        public void EditDistanceRecursive()
        {
            Assert.AreEqual(3, DynamicProgramming.EditDistanceRecursive("SATURDAY", "SUNDAY"));
        }

        [TestMethod]
        public void EditDistanceDynamicProgramming()
        {
            Assert.AreEqual(3, DynamicProgramming.EditDistanceDynamicProgramming("SATURDAY", "SUNDAY"));
        }

        #endregion

        #region Longest Subsequence

        [TestMethod]
        public void LongestSubstringRecursive()
        {
            var result = DynamicProgramming.LongestSubstringRecursive("BANANA", "ATANA");

            Assert.AreEqual("AANA", result);
        }

        [TestMethod]
        public void LongestSubstringDynamic()
        {
            var result = DynamicProgramming.LongestSubstringDynamic("BANANA", "ATANA");

            Assert.AreEqual("AANA", result);
        }

        #endregion

        #region Matrix Traverse

        [TestMethod]
        public void TraverseMatrix()
        {
            Assert.AreEqual(2, DynamicProgramming.TraverseMatrix(3, 2));
        }

        #endregion

    }
}
