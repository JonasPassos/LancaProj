using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LancaProj.Data;
using LancaProj.Pages.Models;

namespace LancaProj.Pages.LancaTable
{
    public class EditModel : PageModel
    {
        private readonly LancaProj.Data.LancaProjContext _context;

        public EditModel(LancaProj.Data.LancaProjContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Adm Adm { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adm =  await _context.Adm.FirstOrDefaultAsync(m => m.ID == id);
            if (adm == null)
            {
                return NotFound();
            }
            Adm = adm;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Adm).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdmExists(Adm.ID))
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

        private bool AdmExists(int id)
        {
            return _context.Adm.Any(e => e.ID == id);
        }
    }
}
