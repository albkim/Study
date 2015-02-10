using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Algorithms.Implementation;
using Algorithms.Implementation.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithms.Tests
{
    [TestClass]
    public class BinarySearchTreeTests
    {
        [TestMethod]
        public void TrueIfNull()
        {
            var tree = new BinarySearchTree();

            Assert.IsTrue(tree.IsValidBST());
        }

        [TestMethod]
        public void TrueIfRootOnly()
        {
            var tree = new BinarySearchTree
                           {
                               Root = new TreeNode {Value = 1}
                           };

            Assert.IsTrue(tree.IsValidBST());
        }

        [TestMethod]
        public void TrueSmallerLeft()
        {
            var tree = new BinarySearchTree
            {
                Root = new TreeNode { Value = 5, Left = new TreeNode { Value = 2 } }
            };

            Assert.IsTrue(tree.IsValidBST());
        }

        [TestMethod]
        public void FalseLargerLeft()
        {
            var tree = new BinarySearchTree
            {
                Root = new TreeNode { Value = 5, Left = new TreeNode { Value = 7 } }
            };

            Assert.IsFalse(tree.IsValidBST());
        }

        [TestMethod]
        public void TrueLargerRight()
        {
            var tree = new BinarySearchTree
            {
                Root = new TreeNode { Value = 5, Right = new TreeNode { Value = 7 } }
            };

            Assert.IsTrue(tree.IsValidBST());
        }

        [TestMethod]
        public void FalseSmallerRight()
        {
            var tree = new BinarySearchTree
            {
                Root = new TreeNode { Value = 5, Right = new TreeNode { Value = 2 } }
            };

            Assert.IsFalse(tree.IsValidBST());
        }

        [TestMethod]
        public void TrueMultiLevel()
        {
            /*
             *       5
             *    2
             *  1   3
             */
            var tree = new BinarySearchTree
            {
                Root = new TreeNode { Value = 5, Left = new TreeNode { Value = 2, Left = new TreeNode { Value = 1 }, Right = new TreeNode { Value = 3 } } }
            };

            Assert.IsTrue(tree.IsValidBST());
        }

        [TestMethod]
        public void FalseMultiLevelAbsoluteLimit()
        {
            /*
             *       5
             *    2
             *  1   8
             */
            var tree = new BinarySearchTree
            {
                Root = new TreeNode { Value = 5, Left = new TreeNode { Value = 2, Left = new TreeNode { Value = 1 }, Right = new TreeNode { Value = 8 } } }
            };

            Assert.IsFalse(tree.IsValidBST());
        }
    }
}
