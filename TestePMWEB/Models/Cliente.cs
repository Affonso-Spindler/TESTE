using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TestePMWEB.Models
{
    [Table("Clientes")]
    public class Cliente
    {
        public Cliente()
        {
            Pedidos = new Collection<Pedido>();
        }

        [Key]
        [Required(ErrorMessage ="Id não informada")]
        [Range(1, int.MaxValue, ErrorMessage = "Id não informada")]
        public int ID { get; set; }


        [Required(ErrorMessage = "O email é obrigatório")]
        [RegularExpression("^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$"
            , ErrorMessage ="Email com formato inválido")]
        [MaxLength(50)]
        public string EMAIL { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [MaxLength(50)]
        public string NOME { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        public DateTime DATA_NASCIMENTO { get; set; }

        //[Required(ErrorMessage = "A cidade é obrigatória")]
        [MaxLength(50)]
        public string CIDADE { get; set; }

        //[Required(ErrorMessage = "A UF é obrigatória")]
        [MaxLength(2, ErrorMessage ="Informe somente 2 letras")]
        public string UF { get; set; }

        [Range(0,1, ErrorMessage ="Valor fora dos limites aceitáveis")]
        [DefaultValue(0)]
        public short PERMISSAO_RECEBE_EMAIL { get; set; }

        public ICollection<Pedido> Pedidos { get; set; }
    }
}
