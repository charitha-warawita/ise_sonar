using System.Data;
using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Core.Model;
using IntelligentSampleEnginePOC.API.Core.Model.Cint;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace IntelligentSampleEnginePOC.API.Core.Data
{
    public class ProjectCintContext : IProjectCintContext
    {
        private readonly DatabaseOptions _options;

        public ProjectCintContext(IOptions<DatabaseOptions> databaseOptions)
        {
            _options = databaseOptions.Value;
        }

        public long StoreProjectCintResponse(bool isSuccess, string statusCode, CintRequestModel request, CintResponse response)
        {
            long savedId = 0;
            if(request != null && response != null)
            {
                var cintRequestJson = JsonConvert.SerializeObject(request.Request);
                var cintResponseJson = JsonConvert.SerializeObject(response);
                using (SqlConnection connection = new SqlConnection(_options.iseDb))
                {
                    using (SqlCommand command = new SqlCommand("[StoreProjectCintResponse]"))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = connection;
                        command.Parameters.AddWithValue("@isSuccess", isSuccess);
                        command.Parameters.AddWithValue("@statusCode", statusCode);
                        command.Parameters.AddWithValue("@projectId", request.ProjectId);
                        command.Parameters.AddWithValue("@taId", request.TargetAudienceId);
                        command.Parameters.AddWithValue("@cintRequestJson", cintRequestJson);
                        command.Parameters.AddWithValue("@cintResponseJson", cintResponseJson);
                        connection.Open();
                        var projectCintResponseId = command.ExecuteScalar();
                        if (projectCintResponseId != null)
                            savedId = Convert.ToInt64(projectCintResponseId);

                        connection.Close();
                    }
                }
            }
            return savedId;
        }

        public async Task<HashSet<long>> GetCintIdsAsync(long id)
        {
            await using var connection = new SqlConnection(_options.iseDb);
            await using var command = new SqlCommand("GetCintIdsForProject", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("ProjectId", id);
            await connection.OpenAsync();

            var results = new HashSet<long>();
            await using var reader = await command.ExecuteReaderAsync();
            if (!reader.HasRows)
                return results;

            while (await reader.ReadAsync())
            {
                var data = reader.GetInt64("CintResponseId");
                results.Add(data);
            }

            return results;
        }
        
        public async Task<List<Link>> GetLinksAsync(long id)
        {
            await using var connection = new SqlConnection(_options.iseDb);
            await using var command = new SqlCommand("GetProjectLinks", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("ProjectId", id);

            var results = new List<Link>();
            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();
            if (!reader.HasRows)
                return results;

            while (await reader.ReadAsync())
            {
                var survey = reader.GetInt64("SurveyId");
                var key = reader.GetString("LinkKey");
                var value = reader.GetString("LinkValue");
                var type = reader.GetString("LinkType");
                
                var link = new Link(survey, key, value, type);
                results.Add(link);
            }

            return results;
        }

        public async Task InsertLinksAsync(long surveyId, long projectId, List<Link> links)
        {
            if (!links.Any())
                return;

            var table = new DataTable
            {
                Columns =
                {
                    { "SurveyId", typeof(long) },
                    { "ProjectId", typeof(long) },
                    { "Type", typeof(string) },
                    { "Key", typeof(string) },
                    { "Value", typeof(string) }
                },
            };

            foreach (var link in links)
            {
                table.Rows.Add(
                    surveyId,
                    projectId,
                    link.Type,
                    link.Key,
                    link.Value);
            }

            await using var connection = new SqlConnection(_options.iseDb);
            await using var command = new SqlCommand("InsertProjectLinks", connection);
            command.CommandType = CommandType.StoredProcedure;

            var param = new SqlParameter("LinksTable", SqlDbType.Structured)
            {
                Value = table,
                TypeName = "dbo.LinkTableType"
            };
            command.Parameters.Add(param);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }
}
