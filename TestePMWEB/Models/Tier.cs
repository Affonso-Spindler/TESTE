using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TestePMWEB.Models
{
    [Table("Tiers")]
    public class Tier
    {

        public int? ValorMin { get; set; }
        public int? ValorMax { get; set; }
        [Key]
        [MaxLength(10)]
        public string Faixa { get; set; }
    }
}