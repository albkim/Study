using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms.Implementation;
using Algorithms.Implementation.Models;

namespace Algorithms.Tests
{
    [TestClass]
    public class BinaryTreeTests
    {
        
        private BinaryTree<int> bt = new BinaryTree<int>();

        [TestInitialize]
        public void Setup()
        {
            TreeNode<int> root = new TreeNode<int>
            {
                Value = 1,
                Left = new TreeNode<int>
                {
                    Value = 3,
                    Left = new TreeNode<int>
                    {
                        Value = 2,
                        Left = new TreeNode<int> { Value = 9 }
                    },
                    Right = new TreeNode<int>
                    {
                        Value = 4,
                        Right = new TreeNode<int> { Value = 8 }
                    }
                },
                Right = new TreeNode<int>
                {
                    Value = 5,
                    Right = new TreeNode<int> { Value = 7 }
                }
            };

            this.bt.Root = root;
        }
        

        #region Level

        /// <summary>
        ///          1
        ///         / \
        ///        3   5
        ///       / \   \
        ///      2   4   7
        ///     /     \
        ///    9       8
        ///    
        /// Expected output:
        ///   1
        ///   3 5
        ///   2 4 7
        ///   9 8
        [TestMethod]
        public void BFSWithLevel()
        {            
            var result = this.bt.BFSWithLevel();

            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Count);

            Assert.AreEqual(1, result[0].Count);
            Assert.AreEqual(1, result[0][0]);

            Assert.AreEqual(2, result[1].Count);
            Assert.AreEqual(3, result[1][0]);
            Assert.AreEqual(5, result[1][1]);

            Assert.AreEqual(3, result[2].Count);
            Assert.AreEqual(2, result[2][0]);
            Assert.AreEqual(4, result[2][1]);
            Assert.AreEqual(7, result[2][2]);

            Assert.AreEqual(2, result[3].Count);
            Assert.AreEqual(9, result[3][0]);
            Assert.AreEqual(8, result[3][1]);
        }

        [TestMethod]
        public void BFSWithLevelRecursion()
        {
            var result = this.bt.BFSWithLevelRecursion();

            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Count);

            Assert.AreEqual(1, result[0].Count);
            Assert.AreEqual(1, result[0][0]);

            Assert.AreEqual(2, result[1].Count);
            Assert.AreEqual(3, result[1][0]);
            Assert.AreEqual(5, result[1][1]);

            Assert.AreEqual(3, result[2].Count);
            Assert.AreEqual(2, result[2][0]);
            Assert.AreEqual(4, result[2][1]);
            Assert.AreEqual(7, result[2][2]);

            Assert.AreEqual(2, result[3].Count);
            Assert.AreEqual(9, result[3][0]);
            Assert.AreEqual(8, result[3][1]);
        }

        /// <summary>
        ///          1
        ///         / \
        ///        3   5
        ///       / \   \
        ///      2   4   7
        ///     /     \
        ///    9       8
        ///    
        /// Expected output:
        ///   1
        ///   3 5
        ///   7, 4, 2
        ///   9 8
        [TestMethod]
        public void LevelZigZag()
        {
            var result = this.bt.LevelZigZag();

            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Count);

            Assert.AreEqual(1, result[0].Count);
            Assert.AreEqual(1, result[0][0]);

            Assert.AreEqual(2, result[1].Count);
            Assert.AreEqual(3, result[1][0]);
            Assert.AreEqual(5, result[1][1]);

            Assert.AreEqual(3, result[2].Count);
            Assert.AreEqual(7, result[2][0]);
            Assert.AreEqual(4, result[2][1]);
            Assert.AreEqual(2, result[2][2]);

            Assert.AreEqual(2, result[3].Count);
            Assert.AreEqual(9, result[3][0]);
            Assert.AreEqual(8, result[3][1]);
        }

        #endregion

        #region Least Common Ancestor

        /// <summary>
        ///          1
        ///         / \
        ///        3   5
        ///       / \   \
        ///      2   4   7
        ///     /     \
        ///    9       8
        ///    
        /// Expected output:
        ///   2, 5 => 1
        ///   2, 9 => 3
        ///   
        [TestMethod]
        public void LeastCommonAncestor()
        {
            //Assert.AreEqual(1, this.bt.LeastCommonAncestor(2, 5));

            Assert.AreEqual(3, this.bt.LeastCommonAncestor(2, 9));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LeastCommonAncestorNotFound()
        {
            this.bt.LeastCommonAncestor(1, 3);
            this.bt.LeastCommonAncestor(10, 3);
        }
        
        #endregion

        #region Lowest Common Ancestor With Parent

        [TestMethod]
        public void LowestCommonAncestorParent() {
            TreeNode<int> zero = new TreeNode<int> { Value = 0 };
            TreeNode<int> one = new TreeNode<int> { Value = 1 };
            TreeNode<int> two = new TreeNode<int> { Value = 2 };
            TreeNode<int> three = new TreeNode<int> { Value = 3 };
            TreeNode<int> four = new TreeNode<int> { Value = 4 };
            TreeNode<int> five = new TreeNode<int> { Value = 5 };
            TreeNode<int> six = new TreeNode<int> { Value = 6 };
            TreeNode<int> seven = new TreeNode<int> { Value = 7 };
            TreeNode<int> eight = new TreeNode<int> { Value = 8 };

            three.Left = five;
            three.Right = one;
            five.Parent = three;
            one.Parent = three;

            five.Left = six;
            five.Right = two;
            six.Parent = five;
            two.Parent = five;

            two.Left = seven;
            two.Right = four;
            seven.Parent = two;
            four.Parent = two;

            one.Left = zero;
            one.Right = eight;
            zero.Parent = one;
            eight.Parent = one;

            Assert.AreEqual(3, BinaryTree<int>.LowestCommonAncestorParent(five, one));
            Assert.AreEqual(5, BinaryTree<int>.LowestCommonAncestorParent(five, four));
        }

        #endregion

    }
}
