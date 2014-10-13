using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CsProjParser;
using System.IO;
using System.Linq;

namespace CsProjParser.Tests
{
    [TestClass]
    public class ProjectParserTests
    {
        [TestMethod]
        [DeploymentItem("fixture1.csproj")]
        public void Parse_fixture1_ParsesProjectGuid()
        {
            var target = new ProjectParser();

            var result = target.Parse("fixture1.csproj");

            Assert.AreEqual(Guid.Parse("0DAB71A5-CB62-447D-B78F-47F93EA2D88A"), result.ProjectGuid);
        }

        [TestMethod]
        [DeploymentItem("fixture2.csproj")]
        public void Parse_fixture2_ParsesProjectGuid()
        {
            var target = new ProjectParser();

            var result = target.Parse("fixture2.csproj");

            Assert.AreEqual(Guid.Parse("78F5CAF5-81BD-4643-B3FC-FBD706CA2318"), result.ProjectGuid);
        }

        [TestMethod]
        [DeploymentItem("fixture1.csproj")]
        public void Parse_fixture1_SetsProjectNameToFileName()
        {
            var target = new ProjectParser();

            var result = target.Parse("fixture1.csproj");

            Assert.AreEqual("fixture1", result.Name);
        }

        [TestMethod]
        [DeploymentItem("fixture2.csproj")]
        public void Parse_fixture2_SetsProjectNameToFileName()
        {
            var target = new ProjectParser();

            var result = target.Parse("fixture2.csproj");

            Assert.AreEqual("fixture2", result.Name);
        }

        [TestMethod]
        [DeploymentItem("fixture1.csproj")]
        public void Parse_fixture1_SetsProjectPathToFilePath()
        {
            var target = new ProjectParser();

            var result = target.Parse("fixture1.csproj");

            Assert.AreEqual(new FileInfo("fixture1.csproj").FullName, result.Path);
        }

        [TestMethod]
        [DeploymentItem("fixture2.csproj")]
        public void Parse_fixture2_SetsProjectPathToFilePath()
        {
            var target = new ProjectParser();

            var result = target.Parse("fixture2.csproj");

            Assert.AreEqual(new FileInfo("fixture2.csproj").FullName, result.Path);
        }

        [TestMethod]
        [DeploymentItem("fixture1.csproj")]
        public void Parse_fixture1_ParsesAssemblyReferences_Name()
        {
            var target = new ProjectParser();

            var result = target.Parse("fixture1.csproj");

            Assert.AreEqual("Empower.Common", result.AssemblyReferences[0].Name);
            Assert.AreEqual("Empower.Framework", result.AssemblyReferences[1].Name);
            Assert.AreEqual("Newtonsoft.Json", result.AssemblyReferences[2].Name);
            Assert.AreEqual("RabbitMQ.Client", result.AssemblyReferences[3].Name);
        }

        [TestMethod]
        [DeploymentItem("fixture1.csproj")]
        public void Parse_fixture1_ParsesProjectReferences_Name()
        {
            var target = new ProjectParser();

            var result = target.Parse("fixture1.csproj");

            Assert.AreEqual("AcademyMortgage.Empower.Core", result.ProjectReferences[0].Name);
            Assert.AreEqual("AcademyMortgage.Empower.Messaging", result.ProjectReferences[1].Name);
        }

        [TestMethod]
        [DeploymentItem("fixture1.csproj")]
        public void Parse_fixture1_DoesNotIncludeAssemblyReferencesStartingWithSystem()
        {
            var target = new ProjectParser();

            var result = target.Parse("fixture1.csproj");

            Assert.IsFalse(result.AssemblyReferences.Any(c => c.Name.StartsWith("System.")));
        }
    }
}
