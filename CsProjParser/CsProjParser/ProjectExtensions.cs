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
                new Node<Item> 
                { 
                    Value = new Project { Name = project.Name } 
                });

            tree.Nodes.AddRange(project.AssemblyReferences.Select(c => 
                new Node<Item> 
                { 
                    Value = new Assembly { Name = c.Name } 
                }));

            tree.Nodes.AddRange(project.ProjectReferences.Select(c => 
                new Node<Item> 
                { 
                    Value = new Project { Name = c.Name } 
                }));

            tree.Edges.AddRange(project.AssemblyReferences.Select(c => 
                new Edge<Item> 
                { 
                    Item1 = tree.Nodes.First(d => d.Value.Equals(project)), 
                    Item2 = tree.Nodes.First(d => d.Value.Equals(c)),
                }));

            tree.Edges.AddRange(project.ProjectReferences.Select(c =>
                new Edge<Item>
                {
                    Item1 = tree.Nodes.First(d => d.Value.Equals(project)),
                    Item2 = tree.Nodes.First(d => d.Value.Equals(c)),
                }));

            return tree;
        }
    }
}
