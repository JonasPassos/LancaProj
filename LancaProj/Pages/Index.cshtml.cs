using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LancaProj.Data;
using LancaProj.Pages.Models;
using System.Collections.Generic;
using System.Linq; // Necess�rio para usar .FirstOrDefaultAsync
using System.Threading.Tasks;

namespace LancaProj.Pages
{
    public class IndexModel(LancaProjContext context) : PageModel
    {
        private readonly LancaProjContext _context = context;

        // Lista de funcion�rios para o dropdown
        public List<SelectListItem> ListaFuncionarios { get; set; } = [];

        // ID selecionado do funcion�rio no form
        [BindProperty]
        public int FuncionarioSelecionadoId { get; set; }

        // Novo hor�rio inserido via formul�rio
        [BindProperty]
        public Horario NovoHorario { get; set; } = new();

        // Carrega os dados na p�gina
        public async Task OnGetAsync()
        {
            ListaFuncionarios = await _context.Adm
                .Select(f => new SelectListItem
                {
                    Value = f.ID.ToString(),
                    Text = f.Nome
                })
                .ToListAsync();
        }

        // Processa o POST do formul�rio
        public async Task<IActionResult> OnPostAsync()
        {
            // � uma boa pr�tica recarregar as listas para o caso de o ModelState.IsValid falhar
            // e a p�gina precisar ser renderizada novamente com os dados.
            await OnGetAsync();

            if (!ModelState.IsValid)
            {
                // Se o modelo n�o for v�lido, retorna a p�gina para exibir as mensagens de erro
                return Page();
            }

            var funcionario = await _context.Adm
                // N�o � estritamente necess�rio incluir HorariosRegistrados aqui para ADICIONAR
                // um novo Horario, mas n�o causa problema. Seria �til se voc� fosse, por exemplo,
                // verificar se j� existe um hor�rio para a mesma data.
                .Include(a => a.HorariosRegistrados) // Mantido para consist�ncia e futuras valida��es
                .FirstOrDefaultAsync(a => a.ID == FuncionarioSelecionadoId);

            if (funcionario == null)
            {
                ModelState.AddModelError(string.Empty, "Funcion�rio n�o encontrado.");
                return Page();
            }

            // Garante que a cole��o HorariosRegistrados n�o seja nula antes de adicionar
            if (funcionario.HorariosRegistrados == null)
            {
                funcionario.HorariosRegistrados = new List<Horario>();
            }

            // Associa o NovoHorario ao funcion�rio selecionado
            NovoHorario.AdmID = funcionario.ID;

            // Se o formul�rio do Index.cshtml n�o tiver um campo para NovoHorario.DataEvento,
            // voc� precisar� defini-lo aqui (ex: data atual)
            if (NovoHorario.DataEvento == default)
            {
                NovoHorario.DataEvento = System.DateTime.Today;
            }

            // Se for folga, zera os hor�rios de entrada/sa�da para manter a consist�ncia
            if (NovoHorario.FunFolga)
            {
                NovoHorario.FunEntrada = null;
                NovoHorario.FunPausa = null;
                NovoHorario.FunVolta = null;
                NovoHorario.FunSaida = null;
            }

            // Adiciona o novo objeto Horario � cole��o do funcion�rio
            funcionario.HorariosRegistrados.Add(NovoHorario);

            // O Entity Framework Core detectar� que NovoHorario foi adicionado � cole��o
            // e o adicionar� ao DbSet Horarios automaticamente.
            await _context.SaveChangesAsync();

            // Opcional: Adicionar uma mensagem de sucesso para o usu�rio
            TempData["MensagemSucesso"] = "Hor�rio/Folga registrado com sucesso!";

            return RedirectToPage();
        }
    }
}