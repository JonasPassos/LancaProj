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
    public class IndexModel : PageModel
    {
        private readonly LancaProj.Data.LancaProjContext _context;

        public IndexModel(LancaProj.Data.LancaProjContext context)
        {
            _context = context;
        }

        public IList<Adm> Adm { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Adm = await _context.Adm.ToListAsync();
        }
    }
}
