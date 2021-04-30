using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestePMWEB.Models
{
    [Table("Clientes")]
    public class Cliente
    {
        [Key]
        public int ID { get; set; }


        [Required(ErrorMessage = "O email é obrigatório")]
        [RegularExpression("",ErrorMessage ="Email com formato inválido")]
        public string EMAIL { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string NOME { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        public DateTime DATA_NASCIMENTO { get; set; }

        [Required(ErrorMessage = "A cidade é obrigatória")]
        public string CIDADE { get; set; }

        [Required(ErrorMessage = "A UF é obrigatória")]
        public string UF { get; set; }

        public short PERMISSAO_RECEBE_EMAIL { get; set; }
    }
}
