using System.ComponentModel.DataAnnotations;

namespace AltaVisual.Models
{
    public class Adesivo : ProdutoGrafico
    {
        [Required(ErrorMessage = "Informe a largura em cm")]
        [Range(0.1, 1000, ErrorMessage = "Valor inválido")]
        [Display(Name = "Largura (cm)")]
        public double LarguraCm { get; set; }

        [Required(ErrorMessage = "Informe a altura em cm")]
        [Range(0.1, 1000, ErrorMessage = "Valor inválido")]
        [Display(Name = "Altura (cm)")]
        public double AlturaCm { get; set; }

        [Required(ErrorMessage = "Informe a quantidade")]
        [Range(1, 1000000, ErrorMessage = "A quantidade mínima é 1")]
        [Display(Name = "Qtd. Total")]
        public int Quantidade { get; set; }

        public decimal MetrosQuadradosUsados { get; set; }

        private const double SANGRIA = 0.3;

        public override void CalcularValor()
        {
            
            double ocupacaoL = LarguraCm + SANGRIA;
            double ocupacaoA = AlturaCm + SANGRIA;

            // área de 1 adesivo em cm²
            double areaAdesivoCm2 = ocupacaoL * ocupacaoA;

            // quantos cabem matematicamente em 1m²
            int cabemNoMetro = (int)(10000 / areaAdesivoCm2);

            // margem de segurança
            cabemNoMetro = (int)(cabemNoMetro * 0.90);

            // cabem pelo menos 1 por segurança
            if (cabemNoMetro < 1)
            {
                cabemNoMetro = 1;
            }

            //Calcula quantos metros quadrados serão gastos
            this.MetrosQuadradosUsados = (decimal)(double)Quantidade / cabemNoMetro;

            // Cobrar sempre no mínimo 1m²
            if (this.MetrosQuadradosUsados < 1.0m)
            {
                this.MetrosQuadradosUsados = 1.0m;
            }

            this.ValorFinal = this.MetrosQuadradosUsados * this.PrecoBase;
        }
    }
}
