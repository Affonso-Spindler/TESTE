using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestePMWEB.Models
{
    [Table("Pedidos")]
    public class Pedido
    {
        [Key]
        [Required(ErrorMessage = "Id do Pedido não informada")]
        [Range(1, int.MaxValue, ErrorMessage = "Id do Pedido não informada")]
        public int ID_PEDIDO { get; set; }

        
        [Key]
        [ForeignKey("Cliente")]
        [Required(ErrorMessage = "Id do Cliente não informada")]
        [Range(1, int.MaxValue, ErrorMessage = "Id do Cliente não informada")]
        public int ID_CLIENTE { get; set; }

        public virtual Cliente Cliente { get; set; }

        [Key]
        [Required(ErrorMessage = "Id do Produto não informada")]
        [Range(1, int.MaxValue, ErrorMessage = "Id do Produto não informada")]
        public int ID_PRODUTO { get; set; }

        
        [MaxLength(50)]
        public string DEPARTAMENTO { get; set; }

        public int QUANTIDADE { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public float VALOR_UNITARIO { get; set; }

        public int PARCELAS { get; set; }

        [Required]
        public DateTime DATA_PEDIDO { get; set; }

        [MaxLength(50)]
        public string MEIO_PAGAMENTO { get; set; }

        [MaxLength(50)]
        public string STATUS_PAGAMENTO { get; set; }
    }
}
