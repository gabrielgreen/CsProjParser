using System;
using System.Collections.Generic;

namespace CsProjParser
{
    public class Tree<T>
    {
        public Tree()
        {
            Nodes = new List<T>();
            Edges = new List<Tuple<T, T>>();
        }

        public List<T> Nodes { get; set; }

        public List<Tuple<T, T>> Edges { get; set; }
    }
}
