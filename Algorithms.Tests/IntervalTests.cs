using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms.Implementation;
using System.Collections.Generic;

namespace Algorithms.Tests
{
    
    [TestClass]
    public class IntervalTests
    {
        
        [TestMethod]
        public void Simple()
        {
            Assert.AreEqual(6, Interval.GetTotalCoveredLength(new List<int[]>
            {
                new int[] { 3, 6 },
                new int[] { 8, 9 },
                new int[] { 1, 5 }
            }));

        }
    }

}
