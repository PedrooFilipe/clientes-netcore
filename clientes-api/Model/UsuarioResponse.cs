using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using clientes_api.Entidades;

namespace clientes_api.Model
{
    public class UsuarioResponse
    {
        public string Email { get; set; }
        public string Nome {get; set;}
        public string Token {get; set;}

        public static UsuarioResponse ToUsuarioResponse(Usuario usuario, string token){
            return new UsuarioResponse(){
                Nome = usuario.Nome,
                Email = usuario.Email,
                Token = token
            };
        }
    }//end of class
}//end of namespace
