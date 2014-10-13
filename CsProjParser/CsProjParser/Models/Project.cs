using System;
using System.Collections.Generic;

namespace CsProjParser.Models
{
    public class Project : Item
    {
        public Guid ProjectGuid { get; set; }

        public string Path { get; set; }

        public List<Assembly> AssemblyReferences { get; set; }

        public List<Project> ProjectReferences { get; set; }
    }
}
