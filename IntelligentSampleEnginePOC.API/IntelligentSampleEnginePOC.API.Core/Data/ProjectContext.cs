using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Core.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Data;
using Microsoft.Extensions.Logging;
using IntelligentSampleEnginePOC.API.Core.Helpers;

namespace IntelligentSampleEnginePOC.API.Core.Data
{
    public class ProjectContext : IProjectContext
    {
        private readonly DatabaseOptions _options;
        private readonly ILogger<ProjectContext> _logger;
        public ProjectContext(IOptions<DatabaseOptions> databaseOptions, ILogger<ProjectContext> logger)
        {
            _options = databaseOptions.Value;
            _logger = logger;
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

        public ProjectList GetProjects(int? status,int pageNumber, string? searchString, int recentCount)        
        {
            ProjectList projectList = new ProjectList();
            List<Project> projects = new List<Project>();
            var isTotalCountRecorded = false;
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
                            projects.Add(project);                         
                            
                            if(!isTotalCountRecorded)
                            {
                                projectList.TotalItems = reader.IsDBNull("RCount") ? 0: (long)reader["RCount"];
                                isTotalCountRecorded = true;
                            }
                        }
                        projectList.Projects = projects;
                    }
                    connection.Close();
                }
            }
            return projectList;
        }

        public long UpdateProjectStatus(long projectId, Status status)
        {
            long result = 0;
            using (SqlConnection connection = new SqlConnection(_options.iseDb))
            {
                using (SqlCommand command = new SqlCommand("[UpdateProjectStatus]"))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@projectId", projectId);
                    command.Parameters.AddWithValue("@status", status);
                    connection.Open();
                    var resultId = command.ExecuteScalar();
                    if (resultId != null)
                        result = Convert.ToInt64(resultId);

                    connection.Close();

                }
            }
            return result;
        }

        public Project? Get(long id)
        {
            using var connection = new SqlConnection(_options.iseDb);
            using var command = new SqlCommand("[GetProject]", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("ProjectId", id);
            connection.Open();

            using var reader = command.ExecuteReader();
            if (!reader.HasRows) return null;
            if (!reader.Read()) return null;

            Project project;

            try
            {
                project = new Project
                {
                    Id = reader.GetInt64("Id"),
                    Name = reader.GetString("Name"),
                    Reference = reader.GetString("Reference"),
                    LastUpdate = reader.GetDateTime("LastUpdate"),
                    StartDate = reader.GetDateTime("StartDate"),
                    FieldingPeriod = reader.GetInt32("FieldingPeriod"),
                    Status = EnumHelper.SafeParse<Status>(reader.GetInt32("Status"))
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                
                throw;
            }

            return project;

        }
    }
}
