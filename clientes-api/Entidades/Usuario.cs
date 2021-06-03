using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clientes_api.Entidades
{
    [Table("usuario")]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Senha {get; set;}
    }
}
