using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Core.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Data
{
    public class QuotaContext : IQuotaContext
    {
        private readonly DatabaseOptions _options;
        public QuotaContext(IOptions<DatabaseOptions> databaseOptions)
        {
            _options = databaseOptions.Value;
        }
        public Quota GetQuota(long qtid)
        {
            Quota quotas = new Quota();
            quotas.Conditions = new List<Question>();

            using (SqlConnection connection = new SqlConnection(_options.iseDb))
            {
                using (SqlCommand command = new SqlCommand("GetQuota"))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@qtid", Convert.ToString(qtid));
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    if (dt.Rows.Count > 0)
                    {
                        var list = dt.AsEnumerable().Where(row => row["Id"].ToString() == qtid.ToString());

                        quotas.Id = Convert.ToInt32(list.FirstOrDefault()["Id"]);
                        quotas.QuotaName = list.FirstOrDefault()["QuotaName"].ToString();
                        quotas.QuotaType = list.FirstOrDefault()["QuotaType"].ToString();
                        quotas.FieldTarget = Convert.ToInt32(list.FirstOrDefault()["FieldTarget"]);
                        quotas.Limit = Convert.ToInt32(list.FirstOrDefault()["Limit"]);
                        quotas.Prescreens = Convert.ToInt32(list.FirstOrDefault()["Prescreens"]);
                        quotas.Completes = Convert.ToInt32(list.FirstOrDefault()["Completes"]);
                        quotas.IsActive = Convert.ToBoolean(list.FirstOrDefault()["IsActive"]);

                        var groupedByValueCodeText = from row in dt.AsEnumerable()
                                               group row by new
                                               {
                                                   QuestionId = row.Field<long>("QuestionId"),
                                                   QuestionName = row.Field<string>("QuestionName"),
                                                   QuestionText = row.Field<string>("QuestionText"),
                                                   QuestionCategory = row.Field<string>("QuestionCategory"),
                                                   ValueCode = row.Field<int>("ValueCode"),
                                                   ValueText = row.Field<string>("ValueText"),

                                               } into valueResults
                                               select new
                                               {
                                                   QuestionId = valueResults.Key.QuestionId,
                                                   QuestionName = valueResults.Key.QuestionName,
                                                   QuestionText = valueResults.Key.QuestionText,
                                                   QuestionCategory = valueResults.Key.QuestionCategory,
                                                   ValueCode = valueResults.Key.ValueCode,
                                                   ValueText = valueResults.Key.ValueText,
                                               };

                        var quesSet = groupedByValueCodeText.GroupBy(x => new 
                        { x.QuestionId, x.QuestionName,x.QuestionText,x.QuestionCategory });


                        foreach (var result in quesSet)
                        {
                            Question currConditions = new Question();
                            currConditions.Id = Convert.ToInt32(result.Key.QuestionId);
                            currConditions.Name = result.Key.QuestionName;
                            currConditions.Text = result.Key.QuestionText;
                            currConditions.CategoryName = result.Key.QuestionCategory;

                            var valueSet = groupedByValueCodeText.Where(x => x.QuestionId == currConditions.Id &&
                                                                             x.QuestionName == currConditions.Name &&
                                                                             x.QuestionText == currConditions.Text &&
                                                                             x.QuestionCategory == currConditions.CategoryName)
                                                                             .Select(y => new {y.ValueCode,y.ValueText});
                            if(valueSet!=null)
                            {
                                currConditions.Variables = new List<Variable>();
                               
                                foreach (var value in valueSet)
                                {
                                    Variable variableObject = new Variable();
                                    variableObject.Id = Convert.ToInt32(value.ValueCode);
                                    variableObject.Name= value.ValueText;
                                    currConditions.Variables.Add(variableObject);
                                }
                            }
                            quotas.Conditions.Add(currConditions);
                        }
                    }
                }

                connection.Close();
            }
            
            return quotas;
        }

        public Quota CreateQuota(long projectId, long taid, Quota qtaData)
        {
                var quotaJson = JsonConvert.SerializeObject(qtaData);

                using (SqlConnection connection = new SqlConnection(_options.iseDb))
                {
                    using (SqlCommand command = new SqlCommand("CreateQuota"))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Connection = connection;
                        command.Parameters.AddWithValue("@TAID", taid);
                        command.Parameters.AddWithValue("@projectId", projectId);
                        command.Parameters.AddWithValue("@QuotaJson", quotaJson);
                        connection.Open();
                        var quotaDataId = command.ExecuteScalar();
                        if (quotaDataId != null)
                            qtaData.Id = Convert.ToInt64(quotaDataId);
                        connection.Close();
                    }
                }
           
            return qtaData;
        }

        public long DeleteQuotaFromDB(long qtid)
        {
            using (SqlConnection connection = new SqlConnection(_options.iseDb))
            {
                using (SqlCommand command = new SqlCommand("DeleteQuota"))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@qtid", Convert.ToString(qtid));
                    connection.Open();
                    var deletedQtId = command.ExecuteNonQuery();
                    connection.Close();
                    return deletedQtId;
                }
            }

        }
    }
}
