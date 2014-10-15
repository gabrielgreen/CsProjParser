using System;
using System.Collections.Generic;

namespace CsProjParser
{
    public class Tree<T>
    {
        public Tree()
        {
            Nodes = new List<Node<T>>();
            Edges = new List<Edge<T>>();
        }

        public List<Node<T>> Nodes { get; set; }

        public List<Edge<T>> Edges { get; set; }
    }

    public class TreeItem
    {
    }

    public class Node<T> : TreeItem
    {
        public T Value { get; set; }
    }

    public class Edge<T> : TreeItem
    {
        public Node<T> Item1 { get; set; }
        public Node<T> Item2 { get; set; }
    }
}
