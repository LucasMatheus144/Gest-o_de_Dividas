using NHibernate;
using System.ComponentModel.DataAnnotations;
using Venda.DOMAIN.Entidades;
using Venda.DOMAIN.DTOs;
using Venda.DOMAIN.Enums;

namespace Venda.DOMAIN.Services { 

    public class ClienteService
    {
        private readonly ISessionFactory _session;

        public ClienteService(ISessionFactory session)
        {
            this._session = session;
        }

        public bool CriarCliente(Cliente clientes, out List<ValidationResult> erros)
        {

            erros = new List<ValidationResult>();

            if (!ValidacaoService.Validar(clientes, out erros)) return false;

            using var sessao = _session.OpenSession();
            using var salvar = sessao.BeginTransaction();

            try
            {
                int IdadeMinima = ValidaIdade(1); //validar parametrochavevalor    1,IDADE MINIMA,18,true

                if (IdadeMinima > (DateTime.Now.Year - clientes.DataNascimento.Year))
                {
                    throw new InvalidOperationException($"Idade Invalida");
                }

                sessao.Save(clientes);
                salvar.Commit();
                return true;
            }
            catch (Exception ex)
            {
                salvar.Rollback();
                TratarRetornoErros(ex, erros);
                return false;
            }
        }

        public bool EditarCliente(Cliente clientes, out List<ValidationResult> erros)
        {
            erros = new List<ValidationResult>();

            if (!ValidacaoService.Validar(clientes, out erros)) return false;

            using var sessao = _session.OpenSession();
            using var transaction = sessao.BeginTransaction();

            var EncontrarCliente = sessao.Get<Cliente>(clientes.Id);

            if (EncontrarCliente == null)
            {
                erros.Add(new ValidationResult("Cliente nao localizado no sistema.", new[] { nameof(Cliente.Id) }));
                return false;
            }

            if ((DateTime.Now.Year - clientes.DataNascimento.Year) < ValidaIdade(1))
            {
                erros.Add(new ValidationResult("Idade alterada Invalida.", new[] { nameof(Cliente.DataNascimento) }));
                return false;
            }

            try
            {
                sessao.Merge(clientes);
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                TratarRetornoErros(ex, erros);
                return false;
            }
        }

        public bool ExcluirCliente(int id, out List<ValidationResult> erros)
        {
            erros = new List<ValidationResult>();
            using var sessao = _session.OpenSession();
            using var transaction = sessao.BeginTransaction();

            var EncontraCliente = sessao.Get<Cliente>(id);

            if (EncontraCliente == null)
            {
                erros.Add(new ValidationResult("Cliente nao localizado no sistema.", new[] { nameof(Cliente.Id) }));
                return false;
            }

            //var possuiDividas = sessao.Query<Dividas>().Any(d => EncontraCliente.Id == d.Cliente.Id);

            var possuiDividas = sessao.QueryOver<Divida>()
             .Where(d => d.Cliente.Id == EncontraCliente.Id)
             .RowCount() > 0;

            if (possuiDividas)
            {
                erros.Add(new ValidationResult("O cliente possui dividas.", new[] { nameof(Divida.Id) }));
                return false;
            }
            try
            {
                sessao.Delete(EncontraCliente);
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                TratarRetornoErros(ex, erros);
                return false;
            }
        }

        public List<ClienteDTO> ListarCliente(int? id, string? search = "", int limit = 10, int offset = 0)
        {
            using var sessao = _session.OpenSession();

            var query = sessao.Query<Cliente>()
             .Where(cl =>
                 (id == null || cl.Id == id) &&
                 (string.IsNullOrWhiteSpace(search) || cl.Nome.Contains(search)))
             .GroupJoin(sessao.Query<Divida>(),
                 cl => cl.Id,
                 d => d.Cliente.Id,
                 (cl, dividas) => new ClienteDTO
                 {
                     Id = cl.Id,
                     Nome = cl.Nome,
                     Email = cl.Email,
                     CpfCnpj = cl.CpfCnpj,
                     Telefone = cl.Telefone,
                     CountDividas = dividas.Count(d => d.Status != SituacaoDivida.EmDia) 
                 });

            return query
                .OrderBy(cl => cl.Nome)
                .Skip(offset)
                .Take(limit)
                .ToList();


       
        }

        private int ValidaIdade(int id)
        {
            using var sessao = _session.OpenSession();

            var parametro = sessao.Query<ParametroChaveValor>()
                              .FirstOrDefault(p => p.Id == id);

            if (parametro == null)
            {
                throw new Exception($"Parametro nao encontrado.");
            }

            return parametro.Valor;
        }

        private void TratarRetornoErros(Exception ex, List<ValidationResult> erros)
        {
            var message = ex.InnerException?.Message ?? ex.Message;

            if (message.Contains("CPF ou CNPJ"))
            {
                erros.Add(new ValidationResult("Cpf ou Cnpj Invalidos.", new[] { nameof(Cliente.CpfCnpj) }));
            }
            else if (message.Contains("Idade Invalida"))
            {
                erros.Add(new ValidationResult("Cliente n�o possui a idade minima", new[] { nameof(Cliente.DataNascimento) }));
            }
            else if (message.Contains("clientes_email_key"))
            {
                erros.Add(new ValidationResult("Esse email ja existe no sistema", new[] { nameof(Cliente.Email) }));
            }
            else if (message.Contains("Possui Dividas"))
            {
                erros.Add(new ValidationResult("O cliente possui dividas.", new[] { nameof(Cliente.Id) }));
            }
            else if (message.Contains("is not mapped"))
            {
                erros.Add(new ValidationResult("Ajusta esse xml ai"));
            }
            else
            {
                erros.Add(new ValidationResult("Erro ao processar a opera��o"));
            }
        }
    }

}