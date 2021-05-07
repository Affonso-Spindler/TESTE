using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestePMWEB.Models
{
    [Table("Cons_Clientes")]
    public class Cons_Cliente
    {
        [Key]
        [Range(1, int.MaxValue)]
        public int ID_CLIENTE { get; set; }

        public DateTime DATA_NASCIMENTO { get; set; }

        [MaxLength(2)]
        public string UF { get; set; }
        
        [MaxLength(50)]
        public string CIDADE { get; set; }

        public int? TEMPO_MEDIOCOMPRAS { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public float? FAIXA { get; set; }

        [MaxLength(10)]
        public string TIERS { get; set; }

        public int? QTD_COMPRAS_12M { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public float? LTV { get; set; }

    }
}
