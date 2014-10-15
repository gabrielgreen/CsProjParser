using System;
using System.Collections.Generic;

namespace CsProjParser.Models
{    
    public class Item
    {
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            return ((Item)obj).Name == Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }

    public class Project : Item
    {
        public Guid ProjectGuid { get; set; }

        public string Path { get; set; }

        public List<Assembly> AssemblyReferences { get; set; }

        public List<Project> ProjectReferences { get; set; }
    }


    public class Assembly : Item
    {
    }
}
