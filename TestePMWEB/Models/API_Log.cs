using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestePMWEB.Models
{
    [Table("API_Logs")]
    public class API_Log
    {
        //Sequencial de controle das mensagens
        [Key]
        public int ID_MENSAGEM { get; set; }


        //Timestamp de referência a data do request
        public long DATA_REFERENCIA { get; set; }


        //Requisição feita para dados de Clientes ou Pedidos
        public string TIPO { get; set; }


        //1 - Sucesso / 0 - Erro
        public short RESULTADO { get; set; }


        //Retorno da chamada utilizando HTTP Status Code
        public int DETALHE { get; set; }


        //Mensagens com tratamento de status code
        public string MENSAGEM { get; set; }

    }
}
