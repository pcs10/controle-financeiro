using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleFinanceiro.Models
{
    public class Saldo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Column(TypeName = "DATE")]
        public DateTime DataConsulta { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Column(TypeName = "DECIMAL(18,2)")]
        public decimal Valor { get; set; }

        [Column(TypeName = "VARCHAR(100)")]
        public string? Observacao { get; set; }

        public Banco? Bancos { get; set; }


        public Saldo( decimal valor, DateTime dataConsulta)
        {
            DataConsulta = dataConsulta;
            Valor = valor;
        }
    }
}
