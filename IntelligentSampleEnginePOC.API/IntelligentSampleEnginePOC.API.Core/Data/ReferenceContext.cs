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

        public List<Country> GetCountriesFromDB()
        {
            List<Country> countries = new List<Country>();
            using (SqlConnection connection = new SqlConnection(_options.iseDb))
            {
                using (SqlCommand command = new SqlCommand("GetCountries"))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Country country = new Country();
                            country.Id = Convert.ToInt32(reader[0]);
                            country.Name = Convert.ToString(reader[1]);
                            countries.Add(country);
                        }
                    }
                    reader.Close();
                    connection.Close();
                }
            }
            return countries;
        }

        public List<string> GetProfileCategoriesFromDB()
        {
            List<string> profileCategories = new List<string>();
            using (SqlConnection connection = new SqlConnection(_options.iseDb))
            {
                using (SqlCommand command = new SqlCommand("GetProfileCategories"))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            profileCategories.Add(Convert.ToString(reader[0]));
                        }
                    }
                    reader.Close();
                    connection.Close();
                }
            }
            return profileCategories;
        }

        public List<Question> GetAllQuestionsFromDB()
        {
            return GetQuestionsByCategoryFromDB(String.Empty);
        }

        public List<Question> GetQuestionsByCategoryFromDB(string categoryName)
        {
            List<Question> questions = new List<Question>();
            using (SqlConnection connection = new SqlConnection(_options.iseDb))
            {
                var sp = string.IsNullOrEmpty(categoryName) ? "GetAllQAs" : "GetQAsByCategory";
                using (SqlCommand command = new SqlCommand(sp))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;
                    if(!string.IsNullOrEmpty(categoryName))
                        command.Parameters.AddWithValue("@Category", categoryName);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var currQuestionId = Convert.ToInt32(reader[0]);
                            var currentQuestion = questions.Where(x => x.Id == currQuestionId).FirstOrDefault();
                            if(currentQuestion != null)
                            {
                                Variable currVar = new Variable();
                                currVar.Id = Convert.ToInt32(reader[4]);
                                currVar.Name = Convert.ToString(reader[5]);
                                currentQuestion.Variables.Add(currVar);
                            }
                            else
                            {
                                Question question = new Question();
                                question.Id = Convert.ToInt32(reader[0]);
                                question.Name = Convert.ToString(reader[1]);
                                question.Text = Convert.ToString(reader[2]);
                                question.CategoryName = Convert.ToString(reader[3]);
                                question.Variables = new List<Variable>();

                                Variable currVar = new Variable();
                                currVar.Id = Convert.ToInt32(reader[4]);
                                currVar.Name = Convert.ToString(reader[5]);
                                question.Variables.Add(currVar);

                                questions.Add(question);
                            }
                        }
                    }
                    reader.Close();
                    connection.Close();
                }
            }
            return questions;
        }
    }
}
