using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Core.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Data
{
    public class TargetAudienceContext : ITargetAudienceContext
    {
        private readonly DatabaseOptions _options;

        public TargetAudienceContext(IOptions<DatabaseOptions> databaseOptions)
        {
            _options = databaseOptions.Value;
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
    }
}
