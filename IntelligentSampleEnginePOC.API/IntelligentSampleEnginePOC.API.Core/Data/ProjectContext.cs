﻿using IntelligentSampleEnginePOC.API.Core.Interfaces;
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
    }
}
