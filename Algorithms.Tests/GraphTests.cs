using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithms.Tests
{
    using System.Collections.Generic;

    using Algorithms.Implementation.Models;

    [TestClass]
    public class GraphTests
    {
        #region SearchForShortestPath

        [TestMethod]
        public void SearchOne()
        {
            var ed = new GraphNode {Value = "ed", Edges = new List<GraphNode> {}};
            var ad = new GraphNode {Value = "ad", Edges = new List<GraphNode> { ed }};
            var bed = new GraphNode {Value = "bed", Edges = new List<GraphNode> { ed }};
            var bet = new GraphNode {Value = "bet", Edges = new List<GraphNode> { bed }};
            ed.Edges.Add(bed);
            var bat = new GraphNode {Value = "bat", Edges = new List<GraphNode> { bet}};
            var at = new GraphNode {Value = "at", Edges = new List<GraphNode> { ad, bat }};
            var cat = new GraphNode { Value = "cat", Edges = new List<GraphNode> { bat, at } };
            bat.Edges.Add(at);
            bat.Edges.Add(cat);
            at.Edges.Add(cat);

            var graph = new Graph {Head = cat};

            var result = graph.SearchForShortestPath("cat", "bed");

            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Count);
            Assert.AreEqual("cat", result[0]);
            Assert.AreEqual("bat", result[1]);
            Assert.AreEqual("bet", result[2]);
            Assert.AreEqual("bed", result[3]);
        }

        [TestMethod]
        public void SearchTwo()
        {
            var ed = new GraphNode { Value = "ed", Edges = new List<GraphNode> { } };
            var ad = new GraphNode { Value = "ad", Edges = new List<GraphNode> { ed } };
            var bed = new GraphNode { Value = "bed", Edges = new List<GraphNode> { ed } };
            ed.Edges.Add(bed);
            var bat = new GraphNode { Value = "bat", Edges = new List<GraphNode> { } };
            var at = new GraphNode { Value = "at", Edges = new List<GraphNode> { ad, bat } };
            var cat = new GraphNode { Value = "cat", Edges = new List<GraphNode> { bat, at } };
            bat.Edges.Add(at);
            bat.Edges.Add(cat);
            at.Edges.Add(cat);

            var graph = new Graph { Head = cat };

            var result = graph.SearchForShortestPath("cat", "bed");

            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.Count);
            Assert.AreEqual("cat", result[0]);
            Assert.AreEqual("at", result[1]);
            Assert.AreEqual("ad", result[2]);
            Assert.AreEqual("ed", result[3]);
            Assert.AreEqual("bed", result[4]);
        }

        [TestMethod]
        public void NoResult()
        {
            var ed = new GraphNode { Value = "ed", Edges = new List<GraphNode> { } };
            var bed = new GraphNode { Value = "bed", Edges = new List<GraphNode> { ed } };
            ed.Edges.Add(bed);
            var bat = new GraphNode { Value = "bat", Edges = new List<GraphNode> { } };
            var at = new GraphNode { Value = "at", Edges = new List<GraphNode> { bat } };
            var cat = new GraphNode { Value = "cat", Edges = new List<GraphNode> { bat, at } };
            bat.Edges.Add(at);
            bat.Edges.Add(cat);
            at.Edges.Add(cat);

            var graph = new Graph { Head = cat };

            var result = graph.SearchForShortestPath("cat", "bed");

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        #endregion

        #region DetectCircular

        [TestMethod]
        public void FalseHeadIsNull()
        {
            var graph = new Graph();

            var result = graph.DetectCircular();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void FalseWhenJustHead()
        {
            var graph = new Graph { Head = new GraphNode { Value = 'A' } };

            var result = graph.DetectCircular();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void FalseWhenNotCircular()
        {
            var graph = new Graph
            {
                Head = new GraphNode
                {
                    Value = 'A',
                    Edges = new List<GraphNode> {
                        new GraphNode { Value = 'B' }
                    }
                }
            };

            var result = graph.DetectCircular();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void FalseWhenNotCircularComplex()
        {
            var head = new GraphNode { Value = 'A' };
            var childOne = new GraphNode { Value = 'B' };
            var childTwo = new GraphNode { Value = 'C' };
            var common = new GraphNode { Value = 'D' };
            head.Edges.Add(childOne);
            head.Edges.Add(childTwo);
            childOne.Edges.Add(common);
            childTwo.Edges.Add(common);

            var graph = new Graph
            {
                Head = head
            };

            var result = graph.DetectCircular();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TrueWhenCircular()
        {
            var head = new GraphNode { Value = 'A' };
            var child = new GraphNode { Value = 'B' };
            head.Edges.Add(child);
            child.Edges.Add(head);

            var graph = new Graph
            {
                Head = head
            };

            var result = graph.DetectCircular();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TrueWhenCircularIndirectly()
        {
            var head = new GraphNode { Value = 'A' };
            var childOne = new GraphNode { Value = 'B' };
            var childTwo = new GraphNode { Value = 'C' };
            head.Edges.Add(childOne);
            childOne.Edges.Add(childTwo);
            childTwo.Edges.Add(head);

            var graph = new Graph
            {
                Head = head
            };

            var result = graph.DetectCircular();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TrueWhenCircularInSubset()
        {
            var head = new GraphNode { Value = 'A' };
            var childOne = new GraphNode { Value = 'B' };
            var childTwo = new GraphNode { Value = 'C' };
            head.Edges.Add(childOne);
            childOne.Edges.Add(childTwo);
            childTwo.Edges.Add(childOne);

            var graph = new Graph
            {
                Head = head
            };

            var result = graph.DetectCircular();

            Assert.IsTrue(result);
        }

        #endregion

        #region ShowDepedency

        [TestMethod]
        public void EmptyCollectionWhenHeadIsNull()
        {
            var graph = new Graph();

            var result = graph.ShowDepedency();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void OneResultWithEmptyCollectionWhenJustHead()
        {
            var graph = new Graph { Head = new GraphNode { Value = 'A' }};

            var result = graph.ShowDepedency();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(0, result['A'].Count);
        }

        [TestMethod]
        public void SingleDependency()
        {
            var graph = new Graph
            {
                Head = new GraphNode
                {
                    Value = 'A',
                    Edges = new List<GraphNode> {
                        new GraphNode { Value = 'B' }
                    }
                }
            };

            var result = graph.ShowDepedency();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result['A'].Count);
            Assert.AreEqual('B', result['A'][0]);
            Assert.AreEqual(0, result['B'].Count);
        }

        [TestMethod]
        public void MultipleDependency()
        {
            var graph = new Graph { Head = new GraphNode
                {
                    Value = 'A',
                    Edges = new List<GraphNode> {
                        new GraphNode { Value = 'B' },
                        new GraphNode { Value = 'C' }
                    }
                } };

            var result = graph.ShowDepedency();

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(2, result['A'].Count);
            Assert.AreEqual('B', result['A'][0]);
            Assert.AreEqual('C', result['A'][1]);
            Assert.AreEqual(0, result['B'].Count);
            Assert.AreEqual(0, result['C'].Count);
        }

        [TestMethod]
        public void ChildDependency()
        {
            var graph = new Graph
            {
                Head = new GraphNode
                {
                    Value = 'A',
                    Edges = new List<GraphNode> {
                        new GraphNode { 
                            Value = 'B', 
                            Edges = new List<GraphNode>
                            {
                                new GraphNode { Value = 'C' }
                            }}
                    }
                }
            };

            var result = graph.ShowDepedency();

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(2, result['A'].Count);
            Assert.AreEqual('B', result['A'][0]);
            Assert.AreEqual('C', result['A'][1]);
            Assert.AreEqual(1, result['B'].Count);
            Assert.AreEqual('C', result['B'][0]);
            Assert.AreEqual(0, result['C'].Count);
        }

        [TestMethod]
        public void ChildDependencySame()
        {
            var common = new GraphNode { Value = 'D' };
            var graph = new Graph
            {
                Head = new GraphNode
                {
                    Value = 'A',
                    Edges = new List<GraphNode> {
                        new GraphNode { 
                            Value = 'B', 
                            Edges = new List<GraphNode>
                            {
                                new GraphNode { Value = 'C', Edges = { common }},
                                common
                            }}
                    }
                }
            };

            var result = graph.ShowDepedency();

            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Count);
            Assert.AreEqual(3, result['A'].Count);
            Assert.AreEqual('B', result['A'][0]);
            Assert.AreEqual('C', result['A'][1]);
            Assert.AreEqual('D', result['A'][2]);
            Assert.AreEqual(2, result['B'].Count);
            Assert.AreEqual('C', result['B'][0]);
            Assert.AreEqual('D', result['B'][1]);
            Assert.AreEqual(1, result['C'].Count);
            Assert.AreEqual('D', result['C'][0]);
        }

        #endregion
    }
}

