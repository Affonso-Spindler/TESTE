using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestePMWEB.Models
{
    [Table("Pedidos")]
    public class Pedido
    {
        [Key]
        public int ID_PEDIDO { get; set; }

        [Key]
        public int ID_CLIENTE { get; set; }
        public Cliente CLIENTE { get; set; }
        
        [Key]
        public int ID_PRODUTO { get; set; }
        
        [MaxLength(50)]
        public string DEPARTAMENTO { get; set; }

        public int QUANTIDADE { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(8, 3)")]
        [Range(0,1,ErrorMessage ="Valor diferente de 0 ou 1")]
        public decimal VALOR_UNITARIO { get; set; }

        public int PARCELAS { get; set; }

        [Required]
        public DateTime DATA_PEDIDO { get; set; }

        [MaxLength(50)]
        public string MEIO_PAGAMENTO { get; set; }

        [MaxLength(50)]
        public string STATUS_PAGAMENTO { get; set; }
    }
}
