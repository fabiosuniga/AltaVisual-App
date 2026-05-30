using System.ComponentModel.DataAnnotations;

namespace AltaVisual.Models
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }

    
        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        [Display(Name = "Data do Pedido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime Data { get; set; } = DateTime.Now;

        [Display(Name = "Status do Pedido")]
        public string Status { get; set; } = "Em produção";
        // Relacionamento com os itens
        public List<ProdutoGrafico>? Itens { get; set; } = new();

        [Display(Name = "Total do Pedido")]
        [DataType(DataType.Currency)]
        public decimal ValorTotalPedido => Itens?.Sum(x => x.ValorFinal) ?? 0;
    }
}
