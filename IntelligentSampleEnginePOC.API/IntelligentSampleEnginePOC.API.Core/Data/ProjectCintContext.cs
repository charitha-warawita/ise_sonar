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
                        command.CommandType = System.Data.CommandType.StoredProcedure;
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
    }
}
