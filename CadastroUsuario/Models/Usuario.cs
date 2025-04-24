﻿using Microsoft.AspNetCore.Identity;

namespace CadastroUsuario.Models
{
    public class Usuario
    {
        public Guid UsuarioId { get; set; }
        public string NomeCompleto { get; set; }
        public string Celular { get; set; }
        public string CPF { get; set; }
        public Guid? AppUserId { get; set; }
        public IdentityUser? IdentityUser { get; set; }
    }
}
