using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Implementation
{
    public class Hamiltonian
    {

        /// <summary>
        /// Use backtracking. 
        /// Try adding a point by point and then check if there is an edge and if point is already added
        /// If this attempt is not successful, then try adding a different point
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static bool Exists(Dictionary<int, List<int>> graph)
        {
            Dictionary<int, int> check = new Dictionary<int, int>();
            List<int> path = new List<int>();

            //add one element as the starting element
            int firstElement = graph.Keys.ElementAt(0);
            path.Add(firstElement);
            check.Add(firstElement, firstElement);

            return Exists(graph, check, path);
        }

        private static bool Exists(Dictionary<int, List<int>> graph, Dictionary<int, int> check, List<int> path)
        {
            //now all points are added
            if (path.Count == graph.Count)
            {
                //we have to do one more check such that origin is next to the last point
                int previous = path[path.Count - 1];
                if (graph[previous].Contains(path[0]))
                {
                    return true;
                }
                return false;
            }

            //try each element
            foreach (int element in graph.Keys)
            {
                //is this a valid next path (not used and is it connected to previous)
                if (!check.ContainsKey(element))
                {
                    int previous = path[path.Count - 1];
                    if (graph[previous].Contains(element))
                    {
                        //try adding it to path and recurse to find another possible next
                        path.Add(element);
                        check.Add(element, element);

                        if (Exists(graph, check, path))
                        {
                            //if we recursive succeed in completing the path, we are good
                            return true;
                        }

                        //if we couldn't get to an answer with this point, we need to back track and try a different point in this position
                        path.Remove(element);
                        check.Remove(element);
                    }
                }
            }

            //if we tried every combination and nothing is found, then we cannot do it
            return false;
        }

    }
}
