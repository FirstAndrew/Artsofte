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
    public class DepartamentsModel : PageModel
    {
        private readonly Artsofte.Data.ArtsofteContext _context;

        public DepartamentsModel(Artsofte.Data.ArtsofteContext context)
        {
            _context = context;
        }

        public IList<Binder> Binder { get;set; } = default!;
        public IList<Department> Department { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Employee != null)
            {
                Binder = await _context.Binder.ToListAsync();
                Department = await _context.Department.ToListAsync();
            }
        }
    }
}
