using CsProjParser.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
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
            Item a =  new Item {Name = "A"};
            Item b = new Item {Name = "B"};
            Item c =  new Item {Name = "C"};

            var tree = new Tree<Item>();
            tree.Nodes.AddRange(new[] {a, b, c});
            tree.Edges.AddRange(new[] {
                Tuple.Create<Item, Item>(a, b),
                Tuple.Create<Item, Item>(a, c),
            });

            _result = tree.ToGraphVizDot();
        }

        [TestMethod]
        public void ToGraphVizDot_ResultStartsWithDiGraphDeclaration()
        {
            Assert.AreEqual("digraph {", _result.First());
        }

        [TestMethod]
        public void ToGraphVizDot_ResultDeclaresEachNode()
        {
            Assert.IsNotNull(_result.Single(c => c == "A;"));
            Assert.IsNotNull(_result.Single(c => c == "B;"));
            Assert.IsNotNull(_result.Single(c => c == "C;"));
        }

        [TestMethod]
        public void ToGraphVizDot_ResultDeclaresEachEdge()
        {
            Assert.IsNotNull(_result.Single(c => c == "A->B;"));
            Assert.IsNotNull(_result.Single(c => c == "A->C;"));
        }

        [TestMethod]
        public void ToGraphVizDot_ResultEndsWithClosingDiGraphDeclaration()
        {
            Assert.AreEqual("} // digraph", _result.Last());
        }
    }
}
