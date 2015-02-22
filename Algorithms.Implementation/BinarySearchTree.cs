using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Algorithms.Implementation.Models;

namespace Algorithms.Implementation
{

    /// <summary>
    /// In order - left, current, right
    /// Pre order - current, left, right
    /// Post order - left, right, current
    /// 
    /// for BST, we cannot determine the structure unless we have in order traversal
    /// </summary>

    public class BinarySearchTree
    {
        public TreeNode<int> Root { get; set; }

        public object NthLargestItem()
        {
            //do reverse of inorder traversal, in order traversal is always sorted by value, so it's matter of doing this and then counting

            //visit right
            //visit self
            //visit left

            return null;
        }

        public bool IsValidBST()
        {
            if (Root == null)
            {
                return true;
            }

            return IsValidBstNode(Root, int.MinValue, int.MaxValue);
        }

        private static bool IsValidBstNode(TreeNode<int> root, int minValue, int maxValue)
        {
            var left = (root.Left == null) ? true : IsValidBstNode(root.Left, minValue, root.Value);
            var right = (root.Right == null) ? true : IsValidBstNode(root.Right, root.Value, maxValue);

            return left && right && (minValue < root.Value) && (maxValue > root.Value);
        }
    }
}
