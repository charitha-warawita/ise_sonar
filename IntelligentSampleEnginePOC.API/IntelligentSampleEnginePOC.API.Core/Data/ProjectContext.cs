using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Core.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace IntelligentSampleEnginePOC.API.Core.Data
{
    public class ProjectContext : IProjectContext
    {
        private readonly DatabaseOptions _options;

        public ProjectContext(IOptions<DatabaseOptions> databaseOptions)
        {
            _options = databaseOptions.Value;
        }
        public Project CreateProject(Project project)
        {
            var projectJson = JsonConvert.SerializeObject(project);

            using (SqlConnection connection = new SqlConnection(_options.iseDb))
            {
                using (SqlCommand command = new SqlCommand("[CreateProject]"))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@ProjectJson", projectJson);
                    connection.Open();
                    var projectId = command.ExecuteScalar();
                    if (projectId != null)
                        project.Id = Convert.ToInt64(projectId);

                    connection.Close();
                }
            }
            return project;
        }

        public List<Project> GetProjects(int? status, string? searchString, int? recentCount)        {

            List<Project> projects = new List<Project>();
            using (SqlConnection connection = new SqlConnection(_options.iseDb))
            {
                using (SqlCommand command = new SqlCommand("GetProjects"))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@SearchString", searchString);
                    command.Parameters.AddWithValue("@RecentCount", recentCount);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                           

                            
                        }
                    }
                    connection.Close();
                }
            }
            return projects;
        }
    }
}
