using System;

using Algorithms.Implementation.Models;

namespace Algorithms.Implementation
{
    public class LinkedList
    {
        public Node Head { get; set; }

        public void Remove(int value)
        {
            if (Head == null)
            {
                throw new InvalidOperationException();
            }

            while (Head != null && (int)Head.Value == value)
            {
                Head = Head.Next;
            }

            if (Head != null)
            {
                var previousNode = Head;
                var currentNode = Head.Next;
                while (currentNode != null)
                {
                    if ((int)currentNode.Value == value)
                    {
                        previousNode.Next = currentNode.Next;
                    }
                    else
                    {
                        previousNode = currentNode;
                    }
                    currentNode = currentNode.Next;
                }
            }
        }
        
        public void Reverse()
        {
            if (Head != null)
            {
                ReverseNeighbor(Head);
            }
        }

        private void ReverseNeighbor(Node node)
        {
            if (node.Next != null)
            {
                var neighbor = node.Next;
                ReverseNeighbor(neighbor);

                neighbor.Next = node;
                node.Next = null;
            }
            else
            {
                Head = node;
            }
        }

        /*
         * node = head, neighbor = second
         * node = second, neighbor = third
         * head = third
         * third.next = second, second.next = null
         * second.next = head, head.next = null
         */
    }
}
