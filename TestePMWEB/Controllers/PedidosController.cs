using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TestePMWEB.Models;
using TestePMWEB.Repository;

namespace TestePMWEB.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[Controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly IUnitOfWork _uof;

        public PedidosController(IUnitOfWork contexto)
        {
            _uof = contexto;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Pedido>> Get()
        {
            try
            {
                var pedidos = _uof.PedidoRepository.Get().ToList();

                return pedidos;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar obter os pedidos do banco de dados");
            }
        }


        [HttpGet("{id}/{idCliente}", Name = "ObterPedido")]
        public ActionResult<Pedido> Get(int id, int idCliente)
        {
            try
            {
                var pedido = _uof.PedidoRepository.GetById(p => p.ID_PEDIDO == id && p.ID_CLIENTE == idCliente);
                if (pedido == null)
                {
                    return NotFound($"O pedido com id: {id} não foi encontrado");
                }

                return pedido;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar obter pedido com id:{id} no banco de dados");
            }
        }


        [HttpPost]
        public ActionResult Post([FromBody] Pedido pedido)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _uof.PedidoRepository.Add(pedido);
                _uof.Commit();

                return new CreatedAtRouteResult("ObterPedido", new { id = pedido.ID_PEDIDO }, pedido);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar cadastrar um novo pedido");
            }

        }


        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Pedido pedido)
        {
            try
            {
                if (id != pedido.ID_PEDIDO)
                {
                    return BadRequest($"Não foi possível atualizar o pedido com id: {id}");
                }

                _uof.PedidoRepository.Update(pedido);
                _uof.Commit();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $" Erro ao tentar atualizar o pedido com id: {id}");
            }

        }


        [HttpDelete("{id}")]
        public ActionResult<Pedido> Delete(int id)
        {
            try
            {
                var pedido = _uof.PedidoRepository.GetById(p => p.ID_PEDIDO == id);

                if (pedido == null)
                {
                    return NotFound($"O pedido com id: {id} não foi encontrado");
                }

                _uof.PedidoRepository.Delete(pedido);
                _uof.Commit();

                return pedido;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar excluir o pedido com id: {id}");
            }
        }
    }
}
