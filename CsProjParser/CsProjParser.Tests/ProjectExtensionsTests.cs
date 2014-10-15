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
            Assert.AreEqual("Empower.Common", _result.Nodes.Single(c => c.Value.Name == "Empower.Common").Value.Name);
            Assert.AreEqual("Empower.Framework", _result.Nodes.Single(c => c.Value.Name == "Empower.Framework").Value.Name);
            Assert.AreEqual("Newtonsoft.Json", _result.Nodes.Single(c => c.Value.Name == "Newtonsoft.Json").Value.Name);
            Assert.AreEqual("RabbitMQ.Client", _result.Nodes.Single(c => c.Value.Name == "RabbitMQ.Client").Value.Name);
        }

        [TestMethod]
        public void ToDependencyTree_EachProjectReferencesIsRepresentedByASingleNode()
        {
            Assert.AreEqual("AcademyMortgage.Empower.Core", _result.Nodes.Single(c => c.Value.Name == "AcademyMortgage.Empower.Core").Value.Name);
            Assert.AreEqual("AcademyMortgage.Empower.Messaging", _result.Nodes.Single(c => c.Value.Name == "AcademyMortgage.Empower.Messaging").Value.Name);
        }

        [TestMethod]
        public void ToDependencyTree_TheProjectItselfIsRepresentedByASingleNode()
        {
            Assert.AreEqual("fixture1", _result.Nodes.Single(c => c.Value.Name == "fixture1").Value.Name);
            Assert.IsInstanceOfType(_result.Nodes.Single(c => c.Value.Name == "fixture1").Value, typeof(CsProjParser.Models.Project));
        }

        [TestMethod]
        public void ToDependencyTree_ThereAreAsManyNodesAsAssemblyAndProjectReferencesPlusOneForTheProject()
        {
            Assert.AreEqual(7, _result.Nodes.Count);
        }

        [TestMethod]
        public void ToDependencyTree_ThereIsAnEdgeFromTheProjectToEachAssemblyReference()
        {
            Assert.IsNotNull(_result.Edges.Single(c => c.Item1.Value.Name == "fixture1" && c.Item2.Value.Name == "Empower.Common"));
            Assert.IsNotNull(_result.Edges.Single(c => c.Item1.Value.Name == "fixture1" && c.Item2.Value.Name == "Empower.Framework"));
            Assert.IsNotNull(_result.Edges.Single(c => c.Item1.Value.Name == "fixture1" && c.Item2.Value.Name == "Newtonsoft.Json"));
            Assert.IsNotNull(_result.Edges.Single(c => c.Item1.Value.Name == "fixture1" && c.Item2.Value.Name == "RabbitMQ.Client"));
        }

        [TestMethod]
        public void ToDependencyTree_ThereIsAnEdgeFromTheProjectToEachProjectReference()
        {
            Assert.IsNotNull(_result.Edges.Single(c => c.Item1.Value.Name == "fixture1" && c.Item2.Value.Name == "AcademyMortgage.Empower.Core"));
            Assert.IsNotNull(_result.Edges.Single(c => c.Item1.Value.Name == "fixture1" && c.Item2.Value.Name == "AcademyMortgage.Empower.Messaging"));
        }

        [TestMethod]
        public void ToDependencyTree_ThereAreAsManyEdgesAsAssemblyAndProjectReferences()
        {
            Assert.AreEqual(6, _result.Edges.Count);
        }
    }
}
