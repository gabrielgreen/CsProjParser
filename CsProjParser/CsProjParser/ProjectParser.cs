using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace CsProjParser
{
    public class ProjectParser
    {
        public Models.Project Parse(string pathToCsProjFile)
        {
            var deserializer = new XmlSerializer(typeof(MsBuild.Project));
            var streamReader = new StreamReader(pathToCsProjFile);

            MsBuild.Project msBuildProject;
            try
            {
                msBuildProject = (MsBuild.Project)deserializer.Deserialize(streamReader);
            }
            catch
            {
                streamReader.Close();
                throw;
            }

            var fileInfo = new FileInfo(pathToCsProjFile);
            var result = new Models.Project
            {
                Name = fileInfo.Name.Substring(0, fileInfo.Name.IndexOf(fileInfo.Extension)),
                ProjectGuid = msBuildProject.PropertyGroups.First(c => c.ProjectGuid != null).ProjectGuid,
                Path = fileInfo.FullName,
                AssemblyReferences =
                    msBuildProject.ItemGroups
                        .Where(c => c.References != null)
                        .SelectMany(c => c.References.Where(d => !d.Include.StartsWith("System.")))
                        .Select(c => new CsProjParser.Models.AssemblyReference
                        {
                            Name = c.Include.Split(',')[0]
                        }).ToList(),
                ProjectReferences =
                    msBuildProject.ItemGroups.Where(c => c.ProjectReferences != null).SelectMany(c => c.ProjectReferences)
                    .Select(c => new CsProjParser.Models.ProjectReference
                    {
                        Name = c.Name
                    }).ToList(),
            };

            return result;
        }
    }
}
