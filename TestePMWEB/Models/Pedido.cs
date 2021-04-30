using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestePMWEB.Models
{
    [Table("Pedidos")]
    public class Pedido
    {
        [Key]
        public int ID_CLIENTE { get; set; }
        [Key]
        public int ID_PEDIDO { get; set; }
        [Key]
        public int ID_PRODUTO { get; set; }

        public string DEPARTAMENTO { get; set; }

        public int QUANTIDADE { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(8, 3)")]
        [Range(0,1,ErrorMessage ="Valor diferente de 0 ou 1")]
        public decimal VALOR_UNITARIO { get; set; }

        public int PARCELAS { get; set; }

        public DateTime DATA_PEDIDO { get; set; }

        public string MEIO_PAGAMENTO { get; set; }

        public string STATUS_PAGAMENTO { get; set; }
    }
}
