using Venda.DOMAIN.Entidades;
using Venda.DOMAIN.Enums;

namespace Venda.DOMAIN.DTOs
{
    public class ClienteDTO
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string CpfCnpj { get; set; } = string.Empty;

        public string Telefone { get; set; } = string.Empty;

        public int CountDividas { get; set; }

        public SituacaoCliente Status { get; set; }

    }
}
