using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Core.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Data
{
    public class ReferenceContext : IReferenceContext
    {
        private readonly DatabaseOptions _options;
        public ReferenceContext(IOptions<DatabaseOptions> databaseOptions)
        {
            _options = databaseOptions.Value;
        }

        public List<Category> GetCategoriesFromDB()
        {
            List<Category> categories = new List<Category>();
            using (SqlConnection connection = new SqlConnection(_options.iseDb))
            {
                using (SqlCommand command = new SqlCommand("GetCategories"))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            Category category = new Category();
                            category.Id = Convert.ToInt32(reader[0]);
                            category.Name = Convert.ToString(reader[1]);
                            categories.Add(category);
                        }
                    }
                    reader.Close();
                    connection.Close();
                }
            }
            return categories;
        }
    }
}
