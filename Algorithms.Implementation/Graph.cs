using Algorithms.Implementation.Models.Graphes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Implementation.Models
{
    public class Graph
    {
        public GraphNode Head { get; set; }

        #region Detect Circulr Dependency
        
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
            foreach (var node in current.Edges)
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

        #endregion

        #region Show Dependencies DFS

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
            if (current.Edges.Count > 0)
            {
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

        #endregion

        #region Task Scheduler

        public class Task
        {

            private List<Task> dependencies = new List<Task>();

            public void Run() { }

            public int Value { get; set; }

            public List<Task> Dependencies
            {
                get
                {
                    return this.dependencies;
                }
                set
                {
                    this.dependencies = value;
                }
            }
        }

        /// <summary>
        /// Given the interface below, implement a task scheduler.
        /// 
        /// Additionally, the task scheduler should follow two rules.
        /// 1. Each task may only be executed once.
        /// 2. The dependencies of a task should be executed before the task itself.
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns>
        public static List<int> TaskScheduler(List<Task> tasks)
        {
            List<int> result = new List<int>();
            Dictionary<int, Task> check = new Dictionary<int, Task>();

            TaskScheduler(tasks, check, result);

            return result;
        }

        private static void TaskScheduler(List<Task> tasks, Dictionary<int, Task> check, List<int> result)
        {
            foreach (Task task in tasks)
            {
                if (!check.ContainsKey(task.Value))
                {
                    //add to check to prevent cycles
                    check.Add(task.Value, task);

                    //process dependencies first
                    if (task.Dependencies.Count > 0)
                    {
                        TaskScheduler(task.Dependencies, check, result);
                    }

                    //now process the current
                    result.Add(task.Value);
                }
            }
        }

        #endregion

        #region Topological Sort

        /// <summary>
        /// for directed acyclic graph
        /// 
        /// 
        ///         D    E
        ///        / \  / \
        ///      \/   \/   \/
        ///       B   C   F
        ///        \     /\
        ///         \/  /
        ///           A
        ///           
        ///     E -> D -> C -> B -> A -> F
        ///     
        /// Kahn's algorithm (O(v) + O(e))
        /// 
        ///     for each node with incoming edge, add it to a queue
        ///     
        ///     while queue is not empty
        ///         remove a node, and add it to the tail of the sorted list
        ///         for each edge for the node
        ///             remove the edge
        ///             if target node has no other edge, add it to the queue to be considered next
        ///     
        ///     if graph still has edges
        ///         there is a cycle
        ///     else
        ///         return sorted list
        ///         
        /// 
        ///     D -> B (and one edge removed from C)
        ///     E -> C (and one edge removed from F)
        ///     B -> A
        ///     C
        ///     A -> F
        ///     F
        ///     
        /// so D -> E -> B -> C -> A -> F which is a valid topological sort
        /// </summary>
        /// <returns></returns>
        public static List<char> TopologicalSort(Graph<char> graph)
        {
            Queue<Node<char>> queue = new Queue<Node<char>>();
            List<char> sorted = new List<char>();

            //parse the edges into an easier lookup
            Dictionary<char, List<Edge<char>>> incomingEdges = new Dictionary<char, List<Edge<char>>>();
            Dictionary<char, List<Edge<char>>> outgoingEdges = new Dictionary<char, List<Edge<char>>>();

            //initialize lookup for each node
            foreach(Node<char> node in graph.Nodes)
            {
                incomingEdges[node.Value] = new List<Edge<char>>();
                outgoingEdges[node.Value] = new List<Edge<char>>();
            }

            //parse edges into look up
            foreach (Edge<char> edge in graph.Edges)
            {
                incomingEdges[edge.Right.Value].Add(edge);
                outgoingEdges[edge.Left.Value].Add(edge);
            }

            //now find the candidates
            foreach(Node<char> node in graph.Nodes) {
                if (incomingEdges[node.Value].Count == 0) {
                    queue.Enqueue(node);
                    incomingEdges.Remove(node.Value);
                }
            }

            while (queue.Count > 0)
            {
                Node<char> node = queue.Dequeue();
                sorted.Insert(0, node.Value);

                //now consider the connected nodes and number of incoming edges for each
                foreach (Edge<char> nodeEdge in outgoingEdges[node.Value])
                {
                    Node<char> neighborNode = nodeEdge.Right;
                    List<Edge<char>> incomingNodeEdges = incomingEdges[neighborNode.Value];
                    incomingNodeEdges.Remove(nodeEdge);

                    //now this is a next candidate
                    if (incomingNodeEdges.Count == 0)
                    {
                        queue.Enqueue(neighborNode);
                        incomingEdges.Remove(neighborNode.Value);
                    }
                }
            }

            if (incomingEdges.Count > 0)
            {
                throw new Exception("Cycle found");
            }

            return sorted;
        }

        #endregion
    }

}
