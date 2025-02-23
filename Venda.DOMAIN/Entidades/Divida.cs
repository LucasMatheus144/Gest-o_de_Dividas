
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

        private DateTime _dataCadastro;

        public DateTime DataCadastro
        {
            get => _dataCadastro;
            set => _dataCadastro = DateTime.Now;
        }

        public DateTime? DataPagamento { get; set; }

        private SituacaoDivida _status;

        public SituacaoDivida Status 
        {
            get => _status;
            set => _status = SituacaoDivida.EmDia;
        
        }

        public Cliente Cliente { get; set; }
    }
}
