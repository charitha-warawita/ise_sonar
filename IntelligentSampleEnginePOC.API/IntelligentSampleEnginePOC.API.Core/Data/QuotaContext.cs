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
    public class QuotaContext : IQuotaContext
    {
        private readonly DatabaseOptions _options;
        public QuotaContext(IOptions<DatabaseOptions> databaseOptions)
        {
            _options = databaseOptions.Value;
        }

        //public List<Quota> GetQuotaFromDB(long qtid)
        //{
        //    List<Quota> quotas = new List<Quota>();

        //    using (SqlConnection connection = new SqlConnection(_options.iseDb))
        //    {
        //        using (SqlCommand command = new SqlCommand("GetQuota"))
        //        {
        //            command.CommandType = System.Data.CommandType.StoredProcedure;
        //            command.Connection = connection;
        //            command.Parameters.AddWithValue("@qtid", Convert.ToString(qtid));
        //            connection.Open();
        //            SqlDataReader reader = command.ExecuteReader();
        //            if (reader.HasRows)
        //            {
        //                Quota Quota = new Quota();
        //                Question currConditions = new Question();


        //                while (reader.Read())
        //                {
        //                    Quota.Id = Convert.ToInt32(reader[0]);
        //                    Quota.QuotaName = Convert.ToString(reader[1]);
        //                    Quota.QuotaType = Convert.ToString(reader[2]);
        //                    Quota.FieldTarget = Convert.ToInt32(reader[3]);
        //                    Quota.Limit = Convert.ToInt32(reader[4]);
        //                    Quota.Prescreens = Convert.ToInt32(reader[5]);
        //                    Quota.Completes = Convert.ToInt32(reader[6]);
        //                    Quota.IsActive = Convert.ToBoolean(reader[7]);

        //                    if (Quota.Conditions is null && reader[8] is not null)
        //                    {
        //                        Quota.Conditions = new List<Question>();

        //                        currConditions.Id = Convert.ToInt32(reader[8]);
        //                        currConditions.Name = Convert.ToString(reader[9]);
        //                        currConditions.Text = Convert.ToString(reader[10]);
        //                        currConditions.CategoryName = Convert.ToString(reader[11]);

        //                        if (Quota.Conditions.Select(x => x.Variables) == null)
        //                        {
        //                            currConditions.Variables = new List<Variable>();
        //                            currConditions.Variables.Add(
        //                                new Variable
        //                                {
        //                                    Id = Convert.ToInt32(reader[12]),
        //                                    Name = Convert.ToString(reader[13])
        //                                });
        //                            Quota.Conditions.Add(currConditions);
        //                        }
        //                        //Quota.Conditions.
        //                        //Variable currVar = new Variable();
        //                        //currVar.Id = Convert.ToInt32(reader[12]);
        //                        //currVar.Name = Convert.ToString(reader[13]);
        //                        //Variable Variable = (currVar);

        //                    }
        //                    else
        //                    {
        //                        //Quota.Conditions = new List<Question>();

        //                        currConditions.Id = Convert.ToInt32(reader[8]);
        //                        currConditions.Name = Convert.ToString(reader[9]);
        //                        currConditions.Text = Convert.ToString(reader[10]);
        //                        currConditions.CategoryName = Convert.ToString(reader[11]);

        //                        //if (Quota.Conditions.Select(x => x.Variables) == null)
        //                        //{
        //                        currConditions.Variables = new List<Variable>();
        //                        currConditions.Variables.Add(
        //                            new Variable
        //                            {
        //                                Id = Convert.ToInt32(reader[12]),
        //                                Name = Convert.ToString(reader[13])
        //                            });
        //                        Quota.Conditions.Add(currConditions);
        //                        //}
        //                    }
        //                    quotas.Add(Quota);
        //                }

        //                //{
        //                //    if (Quota.Conditions != null)
        //                //    {
        //                //        currConditions.Id = Convert.ToInt32(reader[8]);
        //                //        currConditions.Name = Convert.ToString(reader[9]);
        //                //        currConditions.Text = Convert.ToString(reader[10]);
        //                //        currConditions.CategoryName = Convert.ToString(reader[11]);
        //                //        Quota.Conditions.Add(currConditions);

        //                //        Variable currVar = new Variable();
        //                //        currVar.Id = Convert.ToInt32(reader[12]);
        //                //        currVar.Name = Convert.ToString(reader[13]);
        //                //        Variable Variable = (currVar);

        //                //    }
        //                //}

        //            }
        //        //}
        //        reader.Close();
        //        connection.Close();
        //    }
        //}
        //    return quotas;
        //}

        public List<Quota> GetQuotaFromDB(long qtid)
        {
            List<Quota> quotas = new List<Quota>();

            using (SqlConnection connection = new SqlConnection(_options.iseDb))
            {
                using (SqlCommand command = new SqlCommand("GetQuota"))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@qtid", Convert.ToString(qtid));
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        Quota Quota = new Quota();
                        Question currConditions = new Question();
                        var variableList = new List<Variable>();


                        while (reader.Read())
                        {
                            Quota.Id = Convert.ToInt32(reader[0]);
                            Quota.QuotaName = Convert.ToString(reader[1]);
                            Quota.QuotaType = Convert.ToString(reader[2]);
                            Quota.FieldTarget = Convert.ToInt32(reader[3]);
                            Quota.Limit = Convert.ToInt32(reader[4]);
                            Quota.Prescreens = Convert.ToInt32(reader[5]);
                            Quota.Completes = Convert.ToInt32(reader[6]);
                            Quota.IsActive = Convert.ToBoolean(reader[7]);

                            
                            Quota.Conditions = new List<Question>();

                            currConditions.Id = Convert.ToInt32(reader[8]);
                            currConditions.Name = Convert.ToString(reader[9]);
                            currConditions.Text = Convert.ToString(reader[10]);
                            currConditions.CategoryName = Convert.ToString(reader[11]);

                            currConditions.Variables = new List<Variable>();
                            currConditions.Variables.Add(
                                    new Variable
                                    {
                                        Id = Convert.ToInt32(reader[12]),
                                        Name = Convert.ToString(reader[13])
                                    });

                            variableList.AddRange(currConditions.Variables);
                            Quota.Conditions.Add(currConditions);
                        }
                        quotas.Add(Quota);
                    }
                    reader.Close();
                    connection.Close();
                }
            }
             return quotas;
        }
    }
}
