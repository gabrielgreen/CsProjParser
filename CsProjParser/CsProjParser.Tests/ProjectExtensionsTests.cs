using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace CsProjParser.Tests
{
    [TestClass]
    [DeploymentItem("fixture1.csproj")]
    public class ProjectExtensionsTests
    {
        private Tree<Models.Item> _result;

        [TestInitialize]
        public void Initialize()
        {
            var target = new ProjectParser();
            var project = target.Parse("fixture1.csproj");
            _result = project.ToDependencyTree();
        }

        [TestMethod]
        public void ToDependencyTree_EachAssemblyReferencesIsRepresentedByASingleNode()
        {
            Assert.AreEqual("Empower.Common", _result.Nodes.Single(c => c.Name == "Empower.Common").Name);
            Assert.AreEqual("Empower.Framework", _result.Nodes.Single(c => c.Name == "Empower.Framework").Name);
            Assert.AreEqual("Newtonsoft.Json", _result.Nodes.Single(c => c.Name == "Newtonsoft.Json").Name);
            Assert.AreEqual("RabbitMQ.Client", _result.Nodes.Single(c => c.Name == "RabbitMQ.Client").Name);
        }

        [TestMethod]
        public void ToDependencyTree_EachProjectReferencesIsRepresentedByASingleNode()
        {
            Assert.AreEqual("AcademyMortgage.Empower.Core", _result.Nodes.Single(c => c.Name == "AcademyMortgage.Empower.Core").Name);
            Assert.AreEqual("AcademyMortgage.Empower.Messaging", _result.Nodes.Single(c => c.Name == "AcademyMortgage.Empower.Messaging").Name);
        }

        [TestMethod]
        public void ToDependencyTree_TheProjectItselfIsRepresentedByASingleNode()
        {
            Assert.AreEqual("fixture1", _result.Nodes.Single(c => c.Name == "fixture1").Name);
            Assert.IsInstanceOfType(_result.Nodes.Single(c => c.Name == "fixture1"), typeof(CsProjParser.Models.Project));
        }

        [TestMethod]
        public void ToDependencyTree_ThereAreAsManyNodesAsAssemblyAndProjectReferencesPlusOneForTheProject()
        {
            Assert.AreEqual(7, _result.Nodes.Count);
        }

        [TestMethod]
        public void ToDependencyTree_ThereIsAnEdgeFromTheProjectToEachAssemblyReference()
        {
            Assert.IsNotNull(_result.Edges.Single(c => c.Item1.Name == "fixture1" && c.Item2.Name == "Empower.Common"));
            Assert.IsNotNull(_result.Edges.Single(c => c.Item1.Name == "fixture1" && c.Item2.Name == "Empower.Framework"));
            Assert.IsNotNull(_result.Edges.Single(c => c.Item1.Name == "fixture1" && c.Item2.Name == "Newtonsoft.Json"));
            Assert.IsNotNull(_result.Edges.Single(c => c.Item1.Name == "fixture1" && c.Item2.Name == "RabbitMQ.Client"));
        }

        [TestMethod]
        public void ToDependencyTree_ThereIsAnEdgeFromTheProjectToEachProjectReference()
        {
            Assert.IsNotNull(_result.Edges.Single(c => c.Item1.Name == "fixture1" && c.Item2.Name == "AcademyMortgage.Empower.Core"));
            Assert.IsNotNull(_result.Edges.Single(c => c.Item1.Name == "fixture1" && c.Item2.Name == "AcademyMortgage.Empower.Messaging"));
        }

        [TestMethod]
        public void ToDependencyTree_ThereAreAsManyEdgesAsAssemblyAndProjectReferences()
        {
            Assert.AreEqual(6, _result.Edges.Count);
        }
    }
}
