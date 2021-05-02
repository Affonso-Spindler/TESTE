using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestePMWEB.Filters;
using TestePMWEB.Models;
using TestePMWEB.Repository;

namespace TestePMWEB.Controllers
{
    [ServiceFilter(typeof(LoggingFilter))]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[Controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IUnitOfWork _uof;

        public ClientesController(IUnitOfWork contexto)
        {
            _uof = contexto;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Cliente>> Get()
        {
            try
            {
                var clientes = _uof.ClienteRepository.Get().ToList();

                return clientes;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar obter os clientes do banco de dados");
            }
        }


        [HttpGet("{id}", Name = "ObterCliente")]
        public ActionResult<Cliente> Get(int id)
        {
            try
            {
                var cliente = _uof.ClienteRepository.GetById(c => c.ID == id);
                if (cliente == null)
                {
                    return NotFound($"O cliente com id: {id} não foi encontrado");
                }

                return cliente;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar obter cliente com id:{id} no banco de dados");
            }
        }


        [HttpPost]
        public ActionResult Post([FromBody] Cliente cliente)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _uof.ClienteRepository.Add(cliente);
                _uof.Commit();

                return new CreatedAtRouteResult("ObterCliente", new { id = cliente.ID }, cliente);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar cadastrar um novo cliente");
            }

        }


        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Cliente cliente)
        {
            try
            {
                if (id != cliente.ID)
                {
                    return BadRequest($"Não foi possível atualizar o cliente com id: {id}");
                }

                _uof.ClienteRepository.Update(cliente);
                _uof.Commit();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $" Erro ao tentar atualizar o cliente com id: {id}");
            }

        }


        [HttpDelete("{id}")]
        public ActionResult<Cliente> Delete(int id)
        {
            try
            {
                var cliente = _uof.ClienteRepository.GetById(c => c.ID == id);

                //Verifica se o cliente existe
                if (cliente == null)
                {
                    return NotFound($"O cliente com id: {id} não foi encontrado");
                }

                _uof.ClienteRepository.Delete(cliente);
                _uof.Commit();

                return cliente;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar excluir o cliente com id: {id}");
            }
        }



        [AllowAnonymous]
        [Route("EnviarArquivo")]
        [HttpPost]
        public void EnviarArquivo(IFormFile arquivo)
        {
            StringBuilder text = new StringBuilder();
            
            using(StreamReader reader = new StreamReader(arquivo.OpenReadStream()))
            {
                while(reader.Peek() >= 0)
                {
                    text.AppendLine(reader.ReadLine());
                }
            }

        }
    }
}
