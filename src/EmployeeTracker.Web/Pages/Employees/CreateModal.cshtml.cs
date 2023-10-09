using EmployeeTracker.Employees;
using EmployeeTracker.Heads;
using EmployeeTracker.Leaders;
using EmployeeTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTracker.Web.Pages.Employees
{
    public class CreateModal : EmployeeTrackerPageModel
    {
        [BindProperty]
        public CreateEmployeeDto Employee { get; set; }

        [BindProperty]
        public List<HeadViewModel> Heads { get; set; }

        public List<SelectListItem> HeadList { get; set; }

        private readonly IEmployeeAppService _employeeAppService;
        public CreateModal(IEmployeeAppService employeeAppService)
        {
            _employeeAppService = employeeAppService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ValidateModel();
            
            var selectedHeads = Heads.Where(x => x.IsSelected).ToList();
            if (selectedHeads.Any())
            {
                var headNames = selectedHeads.Select(x => x.Name).ToArray();
                Employee.HeadNames = headNames;
            }

            await _employeeAppService.CreateAsync(Employee);
            return NoContent();
        }

        public async Task OnGetAsync()
        {
            Employee = new CreateEmployeeDto();
            Employee.StartTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, DateTime.Now.Day);
            //Get all authors and fill the select list
            

            //Get all categories
            var headLookupDto = await _employeeAppService.GetHeadLookupAsync();
            HeadList = headLookupDto.Items
                .Select(x => new SelectListItem(x.Name, x.Name)).ToList();
            Heads = ObjectMapper.Map<List<HeadLookupDto>, List<HeadViewModel>>(headLookupDto.Items.ToList());
        }
    }
}
