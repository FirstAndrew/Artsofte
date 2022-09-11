using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Artsofte.Data;
using Artsofte.Models;

namespace Artsofte.Pages
{
    public class EditModel : PageModel
    {
        private readonly Artsofte.Data.ArtsofteContext _context;
        public List<Department> departments { get; set; }

        [BindProperty]
        public Employee Employee { get; set; } = default!;

        public EditModel(Artsofte.Data.ArtsofteContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Employee == null)            
                return NotFound();
            

            if (_context.Department != null)
                departments = _context.Department.ToList();

            var employee =  await _context.Employee.FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)            
                return NotFound();
            
            Employee = employee;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Employee).State = EntityState.Modified;

            var bind = await _context.Binder.FirstOrDefaultAsync(m => m.DepartmentId == id && m.EmployeeId == Employee.Id);     
           
            if (bind == null)
                return NotFound();
            bind.DepartmentId = Employee.Department;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(Employee.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EmployeeExists(int id)
        {
          return (_context.Employee?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
