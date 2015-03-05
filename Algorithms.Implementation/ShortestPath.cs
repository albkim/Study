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

        /// <summary>
        /// Given two words (start and end), and a dictionary, find the length of shortest transformation sequence from start to end, such that:
        ///
        /// Only one letter can be changed at a time
        /// Each intermediate word must exist in the dictionary
        /// For example,
        /// 
        /// Given:
        /// start = "hit"
        /// end = "cog"
        /// dict = ["hot","dot","dog","lot","log"]
        /// As one shortest transformation is "hit" -> "hot" -> "dot" -> "dog" -> "cog",
        /// return its length 5.
        ///
        /// Note:
        /// Return 0 if there is no such transformation sequence.
        /// All words have the same length.
        /// All words contain only lowercase alphabetic characters.
        /// 
        /// realize that this is a shortest path problem if we are able to form the following graph
        ///             dot
        ///          /   /   \
        /// hit -- hot   /    dog
        ///         \   /     /  \
        ///           lot--log--cog
        ///           
        /// Simplest will be BFS while deciding which word is is eligible and not travelled
        /// </summary>
        /// <param name="words"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static List<string> WordLadder(List<string> words, string start, string end)
        {
            //to store travelled nodes and previous for contructing the path
            Dictionary<string, string> check = new Dictionary<string, string>();
            //queue to use in bfs
            Queue<string> queue = new Queue<string>();

            //add end to the list of word as a potential node to travel
            words.Add(end);

            //start with starting point
            queue.Enqueue(start);
            check.Add(start, null);

            //until queue is empty;
            while (queue.Count > 0)
            {
                string current = queue.Dequeue();

                //terminal condition; if we reached the end;
                if (current == end)
                {
                    //do stuff like constructing the path and return
                    List<string> path = new List<string>();
                    string pathElement = end;
                    while (pathElement != start)
                    {
                        path.Insert(0, pathElement);
                        pathElement = check[pathElement];
                    }
                    path.Insert(0, start);
                    return path;
                }

                //check if there is a node to travel
                foreach (string newWord in words)
                {
                    //if not travelled and off by 1 letter
                    if ((!check.ContainsKey(newWord)) && (DifferByOneLetter(current, newWord)))
                    {
                        //queue for potential travel
                        queue.Enqueue(newWord);
                        //add to check to track
                        check.Add(newWord, current);
                    }
                }
            }

            return null; //if we reached here no path
        }

        private static bool DifferByOneLetter(string current, string newWord)
        {
            int countDifferentLetter = 0;
            for (int index = 0; index < current.Length; index++)
            {
                countDifferentLetter += (current[index] == newWord[index]) ? 0 : 1;
            }
            return countDifferentLetter == 1;
        }

    }
}
