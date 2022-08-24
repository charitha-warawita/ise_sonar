using IntelligentSampleEnginePOC.API.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Tests.MockModelData
{
    public class Questions
    {
        public List<Question> GetTestQuestions(string categoryName)
        {
            var questions = new List<Question>();
          
            questions.Add(new Question()
            {
                Id = 23,
                Name = "Flights - purpose of travel",
                Text = "For which  do you travel by plane?",
                CategoryName = "Travel",
                Variables = new List<Variable>() { new Variable() { Id = 158, Name = "Business" } }
            });
            questions.Add(new Question()
            {
                Id = 12,
                Name = "Personal Income Classification",
                Text = "What is your personal income, before tax?",
                CategoryName = "Finance",
                Variables = new List<Variable>() { new Variable() { Id = 261,Name = "Very Low" } }
            });
            if (string.IsNullOrEmpty(categoryName))
                return questions;
            else {
                var questionItem = questions.FirstOrDefault<Question>(q => q.CategoryName == categoryName);
                if (questionItem != null)
                    return new List<Question>() { questionItem };
                else
                    return null;
            }
        }
    }
}
