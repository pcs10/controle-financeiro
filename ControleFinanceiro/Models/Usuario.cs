﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(20, ErrorMessage = "Este campo deve conter entre 3 e 20 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 20 caracteres")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(100, ErrorMessage = "Este campo deve conter entre 5 e 100 caracteres")]
        [MinLength(5, ErrorMessage = "Este campo deve conter entre 5 e 100 caracteres")]
        public string Password { get; set; }
        public string? Role { get; set; }


        public Usuario(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
