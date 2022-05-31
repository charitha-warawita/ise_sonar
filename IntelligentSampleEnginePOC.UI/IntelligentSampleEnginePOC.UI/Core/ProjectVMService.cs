using IntelligentSampleEnginePOC.UI.Configurations;
using IntelligentSampleEnginePOC.UI.Models;
using IntelligentSampleEnginePOC.UI.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.Text;

namespace IntelligentSampleEnginePOC.UI.Core
{
    public class ProjectVMService : IProjectVMService
    {
        private readonly HttpClient _httpClient;
        private ISEApiSettings _settings;
        public ProjectVMService(HttpClient client, IOptions<ISEApiSettings> options)
        {
            _httpClient = client;
            _settings = options.Value;

        }

        public async Task<ProjectViewModel> GetProjectVM(string status, string searchString)
        {
            List<Status> statusList = EnumHelpers<Status>.GetValues().ToList();
            StringBuilder urlBuilder = new StringBuilder();
            urlBuilder.Append(Path.Combine(_settings.Url, _settings.Path));

            var queryString = new List<string>();
            if (!string.IsNullOrEmpty(status))
                queryString.Add("status="+(int)(EnumHelpers<Status>.Parse(status)));
            if (!string.IsNullOrEmpty(searchString))
                queryString.Add("searchstring="+searchString);

            queryString.Add("recentCount=20");

            if(queryString.Any())
            {
                urlBuilder.Append("?");
                foreach(var item in queryString)
                {
                    urlBuilder.Append(item+"&");
                }
            }    
            HttpResponseMessage response = await _httpClient.GetAsync(urlBuilder.ToString());
            var projectViewModel = new ProjectViewModel();
            projectViewModel.Statuses = new SelectList(statusList);
            if(response.IsSuccessStatusCode)
            {
                var projects = await response.Content.ReadFromJsonAsync<List<Project>>();
                projectViewModel.Projects = projects;
            }

            return projectViewModel;
        }

        public async Task<Project> CreateProject(Project project)
        {
            
            StringBuilder urlBuilder = new StringBuilder();
            urlBuilder.Append(Path.Combine(_settings.Url, _settings.Path));

            return project;
        }
    }
}
