using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AltaVisual.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O Nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CPF/CNPJ é obrigatório.")]
        [Display(Name = "CPF / CNPJ")]
        [StringLength(18, MinimumLength = 14, ErrorMessage = "O documento deve estar completo (CPF 14 ou CNPJ 18 caracteres).")]
        public string CpfCnpj { get; set; }

        [Required(ErrorMessage = "O Telefone/Celular é obrigatório.")]
        [Display(Name = "Telefone / Celular")]
        [StringLength(15, MinimumLength = 14, ErrorMessage = "O telefone deve estar completo com DDD.")]
        public string Telefone { get; set; }

        public List<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}