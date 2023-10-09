using EmployeeTracker.Leaders;
using EmployeeTracker.Projects;
using EmployeeTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTracker.Web.Pages.Projects
{
    public class CreateModal : EmployeeTrackerPageModel
    {
        [BindProperty]
        public CreateProjectDto Project { get; set; }

        [BindProperty]
        public List<LeaderViewModel> Leaders { get; set; }



        private readonly IProjectAppService _projectAppService;
        public CreateModal(IProjectAppService projectAppService)
        {
            _projectAppService = projectAppService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ValidateModel();

            var selectedLeaders = Leaders.Where(x => x.IsSelected).ToList();
            if (selectedLeaders.Any())
            {
                var leaderNames = selectedLeaders.Select(x => x.Name).ToArray();
                Project.LeaderNames = leaderNames;
            }

            await _projectAppService.CreateAsync(Project);
            return NoContent();
        }

        public async Task OnGetAsync()
        {
            Project = new CreateProjectDto();

            //Get all authors and fill the select list


            //Get all categories
            var leaderLookupDto = await _projectAppService.GetLeaderLookupAsync();
            Leaders = ObjectMapper.Map<List<LeaderLookupDto>, List<LeaderViewModel>>(leaderLookupDto.Items.ToList());
        }
    }
}
