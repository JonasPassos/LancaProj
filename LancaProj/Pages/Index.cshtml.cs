using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LancaProj.Data;
using LancaProj.Pages.Models;
using System.Collections.Generic;
using System.Linq; // Necessário para usar .FirstOrDefaultAsync
using System.Threading.Tasks;

namespace LancaProj.Pages
{
    public class IndexModel(LancaProjContext context) : PageModel
    {
        private readonly LancaProjContext _context = context;

        // Lista de funcionários para o dropdown
        public List<SelectListItem> ListaFuncionarios { get; set; } = [];

        // ID selecionado do funcionário no form
        [BindProperty]
        public int FuncionarioSelecionadoId { get; set; }

        // Novo horário inserido via formulário
        [BindProperty]
        public Horario NovoHorario { get; set; } = new();

        // Carrega os dados na página
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

        // Processa o POST do formulário
        public async Task<IActionResult> OnPostAsync()
        {
            // É uma boa prática recarregar as listas para o caso de o ModelState.IsValid falhar
            // e a página precisar ser renderizada novamente com os dados.
            await OnGetAsync();

            if (!ModelState.IsValid)
            {
                // Se o modelo não for válido, retorna a página para exibir as mensagens de erro
                return Page();
            }

            var funcionario = await _context.Adm
                // Não é estritamente necessário incluir HorariosRegistrados aqui para ADICIONAR
                // um novo Horario, mas não causa problema. Seria útil se você fosse, por exemplo,
                // verificar se já existe um horário para a mesma data.
                .Include(a => a.HorariosRegistrados) // Mantido para consistência e futuras validações
                .FirstOrDefaultAsync(a => a.ID == FuncionarioSelecionadoId);

            if (funcionario == null)
            {
                ModelState.AddModelError(string.Empty, "Funcionário não encontrado.");
                return Page();
            }

            // Garante que a coleção HorariosRegistrados não seja nula antes de adicionar
            if (funcionario.HorariosRegistrados == null)
            {
                funcionario.HorariosRegistrados = new List<Horario>();
            }

            // Associa o NovoHorario ao funcionário selecionado
            NovoHorario.AdmID = funcionario.ID;

            // Se o formulário do Index.cshtml não tiver um campo para NovoHorario.DataEvento,
            // você precisará defini-lo aqui (ex: data atual)
            if (NovoHorario.DataEvento == default)
            {
                NovoHorario.DataEvento = System.DateTime.Today;
            }

            // Se for folga, zera os horários de entrada/saída para manter a consistência
            if (NovoHorario.FunFolga)
            {
                NovoHorario.FunEntrada = null;
                NovoHorario.FunPausa = null;
                NovoHorario.FunVolta = null;
                NovoHorario.FunSaida = null;
            }

            // Adiciona o novo objeto Horario à coleção do funcionário
            funcionario.HorariosRegistrados.Add(NovoHorario);

            // O Entity Framework Core detectará que NovoHorario foi adicionado à coleção
            // e o adicionará ao DbSet Horarios automaticamente.
            await _context.SaveChangesAsync();

            // Opcional: Adicionar uma mensagem de sucesso para o usuário
            TempData["MensagemSucesso"] = "Horário/Folga registrado com sucesso!";

            return RedirectToPage();
        }
    }
}