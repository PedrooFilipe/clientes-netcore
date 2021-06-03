using clientes_api.Entidades;
using clientes_api.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace clientes_api.Interfaces
{
    public interface IUsuarioDAO
    {
        void Cadastrar(Usuario usuario);
        Usuario Alterar(Usuario usuario);
        List<Usuario> Listar(string nome);
        void Excluir(Usuario usuario);
        Usuario ProcurarPorId(int id);
        Usuario Autenticar(UsuarioLogin usuarioLogin);
        Usuario BuscarPorEmail(string email);
    }//end of class
}//end of namespace
