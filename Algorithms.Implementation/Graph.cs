using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Implementation.Models
{
    public class Graph
    {
        public GraphNode Head { get; set; }

        public IList<string> SearchForShortestPathWeighted(string source, string target)
        {
            //use Dijkstra's algorithm

            return null;
        }

        public IList<string> SearchForShortestPath(string source, string target)
        {
            //let's assume source == head
            //also the this only works on unweighted graph
            var pathTracker = new Dictionary<string, string> {{source, null}};
            var bfsQueue = new Queue<GraphNode>();
            bfsQueue.Enqueue(Head);

            while(bfsQueue.Count > 0)
            {
                var current = bfsQueue.Dequeue();
                foreach(var node in current.Edges)
                {
                    //to prevent cycles
                    if (!pathTracker.ContainsKey((string)node.Value))
                    {
                        //add path, this will keep track of previous item, parent-child hierarchy
                        pathTracker.Add((string) node.Value, (string) current.Value);
                        if ((string) node.Value == target)
                        {
                            //we found the target, now use the path tracker to get the path
                            var result = new List<string> { target };

                            var resultCurrent = target;
                            while ((resultCurrent != null) && (pathTracker.ContainsKey(resultCurrent)))
                            {
                                resultCurrent = pathTracker[resultCurrent];
                                if (resultCurrent != null)
                                {
                                    result.Insert(0, resultCurrent);
                                }
                            }

                            return result;
                        }

                        //if not found, add to the queue to examine the edges
                        bfsQueue.Enqueue(node);
                    }
                }
            }

            return new string[] {};
        }

        public bool DetectCircular()
        {
            var result = false;
            var tracker = new Dictionary<object, bool>();

            if (Head != null)
            {
                result = Check(Head, tracker);
            }

            return result;
        }

        private bool Check(GraphNode current, Dictionary<object, bool> tracker)
        {
            //check if current node is already being checked, which means we have a circular dependency
            if (tracker.ContainsKey(current.Value) && tracker[current.Value])
            {
                return true;
            }

            //mark as currently checking
            tracker[current.Value] = true;

            //check childfren
            foreach(var node in current.Edges)
            {
                if (Check(node, tracker))
                {
                    return true;
                }
            }

            //mark as done visiting
            tracker[current.Value] = false;

            return false;
        }

        public IDictionary<object, IList<object>> ShowDepedency()
        {
            var serializer = new Dictionary<object, IList<object>>();

            if (Head != null)
            {
                Visit(Head, serializer);
            }

            return serializer;
        }

        private void Visit(GraphNode current, Dictionary<object, IList<object>> serializer)
        {
            var currentDependencies = new List<object>();

            //since dependent node could depend on other node, do depth first
            if (current.Edges.Count > 0) {
                foreach (var node in current.Edges)
                {
                    //only if we have not seriazlied this node already
                    if (!serializer.ContainsKey(node.Value))
                    {
                        //visit edges
                        Visit(node, serializer);
                    }

                    //add this node as a dependency
                    currentDependencies.Add(node.Value);

                    //see if this depends on others
                    var dependencyDepencies = serializer[node.Value];
                    if (dependencyDepencies.Count > 0)
                    {
                        currentDependencies.AddRange(dependencyDepencies);
                    }
                }
            }

            //serialize current node
            serializer.Add(current.Value, currentDependencies.Distinct().ToList());
        }
    }

}
