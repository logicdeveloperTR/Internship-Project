using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using EmployeeTracker.Employees;
using EmployeeTracker.Web.Models;
using EmployeeTracker.Leaders;
using EmployeeTracker.Heads;

namespace EmployeeTracker.Web.Pages.Employees
{
    public class EditModal : EmployeeTrackerPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public UpdateEmployeeDto EditingEmployee { get; set; }

        [BindProperty]
        public List<HeadViewModel> Heads { get; set; }

        public List<SelectListItem> HeadList { get; set; }

        private readonly IEmployeeAppService _employeeAppService;

        public EditModal(IEmployeeAppService employeeAppService)
        {
            _employeeAppService = employeeAppService;
        }

        public async Task OnGetAsync()
        {
            var employeeDto = await _employeeAppService.GetAsync(Id);
            EditingEmployee = ObjectMapper.Map<EmployeeDto, UpdateEmployeeDto>(employeeDto);

            //get all authors
            
            //get all categories
            var headLookupDto = await _employeeAppService.GetHeadLookupAsync();
            Heads = ObjectMapper.
                Map<List<HeadLookupDto>, List<HeadViewModel>>(headLookupDto.Items.ToList());
            HeadList = headLookupDto.Items
                .Select(x => new SelectListItem( x.Name, x.Name)).ToList();
            //mark as Selected for Categories in the book
            if (EditingEmployee.HeadNames != null && EditingEmployee.HeadNames.Any())
            {
                Heads
                    .Where(x => EditingEmployee.HeadNames.Contains(x.Name))
                    .ToList()
                    .ForEach(x => x.IsSelected = true);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ValidateModel();
            var selectedHeads = Heads.Where(x => x.IsSelected).ToList();
            if (selectedHeads.Any())
            {
                var headNames = selectedHeads.Select(x => x.Name).ToArray();
                EditingEmployee.HeadNames = headNames;
            }

            await _employeeAppService.UpdateAsync(Id, EditingEmployee);
            return NoContent();
        }
    }
}
