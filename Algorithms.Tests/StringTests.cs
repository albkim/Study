using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using String = Algorithms.Implementation.String;

namespace Algorithms.Tests
{
    [TestClass]
    public class StringTests
    {
        #region Boyer Moore Replace

        [TestMethod]
        public void TestReplaceSimple()
        {
            var result = String.Replace("aabbccdd", "bb", "11");

            Assert.AreEqual("aa11ccdd", result);
        }

        [TestMethod]
        public void TestReplaceLargerSearch()
        {
            var result = String.Replace("aabbccdd", "bbcc", "11");

            Assert.AreEqual("aa11dd", result);
        }

        [TestMethod]
        public void TestReplaceLargerReplace()
        {
            var result = String.Replace("aabbccdd", "bb", "11111");

            Assert.AreEqual("aa11111ccdd", result);
        }

        [TestMethod]
        public void TestReplaceLargerMultiple()
        {
            var result = String.Replace("aabbccddbb", "bb", "11");

            Assert.AreEqual("aa11ccdd11", result);
        }

        [TestMethod]
        public void TestReplaceLargerStartSame()
        {
            var result = String.Replace("aaaabbccdd", "aabb", "11");

            Assert.AreEqual("aa11ccdd", result);
        }

        [TestMethod]
        public void TestReplaceLargerStartOdd()
        {
            var result = String.Replace("aaabbccdd", "aabb", "11");

            Assert.AreEqual("a11ccdd", result);
        }

        #endregion

        #region FindAllPermutations

        [TestMethod]
        public void TestReverseWord()
        {
            var result = String.ReverseWord("Interviews are awesome!");

            Assert.AreEqual("awesome! are Interviews", result);
        }

        #endregion

        #region FindAllPhoneNumberPermutations

        [TestMethod]
        public void HandleZero()
        {
            var result = String.FindAllPhoneNumberPermutations("0000000000");

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void HandleOne()
        {
            var result = String.FindAllPhoneNumberPermutations("1111111111");

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void HandleOneNumer()
        {
            var result = String.FindAllPhoneNumberPermutations("0000000002");

            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void HandleMultipleNumer()
        {
            var result = String.FindAllPhoneNumberPermutations("0000000022");

            Assert.AreEqual(9, result.Count);
        }

        #endregion

        #region FindAllPermutations

        [TestMethod]
        public void TestSimplePermutation()
        {
            var result = String.FindAllPermutations("LSE");

            Assert.IsNotNull(result);
            Assert.AreEqual(6, result.Count);

            Assert.AreEqual("LSE", result[0]);
            Assert.AreEqual("SLE", result[1]);
            Assert.AreEqual("SEL", result[2]);
            Assert.AreEqual("LES", result[3]);
            Assert.AreEqual("ELS", result[4]);
            Assert.AreEqual("ESL", result[5]);
        }

        #endregion

        #region ValidShuffle

        [TestMethod]
        public void FalseDiffLength()
        {
            var result = String.ValidShuffle("abc", "def", "ab");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void FalseDiffOrder()
        {
            var result = String.ValidShuffle("abc", "def", "abdfec");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void FalseWrongCharacter()
        {
            var result = String.ValidShuffle("abc", "def", "abhfec");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TrueLeftFirst()
        {
            var result = String.ValidShuffle("abc", "def", "abcdef");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TrueRightFirst()
        {
            var result = String.ValidShuffle("abc", "def", "defabc");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TrueMixed()
        {
            var result = String.ValidShuffle("abc", "def", "adbecf");

            Assert.IsTrue(result);
        }

        #endregion
    }
}
