using Venda.DOMAIN.Enums;

namespace Venda.DOMAIN
{
    public class ClienteDto
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Telefone {  get; set; } = string.Empty;

        public string Email {  get; set; } = string.Empty;

        public SituacaoCliente SituacaoCliente { get; set; }

        public int CountDivida { get; set; }
    }
}
