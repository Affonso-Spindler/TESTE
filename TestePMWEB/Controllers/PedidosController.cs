using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TestePMWEB.Filters;
using TestePMWEB.Models;
using TestePMWEB.Repository;

namespace TestePMWEB.Controllers
{
    [ServiceFilter(typeof(LoggingFilter))]
    [Authorize]
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

                return Ok(pedidos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar obter os pedidos do banco de dados");
            }
        }


        [HttpGet("{id}")]
        public ActionResult<Pedido> Get(int id)
        {
            try
            {
                var pedido = _uof.PedidoRepository.GetById(p => p.ID_PEDIDO == id);
                if (pedido == null)
                {
                    return NotFound($"O pedido com id: {id} não foi encontrado");
                }

                return Ok(pedido);
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

                return StatusCode(StatusCodes.Status201Created, pedido);
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

                return Ok(pedido);
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

                return Ok(pedido);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar excluir o pedido com id: {id}");
            }
        }


        [AllowAnonymous]
        [Route("EnviarArquivo")]
        [HttpPost]
        public ActionResult EnviarArquivo(IFormFile arquivo)
        {
            try
            {
                if (arquivo == null)
                {
                    throw new Exception("Nenhum arquivo carregado");
                }
                StringBuilder text = new StringBuilder();

                using (StreamReader reader = new StreamReader(arquivo.OpenReadStream()
                    , Encoding.UTF8, false))
                {
                    while (reader.Peek() >= 0)
                    {
                        text.AppendLine(reader.ReadLine());
                    }
                    bool isFirstLine = true;

                    foreach (string line in text.ToString().Split(Environment.NewLine.ToCharArray()))
                    {
                        if (string.IsNullOrEmpty(line))
                        {
                            continue;
                        }
                        string[] itens = line.Split('\t');

                        //valida o começo do cabeçalho do arquivo
                        if (isFirstLine && !(itens[0].Equals("COD_CLIENTE") &&
                            itens[1].Equals("COD_PEDIDO") &&
                            itens[2].Equals("CODIGO_PRODUTO")))
                        {
                            throw new Exception("O arquivo de importação não corresponde a Pedidos");
                        }
                        //pula a primeira linha de cabeçalho
                        if (isFirstLine)
                        {
                            isFirstLine = false;
                            continue;
                        }


                        Pedido newPedido = new Pedido
                        {
                            ID_CLIENTE = Convert.ToInt32(itens[0]),
                            ID_PEDIDO = Convert.ToInt32(itens[1]),
                            ID_PRODUTO = Convert.ToInt32(itens[2]),
                            DEPARTAMENTO = itens[3],
                            QUANTIDADE = Convert.ToInt32(itens[4]),
                            VALOR_UNITARIO = Convert.ToDecimal(itens[5]),
                            PARCELAS = Convert.ToInt32(itens[6]),
                            DATA_PEDIDO = DateTime.Parse(itens[7]),
                            MEIO_PAGAMENTO = itens[8],
                            STATUS_PAGAMENTO = itens[9]
                        };

                        var pedido = _uof.PedidoRepository.GetById(p => p.ID_PEDIDO == newPedido.ID_PEDIDO
                                                                        && p.ID_CLIENTE == newPedido.ID_CLIENTE
                                                                        && p.ID_PRODUTO == newPedido.ID_PRODUTO);
                        if (pedido != null)
                        {
                            _uof.PedidoRepository.Update(newPedido);
                            _uof.Commit();
                        }
                        else
                        {
                            _uof.PedidoRepository.Add(newPedido);
                        }

                    }
                }
                return StatusCode(StatusCodes.Status200OK, "Dados inseridos com sucesso");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
        }
    }
}
