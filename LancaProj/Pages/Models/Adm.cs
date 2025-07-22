using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LancaProj.Pages.Models
{
    public class Adm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [DisplayName("Funcionário")]
        [Required(ErrorMessage = "Nome é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        [DisplayName("Função")]
        [Required(ErrorMessage = "Função é obrigatória.")]
        public string Funcao { get; set; } = string.Empty;

        [DisplayName("Entrada")]
        [DataType(DataType.Date)]
        public DateTime Dia { get; set; }

        // Relação 1:N com Horarios
        public ICollection<Horario> HorariosRegistrados { get; set; } = new List<Horario>();
    }

    public class Horario
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "A data do evento é obrigatória.")]
        [DataType(DataType.Date)]
        [DisplayName("Data do Evento")]
        public DateTime DataEvento { get; set; }

        [DataType(DataType.Time)]
        [DisplayName("Entrada")]
        public TimeSpan? FunEntrada { get; set; }

        [DataType(DataType.Time)]
        [DisplayName("Pausa")]
        public TimeSpan? FunPausa { get; set; }

        [DataType(DataType.Time)]
        [DisplayName("Volta")]
        public TimeSpan? FunVolta { get; set; }

        [DataType(DataType.Time)]
        [DisplayName("Saída")]
        public TimeSpan? FunSaida { get; set; }

        [DisplayName("Folga")]
        public bool FunFolga { get; set; } = false;

        [Required(ErrorMessage = "A cor do evento é obrigatória.")]
        public string Cor { get; set; } = "amarelo";

        // Chave estrangeira
        [Required]
        public int AdmID { get; set; }

        [ForeignKey("AdmID")]
        public Adm Adm { get; set; } = null!;
    }
}
