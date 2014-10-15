using CsProjParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CsProjParser
{
    public static class TreeExtensions
    {
        public static List<string> ToGraphViz<T>(this Tree<T> tree) where T : Item
        {
            var result = new List<string>();

            result.Add("digraph {");

            result.AddRange(tree.Nodes.Select(c => c.Value.Name + ";"));

            result.AddRange(tree.Edges.Select(c => c.Item1.Value.Name + "->" + c.Item2.Value.Name + ";"));

            result.Add("} // digraph");

            return result;
        }
    }
}
