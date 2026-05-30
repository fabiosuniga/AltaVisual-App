using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AltaVisual.Models
{
    public abstract class ProdutoGrafico
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(100, ErrorMessage = "A descrição deve ter no máximo 100 caracteres")]
        [Display(Name = "Descrição do Item")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informe o preço base por m²")]
        [Display(Name = "Preço por m² (R$)")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecoBase { get; set; }

        [Display(Name = "Valor Final (R$)")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorFinal { get; protected set; }

        public abstract void CalcularValor();
    }
}
