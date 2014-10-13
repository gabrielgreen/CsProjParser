using System;
using System.Xml.Serialization;

namespace CsProjParser.MsBuild
{
    [XmlRoot(Namespace = "http://schemas.microsoft.com/developer/msbuild/2003")]
    public class Project
    {
        [XmlElement(ElementName = "ItemGroup")]
        public ItemGroup[] ItemGroups { get; set; }

        [XmlElement(ElementName = "PropertyGroup")]
        public PropertyGroup[] PropertyGroups { get; set; }
    }

    public class ItemGroup
    {
        [XmlElement(ElementName = "Reference")]
        public Reference[] References { get; set; }

        [XmlElement(ElementName = "ProjectReference")]
        public ProjectReference[] ProjectReferences { get; set; }
    }

    public class Reference
    {
        [XmlAttribute]
        public string Include { get; set; }
    }

    public class PropertyGroup
    {
        [XmlElement]
        public Guid ProjectGuid { get; set; }
    }

    public class ProjectReference
    {
        [XmlAttribute]
        public string Include { get; set; }

        [XmlElement]
        public Guid Project { get; set; }

        [XmlElement]
        public string Name { get; set; }
    }
}
