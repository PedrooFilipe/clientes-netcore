using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clientes_api.Model
{
    public class UsuarioLogin
    {
        public string Email { get; set; }
        public string Senha {get; set;}
    }
}
