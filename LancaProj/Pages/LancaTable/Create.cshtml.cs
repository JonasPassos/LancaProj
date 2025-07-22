using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LancaProj.Data;
using LancaProj.Pages.Models;

namespace LancaProj.Pages.LancaTable
{
    public class CreateModel : PageModel
    {
        private readonly LancaProj.Data.LancaProjContext _context;

        public CreateModel(LancaProj.Data.LancaProjContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }
        
       

        [BindProperty]
        public Adm Adm { get; set; } = default!;
        
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Adm.Dia = DateTime.UtcNow;
            _context.Adm.Add(Adm);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
