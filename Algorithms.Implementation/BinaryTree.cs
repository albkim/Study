using Algorithms.Implementation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Implementation
{

    public class BinaryTree<T>
    {

        public TreeNode<T> Root { get; set; }

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
        ///   
        /// Use a sentinel node when parsing nodes into the queue
        /// If sentinel node is encountered, put another one (we parsed
        /// all previous level) and end the level
        /// </summary>
        /// <returns></returns>
        public List<List<T>> BFSWithLevel()
        {
            List<List<T>> result = new List<List<T>>();

            Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();

            queue.Enqueue(this.Root);

            //put sentinel
            queue.Enqueue(new SentinelTreeNode<T>());

            List<T> level = new List<T>();
            while (queue.Count > 0)
            {
                TreeNode<T> node = queue.Dequeue();

                //if we don't check for count, we keep repeating sentinel node
                //since if the last node is sentinel we just put it right back
                if ((node is SentinelTreeNode<T>) && (level.Count > 0))
                {
                    //we encountered a sentinel node which means we are done processing previous level
                    result.Add(level);

                    //reinitialize level
                    level = new List<T>();

                    //requeue the sentinel node
                    queue.Enqueue(node);
                }
                else
                {
                    //normal nodes, do BFS
                    level.Add(node.Value);

                    if (node.Left != null)
                    {
                        queue.Enqueue(node.Left);
                    }
                    if (node.Right != null)
                    {
                        queue.Enqueue(node.Right);
                    }
                }
            }

            return result;
        }

        public List<List<T>> BFSWithLevelRecursion()
        {
            List<List<T>> result = new List<List<T>>();

            Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
            queue.Enqueue(this.Root);

            this.BFSWithLevelRecursion(result, queue);

            return result;
        }

        private void BFSWithLevelRecursion(List<List<T>> result, Queue<TreeNode<T>> queue)
        {
            Queue<TreeNode<T>> lowerLevel = new Queue<TreeNode<T>>();

            List<T> level = new List<T>();
            foreach(TreeNode<T> node in queue) {
                if (node.Left != null)
                {
                    lowerLevel.Enqueue(node.Left);
                }
                if (node.Right != null)
                {
                    lowerLevel.Enqueue(node.Right);
                }

                level.Add(node.Value);
            }

            result.Add(level);

            if (lowerLevel.Count > 0)
            {
                BFSWithLevelRecursion(result, lowerLevel);
            }
        }

        public List<List<T>> LevelZigZag()
        {
            List<List<T>> result = new List<List<T>>();

            Stack<TreeNode<T>> stack = new Stack<TreeNode<T>>();
            stack.Push(this.Root);

            this.LevelZigZag(result, stack, false);

            return result;
        }

        private void LevelZigZag(List<List<T>> result, Stack<TreeNode<T>> stack, bool leftFirst)
        {
            Stack<TreeNode<T>> lowerLevel = new Stack<TreeNode<T>>();

            List<T> level = new List<T>();
            
            while(stack.Count > 0)
            {
                TreeNode<T> node = stack.Pop();
                
                TreeNode<T> newFirstNode = (leftFirst) ? node.Left : node.Right;
                TreeNode<T> newSecondNode = (leftFirst) ? node.Right : node.Left;
                
                if (newFirstNode != null)
                {
                    lowerLevel.Push(newFirstNode);
                }
                if (newSecondNode != null)
                {
                    lowerLevel.Push(newSecondNode);
                }

                level.Add(node.Value);
            }

            result.Add(level);

            if (lowerLevel.Count > 0)
            {
                LevelZigZag(result, lowerLevel, !leftFirst);
            }
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
        /// Perform post order DFS with count, if count is two, then we have found it. 
        /// There are two cases
        ///     When we get match from each sub tree
        ///     When both are in the same sub tree, doing post order will ensure that it finds the ancestor not itself
        public T LeastCommonAncestor(T val1, T val2)
        {
            T leastCommonAncestor = default(T);

            int count = this.LeastCommonAncestor(this.Root, val1, val2, ref leastCommonAncestor);

            if ((count == 2) && (!leastCommonAncestor.Equals(default(T))))
            {
                //we found it
                return leastCommonAncestor;
            }

            //cannot find one maybe nodes are not valid
            throw new ArgumentException();
        }

        private int LeastCommonAncestor(TreeNode<T> node, T val1, T val2, ref T leastCommonAncestor)
        {
            int count = 0;

            if (node.Left != null)
            {
                count += this.LeastCommonAncestor(node.Left, val1, val2, ref leastCommonAncestor);
            }
            if (node.Right != null)
            {
                count += this.LeastCommonAncestor(node.Right, val1, val2, ref leastCommonAncestor);
            }

            //if we assume no duplicate, we will never find more than 2 so it's sufficient to check only once here
            if ((count == 2) && (leastCommonAncestor.Equals(default(T))))
            {
                //also make sure we check is leastCommonAncestor is not set, otherwise we will keep overriding it
                leastCommonAncestor = node.Value;
            }

            //now check for self
            if ((node.Value.Equals(val1)) || (node.Value.Equals(val2)))
            {
                count++;
            }

            return count;
        }

        #endregion

    }

}
