using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Artsofte.Data;
using Artsofte.Models;

namespace Artsofte.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly Artsofte.Data.ArtsofteContext _context;

        public DeleteModel(Artsofte.Data.ArtsofteContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Employee Employee { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Employee == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FirstOrDefaultAsync(m => m.Id == id);

            if (employee == null)
            {
                return NotFound();
            }
            else 
            {
                Employee = employee;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Employee == null)
            {
                return NotFound();
            }
            var employee = await _context.Employee.FindAsync(id);
            var bind = await _context.Binder.FirstOrDefaultAsync(b => b.EmployeeId == Employee.Id);

            if (employee != null && bind != null)
            {
                Employee = employee;
                _context.Employee.Remove(Employee);
                _context.Binder.Remove(bind);
                

                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
