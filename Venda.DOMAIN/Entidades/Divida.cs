
using System.ComponentModel.DataAnnotations;
using Venda.DOMAIN.Enums;

namespace Venda.DOMAIN.Entidades
{
    public class Divida
    {
        public int Id { get; set; }

        public string Descricao { get; set; } = String.Empty;

        [Required]
        public decimal Valor {  get; set; }

        public SituacaoDivida Status { get; set; }

        public Cliente ClienteDivida { get; set; }
    }
}
