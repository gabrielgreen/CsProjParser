using CsProjParser.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace CsProjParser.Tests
{
    [TestClass]
    public class TreeExtensionsTests
    {
        private System.Collections.Generic.List<string> _result;

        [TestInitialize]
        public void Initialize()
        {
            var a = new Node<Item> { Value = new Item { Name = "A" } };
            var b = new Node<Item> { Value = new Item { Name = "B" } };
            var c = new Node<Item> { Value = new Item { Name = "C" } };

            var tree = new Tree<Item>();
            tree.Nodes.AddRange(new[] {a, b, c});
            tree.Edges.AddRange(new[] {
                new Edge<Item> { Item1 = a, Item2 = b },
                new Edge<Item> { Item1 = a, Item2 = c },
            });

            _result = tree.ToGraphViz();
        }

        [TestMethod]
        public void ToGraphViz_ResultStartsWithDiGraphDeclaration()
        {
            Assert.AreEqual("digraph {", _result.First());
        }

        [TestMethod]
        public void ToGraphViz_ResultDeclaresEachNode()
        {
            Assert.IsNotNull(_result.Single(c => c == "A;"));
            Assert.IsNotNull(_result.Single(c => c == "B;"));
            Assert.IsNotNull(_result.Single(c => c == "C;"));
        }

        [TestMethod]
        public void ToGraphViz_ResultDeclaresEachEdge()
        {
            Assert.IsNotNull(_result.Single(c => c == "A->B;"));
            Assert.IsNotNull(_result.Single(c => c == "A->C;"));
        }

        [TestMethod]
        public void ToGraphViz_ResultEndsWithClosingDiGraphDeclaration()
        {
            Assert.AreEqual("} // digraph", _result.Last());
        }
    }
}
 