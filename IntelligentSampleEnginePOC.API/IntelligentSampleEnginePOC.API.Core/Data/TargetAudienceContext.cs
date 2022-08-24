using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Core.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Data;
using IntelligentSampleEnginePOC.API.Core.Extensions;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using Microsoft.Extensions.Logging;

namespace IntelligentSampleEnginePOC.API.Core.Data
{
    public class TargetAudienceContext : ITargetAudienceContext
    {
        private readonly DatabaseOptions _options;
        private readonly ILogger<TargetAudienceContext> _logger;

        public TargetAudienceContext(IOptions<DatabaseOptions> databaseOptions, ILogger<TargetAudienceContext> logger)
        {
            _options = databaseOptions.Value;
            _logger = logger;
        }

        public TargetAudience CreateTargetAudience(long projectId, TargetAudience audience)
        {
            var taJson = JsonConvert.SerializeObject(audience);

            using (SqlConnection connection = new SqlConnection(_options.iseDb))
            {
                using (SqlCommand command = new SqlCommand("CreateTargetAudience"))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@ProjectId", Convert.ToString(projectId));
                    command.Parameters.AddWithValue("@AudienceJson", taJson);
                    connection.Open();
                    var tAudienceId = command.ExecuteScalar();
                    if (tAudienceId != null)
                        audience.Id = Convert.ToInt64(tAudienceId);

                    connection.Close();
                }
            }
            return audience;
        }

        public IEnumerable<TargetAudience> GetTargetAudiencesByProjectId(long id)
        {
            using var connection = new SqlConnection(_options.iseDb);
            using var command = new SqlCommand("GetTargetAudiencesByProjectId", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("ProjectId", id);
            connection.Open();

            using var reader = command.ExecuteReader();
            if (!reader.HasRows) return Enumerable.Empty<TargetAudience>();

            var audiences = new List<TargetAudience>();
            while (reader.Read())
            {

                try
                {
                    var audience = new TargetAudience
                    {
                        Id = reader.GetInt64("Id"),
                        Name = reader.GetString("Name"),
                        AudienceNumber = reader.GetInt32("AudienceNumber"),
                        EstimatedIR = reader.GetInt32("EstimatedIR"),
                        EstimatedLOI = reader.GetInt32("EstimatedLOI"),
                        Limit = reader.GetInt32("Limit"),
                        LimitType = reader.GetNullableInt32("LimitType"),
                        TestingUrl = reader.GetNullableString("TestingUrl") ?? string.Empty,
                        LiveUrl = reader.GetNullableString("LiveUrl") ?? string.Empty,
                    };
                    
                    audiences.Add(audience);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    
                    throw;
                }
            };
                
                
            

            return audiences;
        }
    }
}
