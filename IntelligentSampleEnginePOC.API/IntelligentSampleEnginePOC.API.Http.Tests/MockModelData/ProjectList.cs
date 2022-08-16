using IntelligentSampleEnginePOC.API.Core.Model;


namespace IntelligentSampleEnginePOC.API.Http.Tests.MockModelData
{
    public class ProjectList
    {
        public List<Project> GetTestProjectList(int? status, int pageNumber, string? searchString, int recordCount)
        {
            var projects = new List<Project>();
            
            projects.Add(new Project()
            {
                Id = 1,
                Name = "Test1",
                Reference = "Survey Test",
                User = new User() { Id = 47, Name = "Test", Email = "ASD@sdfsf.com" },
                LastUpdate = Convert.ToDateTime("2022-08-09"),
                StartDate = Convert.ToDateTime("2022-08-09"),
                FieldingPeriod = 10,
                Status = (Status)2
            });
            projects.Add(new Project()
            {
                Id = 2,
                Name = "Test2",
                Reference = "Survey Test",
                User = new User() { Id = 12, Name = "Test", Email = "sdfsd@sdfsf.com" },
                LastUpdate = Convert.ToDateTime("2022-08-09"),
                StartDate = Convert.ToDateTime("2022-08-09"),
                FieldingPeriod = 60,
                Status = (Status)2
            });
            projects.Add(new Project()
            {
                Id = 3,
                Name = "Test1",
                Reference = "Survey Test",
                User = new User() { Id = 1, Name = "Test", Email = "sdfsd@sdfsf.com" },
                LastUpdate = Convert.ToDateTime("2022-08-09"),
                StartDate = Convert.ToDateTime("2022-08-09"),
                FieldingPeriod = 60,
                Status = (Status)0
            });
            projects.Add(new Project()
            {
                Id = 4,
                Name = "Test3",
                Reference = "Survey Test",
                User = new User() { Id = 1, Name = "Test", Email = "sdfsd@sdfsf.com" },
                LastUpdate = Convert.ToDateTime("2022-08-09"),
                StartDate = Convert.ToDateTime("2022-08-09"),
                FieldingPeriod = 60,
                Status = (Status)0
            });
            projects.Add(new Project()
            {
                Id = 5,
                Name = "Test2",
                Reference = "Survey Test",
                User = new User() { Id = 1, Name = "Test", Email = "sdfsd@sdfsf.com" },
                LastUpdate = Convert.ToDateTime("2022-08-09"),
                StartDate = Convert.ToDateTime("2022-08-09"),
                FieldingPeriod = 60,
                Status = (Status)3
            });
            projects.Add(new Project()
            {
                Id = 6,
                Name = "Test3",
                Reference = "Survey Test",
                User = new User() { Id = 1, Name = "Test", Email = "sdfsd@sdfsf.com" },
                LastUpdate = Convert.ToDateTime("2022-08-09"),
                StartDate = Convert.ToDateTime("2022-08-09"),
                FieldingPeriod = 60,
                Status = (Status)4
            });
            if (string.IsNullOrEmpty(searchString) && status is null)
            {
                if (projects != null)
                {
                    projects = GetPage(projects, pageNumber, recordCount);
                    return projects;
                }
                else
                    return null;
            }            
            else if (!string.IsNullOrEmpty(searchString) && status is null)
                projects = projects.FindAll(q => q.Name == searchString);
            else if (string.IsNullOrEmpty(searchString) && status is not null)
                projects = projects.FindAll(q => q.Status == (Status)status);
            else
                projects = projects.FindAll(q => q.Name == searchString && q.Status == (Status)status);

            if (projects != null)
                return projects;
            else
                return null;
           
        }

        List<Project> GetPage(List<Project> list, int page, int pageSize)
        {
            return list.Skip(page - 1 * pageSize).Take(pageSize).ToList();
           
        }
    }
}

