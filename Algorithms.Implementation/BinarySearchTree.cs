using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Algorithms.Implementation.Models;

namespace Algorithms.Implementation
{
    public class BinarySearchTree
    {
        public TreeNode Root { get; set; }

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

        private static bool IsValidBstNode(TreeNode root, int minValue, int maxValue)
        {
            var left = (root.Left == null) ? true : IsValidBstNode(root.Left, minValue, (int)root.Value);
            var right = (root.Right == null) ? true : IsValidBstNode(root.Right, (int)root.Value, maxValue);

            return left && right && (minValue < (int)root.Value) && (maxValue > (int)root.Value);
        }
    }
}
