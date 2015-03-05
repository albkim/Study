using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Math = Algorithms.Implementation.Math;

namespace Algorithms.Tests
{
    
    [TestClass]
    public class MathTests
    {
        [TestMethod]
        public void SimpleSquareRoot()
        {
            var result = Math.SquareRoot(125348, 6);

            Assert.AreEqual(354.045, result);
        }

        [TestMethod]
        public void SumOfAllFactorOf3And5()
        {
            Assert.AreEqual(14, Math.SumOfAllFactorOf3And5(9));
            Assert.AreEqual(23, Math.SumOfAllFactorOf3And5(10));
        }
    } 

}
