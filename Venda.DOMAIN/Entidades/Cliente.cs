using System.ComponentModel.DataAnnotations;
using Venda.DOMAIN.Enums;
using static Venda.DOMAIN.Services.ValidacaoService;

namespace Venda.DOMAIN.Entidades
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [MinLength(3, ErrorMessage = "O campo Nome deve ter no minimo 3 caracteres.")]
        [MaxLength(25, ErrorMessage = "O campo Nome deve ter no minimo 25 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [MaxLength(30,ErrorMessage = "O emails precisa ter no maximo 30 caracteres")]
        [ValidaEmail]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string CpfCnpj { get; set; } = string.Empty;

        public DateTime DataNascimento { get; set; }

        public DateTime DataCadastro {get ;set;} = DateTime.Now;

        [Required] 
        public string Telefone { get; set; } = string.Empty;

        public string Observacao { get; set; } = string.Empty;

        public SituacaoCliente Status { get; set; }
    }
}
