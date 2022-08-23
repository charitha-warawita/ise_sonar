﻿using IntelligentSampleEnginePOC.API.Core.Interfaces;
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

        public Project? Get(int id)
        {
            using var connection = new SqlConnection(_options.iseDb);
            using var command = new SqlCommand("[GetProject]", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("ProjectId", id);
            connection.Open();

            using var reader = command.ExecuteReader();
            if (!reader.HasRows) return null;
            if (!reader.Read()) return null;
            
            var project = new Project
            {
                Id = long.Parse(reader["Id"].ToString() ?? string.Empty),
                Name = reader["Name"].ToString() ?? string.Empty,
                Reference = reader["Reference"].ToString() ?? string.Empty,
                LastUpdate = DateTime.Parse(reader["LastUpdate"].ToString() ?? string.Empty),
                StartDate = DateTime.Parse(reader["StartDate"].ToString() ?? string.Empty),
                FieldingPeriod = int.Parse(reader["FieldingPeriod"].ToString() ?? string.Empty),
                Status =  Enum.Parse<Status>(reader["Status"].ToString() ?? string.Empty)
            };

            return project;

        }
    }
}
