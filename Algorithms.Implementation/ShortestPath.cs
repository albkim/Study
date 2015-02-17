using Algorithms.Implementation.Models.Graphes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Implementation
{

    public class ShortestPath
    {

        /// <summary>
        /// Used for unweighted, non negative shortest path
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static List<int> BFS(Dictionary<int, List<int>> graph, int start, int end)
        {
            Queue<int> queue = new Queue<int>();

            //used to check for circular path and store previous node
            Dictionary<int, int> check = new Dictionary<int,int>();

            queue.Enqueue(start);
            while (queue.Count > 0)
            {
                int node = queue.Dequeue();
                
                //do some logic on this node
                if (node == end)
                {
                    //we have not found the node. return the path
                    return GetPath(check, start, end);
                }

                if (graph.ContainsKey(node))
                {
                    //look at the connected nodes
                    foreach (int connected in graph[node])
                    {
                        //make sure we haven't seen this node
                        if (!check.ContainsKey(connected))
                        {
                            queue.Enqueue(connected);

                            //add this node as visited and then store previous node
                            //this can later be used to reconstruct the path
                            check.Add(connected, node);
                        }
                    }
                }
            }

            throw new InvalidOperationException("Cannot find path");
        }

        /// <summary>
        /// For weighted, non negative shortest path
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static List<int> Djikstra(Graph<int> graph, int start, int end)
        {
            //Potential suspect
            Dictionary<int, Node<int>> queue = new Dictionary<int, Node<int>>();

            //Djikstra score
            Dictionary<int, int> scores = new Dictionary<int, int>();
            //Use to check for circular and to track previous
            Dictionary<int, int> check = new Dictionary<int, int>();

            //look at each edge crossing the boundary
            //calculate score which is previous score + current edge cost
            //take the one with the lowest score
            DjikstraTake(graph, queue, check, scores, start, start, 0);

            while (queue.Count > 0)
            {
                //look at all elements in the queue and take the one with minimum score
                Edge<int> minEdge = null;
                int minScore = int.MaxValue;

                foreach (Node<int> currentNode in queue.Values)
                {
                    //look at all the edges, if it comes from added nodes, find the min score
                    foreach(Edge<int> edge in graph.Edges)
                    {
                        if ((check.ContainsKey(edge.Left.Value)) && (currentNode.Value == edge.Right.Value))
                        {
                            int currentScore = scores[edge.Left.Value] + edge.Weight;
                            if (currentScore < minScore)
                            {
                                minEdge = edge;
                                minScore = currentScore;
                            }
                        }
                    }
                }

                //if we found the good node, take it
                queue.Remove(minEdge.Right.Value);

                DjikstraTake(graph, queue, check, scores, minEdge.Right.Value, minEdge.Left.Value, minScore);

                //if we found the match return the path
                if (minEdge.Right.Value == end)
                {
                    return GetPath(check, start, end);
                }
            }

            throw new InvalidOperationException("Cannot find path");
        }

        private static void DjikstraTake(Graph<int> graph, Dictionary<int, Node<int>> queue, Dictionary<int, int> check,
            Dictionary<int, int> scores, int node, int previous, int score)
        {
            check.Add(node, previous);
            scores.Add(node, score);

            foreach (Edge<int> edge in graph.Edges)
            {
                if ((node == edge.Left.Value) && (!check.ContainsKey(edge.Right.Value)) && (!queue.ContainsKey(edge.Right.Value)))
                {
                    queue.Add(edge.Right.Value, edge.Right);
                }
            }
        }

        private static List<int> GetPath(Dictionary<int, int> check, int start, int end)
        {
            List<int> path = new List<int>();
            int pathElement = end;
            while (pathElement != start)
            {
                path.Insert(0, pathElement);
                pathElement = check[pathElement];
            }
            path.Insert(0, start);
            return path;
        }

    }
}
