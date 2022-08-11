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
    public class QualificationContext : IQualificationContext
    {
        private readonly DatabaseOptions _options;

        public QualificationContext (IOptions<DatabaseOptions> databaseOptions)
        {
            _options = databaseOptions.Value;
        }

        public List<Qualification> GetQualificationsFromDB()
        {
            List<Qualification> qualifications = new List<Qualification>();
            using (SqlConnection connection = new SqlConnection(_options.iseDb))
            {
                using (SqlCommand command = new SqlCommand("GetQualifications"))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Qualification qualification = new Qualification();
                            
                            qualification.Id = Convert.ToInt32(reader[0]);
                            qualification.Name = Convert.ToString(reader[1]);
                            qualification.Order = Convert.ToInt32(reader[2]);
                            qualification.LogicalDecision = (string)reader[3];
                            //qualification.NumberOfRequiredConditions = Convert.ToInt32(reader[4]);
                            // qualification.IsActive = Convert.ToBoolean(reader[5]);
                            //if (qualification.Question != null)
                            //{
                                Question currQuestion = new Question();
                                currQuestion.Name = Convert.ToString(reader[6]);
                                qualification.Question = currQuestion;  
                            //}

                            qualifications.Add(qualification);
                        }
                    }
                    reader.Close();
                    connection.Close();
                }
            }
            return qualifications;
        }



        public Qualification CreateQualification(long taid, Qualification qualData)
        {
            var qualJson = JsonConvert.SerializeObject(qualData);

            using (SqlConnection connection = new SqlConnection(_options.iseDb))
            {
                using (SqlCommand command = new SqlCommand("CreateQualification"))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@taid", Convert.ToString(taid));
                    command.Parameters.AddWithValue("@QualificationJson", qualJson);
                    connection.Open();
                    var qualDataId = command.ExecuteScalar();
                    if (qualDataId != null)
                        qualData.Id = Convert.ToInt64(qualDataId);

                    connection.Close();
                }
            }
            return qualData;
        }
    }
}
