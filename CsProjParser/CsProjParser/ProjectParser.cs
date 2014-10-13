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

            var result = GetProject(msBuildProject, new FileInfo(pathToCsProjFile));

            return result;
        }

        private static Models.Project GetProject(MsBuild.Project msBuildProject, FileInfo csProjFileInfo)
        {
            var project = new Models.Project
            {
                Name = csProjFileInfo.Name.Substring(0, csProjFileInfo.Name.IndexOf(csProjFileInfo.Extension)),

                ProjectGuid = msBuildProject.PropertyGroups.First(c => c.ProjectGuid != null).ProjectGuid,

                Path = csProjFileInfo.FullName,

                AssemblyReferences =
                    msBuildProject.ItemGroups
                        .Where(c => c.References != null)
                        .SelectMany(c => c.References.Where(d => !(d.Include.StartsWith("System.") || d.Include == "System")))
                        .Select(c => new CsProjParser.Models.Assembly
                        {
                            Name = c.Include.Split(',')[0]
                        }).ToList(),

                ProjectReferences =
                    msBuildProject.ItemGroups.Where(c => c.ProjectReferences != null).SelectMany(c => c.ProjectReferences)
                    .Select(c => new CsProjParser.Models.Project
                    {
                        Name = c.Name
                    }).ToList(),
            };

            return project;
        }
    }
}
