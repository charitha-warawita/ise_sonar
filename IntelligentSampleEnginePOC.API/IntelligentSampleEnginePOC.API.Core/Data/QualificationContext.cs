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

        public List<Qualification> GetQualificationsFromDB(long qid)
        {
            List<Qualification> qualifications = new List<Qualification>();
            
            using (SqlConnection connection = new SqlConnection(_options.iseDb))
            {
                using (SqlCommand command = new SqlCommand("GetQualifications"))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@qid", Convert.ToString(qid));
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        Qualification qualification = new Qualification();
                        Question currQuestion = new Question();
                        while (reader.Read())
                        {
                            if (qualification.Question is null)
                            {
                                qualification.Id = Convert.ToInt32(reader[0]);
                                qualification.Name = Convert.ToString(reader[1]);
                                qualification.Order = Convert.ToInt32(reader[2]);
                                qualification.LogicalDecision = (string)reader[3];
                                //qualification.NumberOfRequiredConditions = Convert.ToInt32(reader[4]);
                                // qualification.IsActive = Convert.ToBoolean(reader[5]);
                                    currQuestion.Id = Convert.ToInt32(reader[6]);
                                    currQuestion.Name = Convert.ToString(reader[7]);
                                    currQuestion.Text = Convert.ToString(reader[8]);
                                    currQuestion.CategoryName = Convert.ToString(reader[9]);

                                    currQuestion.Variables = new List<Variable>();
                                    qualification.Question = currQuestion;

                                        if (qualification.Question != null)
                                        {
                                            Variable currVar = new Variable();
                                            currVar.Id = Convert.ToInt32(reader[10]);
                                            currVar.Name = Convert.ToString(reader[11]);
                                            Variable Variable = (currVar);
                                            currQuestion.Variables.Add(Variable);
                                        }
                               qualifications.Add(qualification);
                            }
                            else
                            {
                                if (qualification.Question != null)
                                {
                                   Variable currVar = new Variable();
                                    currVar.Id = Convert.ToInt32(reader[10]);
                                    currVar.Name = Convert.ToString(reader[11]);
                                    Variable Variable = (currVar);
                                    currQuestion.Variables.Add(Variable);
                                }
                            }
                            
                        }
                    }
                    reader.Close();
                    connection.Close();
                }
            }
            return qualifications;
        }
        


        public Qualification CreateQualification(long projectId ,long taid, Qualification qualData)
        {
            var qualJson = JsonConvert.SerializeObject(qualData);

            using (SqlConnection connection = new SqlConnection(_options.iseDb))
            {
                using (SqlCommand command = new SqlCommand("CreateQualification"))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@projectId", Convert.ToString(projectId));
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




        public long DeleteQualificationFromDB(long qid)
        {
            using (SqlConnection connection = new SqlConnection(_options.iseDb))
            {
                using (SqlCommand command = new SqlCommand("DeleteQualification"))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@qid", Convert.ToString(qid));
                    connection.Open();
                    var deletedQId = command.ExecuteNonQuery();
                    connection.Close();
                    return deletedQId;
                }
            }
            
        }
    }
}
