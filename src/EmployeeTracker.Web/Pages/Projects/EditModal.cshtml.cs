using EmployeeTracker.Leaders;
using EmployeeTracker.Projects;
using EmployeeTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTracker.Web.Pages.Projects
{
    public class EditModal : EmployeeTrackerPageModel
    {
        
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }


        [BindProperty]
        public UpdateProjectDto EditingProject { get; set; }

        [BindProperty]
        public List<LeaderViewModel> Leaders { get; set; }

        

        private readonly IProjectAppService _projectAppService;

        public EditModal(IProjectAppService projectAppService)
        {
            _projectAppService = projectAppService;
        }

        public async Task OnGetAsync()
        {
            var projectDto = await _projectAppService.GetAsync(Id);
            EditingProject = ObjectMapper.Map<ProjectDto, UpdateProjectDto>(projectDto);

            //get all authors

            //get all categories
            var leaderLookupDto = await _projectAppService.GetLeaderLookupAsync();
            Leaders = ObjectMapper.
                Map<List<LeaderLookupDto>, List<LeaderViewModel>>(leaderLookupDto.Items.ToList());

            //mark as Selected for Categories in the book
            if (EditingProject.LeaderNames != null && EditingProject.LeaderNames.Any())
            {
                Leaders
                    .Where(x => EditingProject.LeaderNames.Contains(x.Name))
                    .ToList()
                    .ForEach(x => x.IsSelected = true);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ValidateModel();

            var selectedLeaders = Leaders.Where(x => x.IsSelected).ToList();
            if (selectedLeaders.Any())
            {
                var leaderNames = selectedLeaders.Select(x => x.Name).ToArray();
                EditingProject.LeaderNames = leaderNames;
            }
            await _projectAppService.UpdateAsync(Id, EditingProject);
            return NoContent();
        }
    }
}
