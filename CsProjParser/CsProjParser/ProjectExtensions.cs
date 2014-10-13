using CsProjParser.Models;
using System;
using System.Linq;

namespace CsProjParser
{
    public static class ProjectExtensions
    {
        public static Tree<Item> ToDependencyTree(this Project project)
        {
            var tree = new Tree<Item>();

            tree.Nodes.Add(
                new Project
                {
                    Name = project.Name
                });

            tree.Nodes.AddRange(project.AssemblyReferences.Select(c =>
                new Assembly
                {
                    Name = c.Name
                }));

            tree.Nodes.AddRange(project.ProjectReferences.Select(c =>
                new Project
                {
                    Name = c.Name
                }));

            tree.Edges.AddRange(project.AssemblyReferences.Select(c => Tuple.Create<Item, Item>(project, c)));

            tree.Edges.AddRange(project.ProjectReferences.Select(c => Tuple.Create<Item, Item>(project, c)));

            return tree;
        }
    }
}
