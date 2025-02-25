using NHibernate;
using NHibernate.Criterion;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.Marshalling;
using Venda.DOMAIN.Entidades;
using Venda.DOMAIN.Enums;
using Venda.DOMAIN.Services;
using Vendinha.DOMINIO.DTO;
using static NHibernate.Engine.Query.CallableParser;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Venda.DOMAIN.Services { 

    public class DividasService
    {
        private readonly ISessionFactory _session;

        public DividasService(ISessionFactory session)
        {
            this._session = session;
        }

        public bool CriarDividaCliente(Divida divida, out List<ValidationResult> errors)
        {
            errors = new List<ValidationResult>();

            if (!ValidacaoService.Validar(divida, out errors)) return false;

            using var sessao = _session.OpenSession();
            using var salvar = sessao.BeginTransaction();

            var cliente = sessao.Get<Cliente>(divida.Cliente.Id);
            if (cliente == null)
            {
                errors.Add(new ValidationResult("Cliente nao encontrado.", new[] { nameof(Divida.Cliente) }));
                return false;
            }

            // validar o valor da divida superior ao parametrizado

            if (!ValidaValorDivida(divida.Cliente.Id))
            {
                errors.Add(new ValidationResult($"O cliente possui dividas superiores ao limite ", new[] { nameof(Divida.Valor) }));
                return false;
            }

            divida.Status = SituacaoDivida.EmDia;

            try
            {
                sessao.Save(divida);
                salvar.Commit();
                return true;
            }
            catch (Exception ex)
            {
                salvar.Rollback();
                TratarRetornoErros(ex, errors);
                return false;
            }
        }

        public bool EditarDividaCliente(Divida divida, out List<ValidationResult> errors)
        {
            errors = new List<ValidationResult>();

            if (!ValidacaoService.Validar(divida, out errors)) return false;

            if (divida.Cliente.Id <= 0)
            {
                errors.Add(new ValidationResult("Id invalido.", new[] { nameof(Divida.Cliente) }));
                return false;
            }

            using var sessao = _session.OpenSession();
            using var salvar = sessao.BeginTransaction();

            var cliente = sessao.Get<Cliente>(divida.Cliente.Id);
            if (cliente == null)
            {
                errors.Add(new ValidationResult("Cliente n�o encontrado.", new[] { nameof(Divida.Cliente) }));
                return false;
            }

            var DividaExistente = sessao.Get<Divida>(divida.Id);

            
            if( DividaExistente.Valor != divida.Valor)
            {
                if (ValidaValorDivida(divida.Cliente.Id))
                {
                    errors.Add(new ValidationResult($"O cliente ja possui muitas dividas ", new[] { nameof(Divida.Valor) }));
                    return false;
                }
            }

            try
            {
                sessao.Merge(divida);
                salvar.Commit();
                return true;
            }
            catch (Exception ex)
            {
                salvar.Rollback();
                TratarRetornoErros(ex, errors);
                return false;
            }
        }

        public bool ExcluirDividaCliente(Divida divida, out List<ValidationResult> errors)
        {
            errors = new List<ValidationResult>();

            if (!ValidacaoService.Validar(divida, out errors)) return false;

            if (divida.Cliente.Id <= 0)
            {
                errors.Add(new ValidationResult("Id invalido.", new[] { nameof(Divida.Cliente) }));
                return false;
            }

            using var sessao = _session.OpenSession();
            using var salvar = sessao.BeginTransaction();

            var cliente = sessao.Get<Cliente>(divida.Cliente.Id);
            if (cliente == null)
            {
                errors.Add(new ValidationResult("Cliente nao encontrado.", new[] { nameof(Divida.Cliente) }));
                return false;
            }

            try
            {
                sessao.Delete(divida);
                salvar.Commit();
                return true;
            }
            catch (Exception ex)
            {
                salvar.Rollback();
                TratarRetornoErros(ex, errors);
                return false;
            }
        }

        public List<DividasDto> ListarDividas(int id)
        {
            using var sessao = _session.OpenSession();

            var query = sessao.Query<Divida>()
                .Where(d => d.Cliente.Id == id)
                .Select(d => new DividasDto
                {
                    Id = d.Id,
                    Descricao = d.Descricao,
                    Valor = d.Valor,
                    Cadastro = d.DataCadastro,
                    Pagou = d.DataPagamento.HasValue
                        ? d.DataPagamento.Value.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
                        : "Não Pago",
                    Clientes = d.Cliente
                })
                .ToList();

            return query;
        }

        private bool ValidaValorDivida(int id)
        {
            using var sessao = _session.OpenSession();

            var resultado = sessao.CreateSQLQuery("SELECT vendinha.valida_newdivida(:id)")
                                  .SetParameter("id", id)
                                  .UniqueResult<bool>();

            return resultado;
        }

        private void TratarRetornoErros(Exception ex, List<ValidationResult> erros)
        {
            var message = ex.InnerException?.Message ?? ex.Message;

            if (message.Contains("is not mapped"))
            {
                erros.Add(new ValidationResult("Ajusta esse xml ai"));
            }
            else if (message.Contains("O cliente já bateu o limite de dívidas."))
            {
                erros.Add(new ValidationResult("O cliente já bateu o limite de dívidas"));
            }
            else
            {
                erros.Add(new ValidationResult("Erro ao processar a opera��o"));
            }
        }
    }

}