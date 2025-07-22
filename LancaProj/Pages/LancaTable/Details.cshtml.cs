using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LancaProj.Data;
using LancaProj.Pages.Models;

namespace LancaProj.Pages.LancaTable
{
    public class DetailsModel : PageModel
    {
        private readonly LancaProj.Data.LancaProjContext _context;

        public DetailsModel(LancaProj.Data.LancaProjContext context)
        {
            _context = context;
        }

        public Adm Adm { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adm = await _context.Adm.FirstOrDefaultAsync(m => m.ID == id);

            if (adm is not null)
            {
                Adm = adm;

                return Page();
            }

            return NotFound();
        }
    }
}
