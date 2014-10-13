using System;
using System.Collections.Generic;

namespace CsProjParser.Models
{
    public class Project
    {
        public string Name { get; set; }

        public Guid ProjectGuid { get; set; }

        public string Path { get; set; }

        public List<AssemblyReference> AssemblyReferences { get; set; }

        public List<ProjectReference> ProjectReferences { get; set; }
    }
}
