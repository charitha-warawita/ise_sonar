using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Core.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Data;

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

        public List<Project> GetProjects(int? status,int pageNumber, string? searchString, int recentCount)        {

            List<Project> projects = new List<Project>();
            using (SqlConnection connection = new SqlConnection(_options.iseDb))
            {
                using (SqlCommand command = new SqlCommand("GetProjects"))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@PageNumber", pageNumber);
                    command.Parameters.AddWithValue("@SearchString", searchString);
                    command.Parameters.AddWithValue("@RecentCount", recentCount);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Project project = new Project();
                            project.Id = reader.IsDBNull("Id") ? 0 : (long)reader["Id"];
                            project.Name = reader.IsDBNull("Name") ? string.Empty : reader.GetString("Name");
                            project.Reference= reader.IsDBNull("Reference") ? string.Empty : (string)reader["Reference"];
                            project.User = new User();
                            if (!string.IsNullOrEmpty(reader["UserId"].ToString()))
                            {                          
                                project.User.Id = reader.IsDBNull("UserId") ? 0 : (long)reader["UserId"];
                                project.User.Name = reader.IsDBNull("UserName") ? string.Empty : (string)reader["UserName"];
                                project.User.Email = reader.IsDBNull("UserEmail") ? string.Empty : (string)reader["UserEmail"];
                            }
                            project.LastUpdate = reader.IsDBNull("LastUpdate") ? default : (DateTime)reader["LastUpdate"];
                            project.StartDate = reader.IsDBNull("StartDate") ? default : (DateTime)reader["StartDate"];
                            project.FieldingPeriod = reader.IsDBNull("FieldingPeriod") ? 0 : (int)reader["FieldingPeriod"];
                            project.Status = (Status)(int)reader["Status"];                           
                            project.TestingUrl = reader.IsDBNull("TestingUrl") ? string.Empty : (string)reader["TestingUrl"];
                            project.LiveUrl = reader.IsDBNull("LiveUrl") ? string.Empty : (string)reader["LiveUrl"];
                            projects.Add(project);                           
                        }
                    }
                    connection.Close();
                }
            }
            return projects;
        }
    }
}
