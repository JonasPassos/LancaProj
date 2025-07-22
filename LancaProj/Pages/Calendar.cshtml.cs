using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LancaProj.Data;
using LancaProj.Pages.Models;

namespace LancaProj.Pages
{
    public class CalendarModel : PageModel
    {
        private readonly LancaProjContext _context;
        public CalendarModel(LancaProjContext context) => _context = context;

        public List<SelectListItem> ListaFuncionarios { get; set; } = new();
        public IList<Horario> HorariosDoCalendario { get; set; } = new List<Horario>();

        [BindProperty]
        public Horario NovoHorario { get; set; } = new();

        [BindProperty]
        public int FuncionarioSelecionadoId { get; set; }

        public async Task OnGetAsync()
        {
            ListaFuncionarios = await _context.Adm
                .Select(a => new SelectListItem
                {
                    Value = a.ID.ToString(),
                    Text = a.Nome
                }).ToListAsync();

            HorariosDoCalendario = await _context.Horarios
                .Include(h => h.Adm)
                .OrderBy(h => h.DataEvento)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostSaveHorario()
        {
            await OnGetAsync();
            if (!ModelState.IsValid)
                return Page();

            NovoHorario.AdmID = FuncionarioSelecionadoId;
            if (NovoHorario.FunFolga)
            {
                NovoHorario.FunEntrada = null;
                NovoHorario.FunPausa = null;
                NovoHorario.FunVolta = null;
                NovoHorario.FunSaida = null;
            }

            _context.Horarios.Add(NovoHorario);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
