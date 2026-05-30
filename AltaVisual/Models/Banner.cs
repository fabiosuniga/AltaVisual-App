using System.ComponentModel.DataAnnotations;

namespace AltaVisual.Models
{
    public class Banner : ProdutoGrafico
    {
        [Required(ErrorMessage = "Informe a largura")]
        [Range(0.01, 100, ErrorMessage = "A largura deve ser entre 0.01m e 100m")]
        [Display(Name = "Largura (m)")]
        public double Largura { get; set; }

        [Required(ErrorMessage = "Informe a altura")]
        [Range(0.01, 100, ErrorMessage = "A altura deve ser entre 0.01m e 100m")]
        [Display(Name = "Altura (m)")]
        public double Altura { get; set; }

        public int Quantidade { get; set; } = 1;

        [Display(Name = "Tipo de Acabamento")]
        public string Acabamento { get; set; }

        public override void CalcularValor()
        {
            decimal area = (decimal)(Largura * Altura);
            // Regra do 1m² mínimo
            decimal areaCobrada = area < 1.0m ? 1.0m : area;
            this.ValorFinal = (decimal)area * this.PrecoBase * this.Quantidade;
        }
    }
}
