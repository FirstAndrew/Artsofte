using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Artsofte.Data;
using Artsofte.Models;

namespace Artsofte.Pages
{
    public class CreateModel : PageModel
    {
        private readonly Artsofte.Data.ArtsofteContext _context;

        [BindProperty]
        public Employee Employee { get; set; } = default!;
        public List<Department> departments { get; set; }

        public CreateModel(Artsofte.Data.ArtsofteContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            if (_context.Department != null)
                departments = _context.Department.ToList();

            return Page();
        }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Employee == null || Employee == null)
            {
                return Page();
            }
            Binder bind = new Binder();
            if (_context.Employee.Count() > 0)
                bind.EmployeeId = _context.Employee.OrderBy(y => y.Id).LastOrDefault().Id + 1;
            else
                bind.EmployeeId = 1;
            bind.DepartmentId = Employee.Department;

            _context.Employee.Add(Employee);
            _context.Binder.Add(bind);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
