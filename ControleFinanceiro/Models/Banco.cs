using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleFinanceiro.Models
{
    public class Banco
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Column(TypeName = "VARCHAR(50)")]
        [MaxLength(20, ErrorMessage = "Este campo deve conter entre 3 e 50 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 50 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Column(TypeName = "VARCHAR(100)")]
        [MaxLength(100, ErrorMessage = "Este campo deve conter entre 5 e 100 caracteres")]
        [MinLength(5, ErrorMessage = "Este campo deve conter entre 5 e 100 caracteres")]
        public string Descricao { get; set; }

        public Banco(string nome, string descricao)
        {
            Nome = nome;
            Descricao = descricao;
        }
    }
}
