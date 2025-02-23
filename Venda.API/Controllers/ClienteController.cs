using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Venda.DOMAIN.Entidades;
using Venda.DOMAIN.Services;


namespace Venda.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService clienteService;
        private readonly IWebHostEnvironment env;

        public ClienteController(ClienteService clienteService, IWebHostEnvironment env)
        {
            this.clienteService = clienteService;
            this.env = env;
        }

        [HttpGet]
        public IActionResult GetListarCliente(string? Nome, int? id_cliente, int page= 0, int size = 6)
        {
            try
            {
                if ( page < 0 && size <= 0){
                    return BadRequest(
                        new
                        {
                            messagem = "Os valores limite e pagina nao podem ser menores de zero",
                            statusCode = 401
                        });
                }

                var clientes = clienteService.ListarCliente(id_cliente, Nome, size, page);

                return Ok(new
                {
                    statusCode = 200,
                    data = clientes,
                });

            }
            catch(Exception ex)
            {
                return StatusCode(404, new
                {
                    messagem = "Erro ao processar. ",
                    error = ex.Message,
                    statuscode = 404

                });
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Cliente cliente)
        {
            if (cliente  == null)
            {
                return BadRequest(new
                {
                    messagem = "Preencha os campos obrigatorios",
                    StatusCode = 401
                });
            }

            
            var criar = clienteService.CriarCliente(cliente, out List<ValidationResult> erros);

            if (criar)
            {
                return Ok("Cadastro realizado com sucesso");
            }

            return BadRequest(new
            {
                mensagem = "Erro ao cadastrar",
                StatusCode = 401,
                error = erros
            });

             
        }

        [HttpPut]
        public IActionResult Put([FromBody] Cliente cliente)
        {
            if (cliente.Id < 0)
            {
                return BadRequest(new
                {
                    messagem = "Id invalido",
                    StatusCode = 401
                });
            }

            var editar = clienteService.EditarCliente(cliente, out List<ValidationResult> erros);

            if (editar)
            {
                return Ok("Cadastro editado com sucesso");
            }

            return BadRequest(new
            {
                error = erros,
                statuscode = 404
            });

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id < 0)
            {
                return BadRequest(new
                {
                    messagem = "Id invalido",
                    StatusCode = 401
                });
            }

            var exclui = clienteService.ExcluirCliente(id, out List<ValidationResult> erros);

            if (exclui)
            {
                return Ok("Cadastro excluido com sucesso");
            }

            return BadRequest(new
            {
                error = erros,
                statuscode = 404
            });
        }
    }
}
