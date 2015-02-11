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

    }
}
