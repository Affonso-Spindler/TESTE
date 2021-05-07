using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestePMWEB.Models
{
    [Table("CONS_RFV")]
    public class Cons_RFV
    {
        [Key]
        public int ID_CLIENTE { get; set; }

        public DateTime? DATA_ULTIMA_COMPRA { get; set; }
        
        [MaxLength(30)]
        public string ULTIMO_DEPTO_COMPRA { get; set; }
        
        [MaxLength(20)]
        public string PARCELAMENTO_PREFER { get; set; }
        
        [MaxLength(30)]
        public string MEIO_PAGAMENTO_PREFER { get; set; }
        
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(8, 3)")]
        public decimal? TICKET_MEDIO_ALL { get; set; }
        
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(8, 3)")]
        public decimal? TICKET_MEDIO_12M { get; set; }
        
        public int? FREQUENCIA_COMPRA_ALL { get; set; }
        
        public int? FREQUENCIA_COMPRA_12M { get; set; }
        
        public string TIER_ATUAL { get; set; }
    }
}
