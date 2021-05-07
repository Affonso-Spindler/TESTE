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

                return Ok(clientes);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar obter os clientes do banco de dados");
            }
        }


        [HttpGet("{id}")]
        public ActionResult<Cliente> Get(int id)
        {
            try
            {
                var cliente = _uof.ClienteRepository.GetById(c => c.ID == id);
                if (cliente == null)
                {
                    return NotFound($"O cliente com id: {id} não foi encontrado");
                }

                return Ok(cliente);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , "Erro ao tentar obter cliente com id:{id} no banco de dados");
            }
        }


        [HttpGet("regiao/{uf}")]
        public ActionResult<Cliente> GetByRegiao(string uf)
        {
            try
            {
                var cliente = _uof.ClienteRepository.GetByRegiao(uf);
                if (cliente == null)
                {
                    return NotFound($"Nenhum cliente encontrado para uf:{uf}");
                }

                return Ok(cliente);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , $"Erro ao tentar obter clientes da uf:{uf} no banco de dados");
            }
        }


        [HttpGet("regiao/{uf}/{cidade}")]
        public ActionResult<Cliente> GetByRegiao(string uf, string cidade)
        {
            try
            {
                var cliente = _uof.ClienteRepository.GetByRegiao(uf, cidade);
                if (cliente == null)
                {
                    return NotFound($"Nenhum cliente encontrado para uf: {uf} e cidade: {cidade}");
                }

                return Ok(cliente);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , $"Erro ao tentar obter clientes da uf: {uf} e cidade: {cidade} no banco de dados");
            }
        }


        [HttpGet("tiers")]
        public ActionResult<Cons_Cliente> GetByTiers([FromQuery] string tier = null)
        {
            try
            {
                var conscliente = _uof.Cons_ClienteRepository.GetByTiers().ToList();

                if (tier != null)
                {
                    conscliente = new List<Cons_Cliente>(conscliente.Where(t => t.TIERS.ToUpper().Equals(tier.ToUpper())));
                }

                return Ok(conscliente);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , $"Erro ao tentar obter clientes por Tiers");
            }
        }


        [HttpGet("faixa")]
        public ActionResult<Cons_Cliente> GetByFaixa([FromQuery] int? ini, int? fim)
        {
            try
            {
                var conscliente = new object();
                if (ini.HasValue && fim.HasValue)
                {
                    conscliente = _uof.Cons_ClienteRepository.GetByFaixa((int)ini, (int)fim);
                }
                else
                {
                    conscliente = _uof.Cons_ClienteRepository.Get().ToList();
                }

                if (conscliente == null)
                {
                    return NotFound($"Nenhum cliente encontrado");
                }

                return Ok(conscliente);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , $"Erro ao tentar obter clientes por Faixa");
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

                return StatusCode(StatusCodes.Status201Created, cliente);
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

                return Ok(cliente);
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

                return Ok(cliente);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar excluir o cliente com id: {id}");
            }
        }


        [HttpGet]

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
                        if (isFirstLine && !(itens[0].Equals("ID") &&
                            itens[1].Equals("EMAIL") &&
                            itens[2].Equals("NOME")))
                        {
                            throw new Exception("O arquivo de importação não corresponde a Clientes");
                        }
                        //pula a primeira linha de cabeçalho
                        if (isFirstLine)
                        {
                            isFirstLine = false;
                            continue;
                        }


                        Cliente newCliente = new Cliente
                        {
                            ID = Convert.ToInt32(itens[0]),
                            EMAIL = itens[1],
                            NOME = itens[2],
                            DATA_NASCIMENTO = DateTime.Parse(itens[3]),
                            CIDADE = itens[6],
                            UF = itens[7],
                            PERMISSAO_RECEBE_EMAIL = Convert.ToInt16(itens[8])
                        };

                        var cliente = _uof.ClienteRepository.GetById(c => c.ID == newCliente.ID);
                        if (cliente != null)
                        {
                            _uof.ClienteRepository.Update(newCliente);
                            _uof.Commit();
                        }
                        else
                        {
                            _uof.ClienteRepository.Add(newCliente);
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
