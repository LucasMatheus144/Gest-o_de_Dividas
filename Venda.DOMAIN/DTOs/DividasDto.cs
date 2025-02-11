using Venda.DOMAIN.Entidades;
using Venda.DOMAIN.Enums;


namespace Vendinha.DOMINIO.DTO
{
    public class DividasDto
    {
        public int Id { get; set; }

        public string Descricao { get; set; } = string.Empty;

        public decimal Valor { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public DateTime Cadastro { get; set; }

        public string? Pagou { get; set; }

        public SituacaoDivida Situacao { get; set; }

        public Cliente? Clientes { get; set; }

    }
}
