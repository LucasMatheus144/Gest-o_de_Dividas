using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Venda.DOMAIN.Entidades;
using Venda.DOMAIN.Services;

namespace Venda.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DividaController : ControllerBase
    {
        private readonly DividasService dividaService;
        private readonly IWebHostEnvironment env;

        public DividaController(DividasService dividaService, IWebHostEnvironment env)
        {
            this.dividaService = dividaService;
            this.env = env;
        }

      
        [HttpGet]
        public IActionResult GetDividasCliente(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new
                {
                    StatusCode = 401,
                    Messagem = "Id inserido invalido"
                });
            }

            var consulta = dividaService.ListarDividas(id);

            return Ok(consulta);
        }

        // POST api/<DividaController>
        [HttpPost]
        public IActionResult CadastrarDivida([FromBody] Divida divida)
        {
            if (divida == null)
            {
                return BadRequest(new
                {
                    StatusCode = 401,
                    MenuMsg = "menu.mensagem.CamposObrigatorios",
                    MensagemErro = "Preencher todos os dados obrigatorios"
                });
            }

            try
            {
                var cadastra = dividaService.CriarDividaCliente(divida, out List<ValidationResult> erros);

                if (cadastra)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Mensagem = "Sucesso ao Cadastrar",
                        data = divida
                    });
                }

                return BadRequest(new
                {
                    StatusCode = 400,
                    MenuMsg = "menu.mensagem.ErrorCaptch",
                    MensagemErro = "Error"
                });


            }
            catch (Exception ex) {

                return BadRequest(new
                {
                    StatusCode = 400,
                    MenuMsg = "menu.mensagem.ErrorCaptch",
                    MensagemErro = ex.Message
                });
            }
        }

        // PUT api/<DividaController>/5
        [HttpPut]
        public IActionResult Editar([FromBody] Divida divida)
        {
            if (divida.Id < 0)
            {
                return BadRequest(new
                {
                    StatusCode = 401,
                    ErrorMessage = "menu.mensagem.delete.valor.invalido",
                    Mensagem = "O id esta invalido"
                });
            }

            var edita = dividaService.EditarDividaCliente(divida, out List<ValidationResult> errors);

            if (!edita)
            {
                return BadRequest(new
                {
                    StatusCode = 401,
                    MensagemErro = errors
                });
            }

            return Ok("Cadastro Editado com sucesso");
        }

        // DELETE api/<DividaController>/5
        [HttpDelete]
        public IActionResult Delete([FromBody]Divida divida)
        {
            if (divida.Id < 0)
            {
                return BadRequest(new 
                { 
                    StatusCode = 401,
                    ErrorMessage = "menu.mensagem.delete.valor.invalido",
                    Mensagem = "O id esta invalido"
                });
            }


            var deletar = dividaService.ExcluirDividaCliente(divida, out List<ValidationResult> errors);

            if (!deletar)
            {
                return BadRequest(new
                {
                    StatusCode = 401,
                    MensagemErro = errors
                });
            }

            return Ok("Cadastro Excluido com sucesso");

        }
    }
}
