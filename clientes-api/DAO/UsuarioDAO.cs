using clientes_api.Interfaces;
using clientes_api.Entidades;
using clientes_api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace clientes_api.DAO
{
    public class UsuarioDAO : IUsuarioDAO
    {
        private IConfiguration iConfiguration;
        string query;
        public UsuarioDAO( IConfiguration iConfiguration)
        {
            this.iConfiguration = iConfiguration;
        }

        public void Cadastrar(Usuario usuario)
        {
            query = "INSERT INTO usuarios (Email, Nome, Senha) VALUES (@Email, @Nome, @Senha)";

            var parameters = new DynamicParameters();
            parameters.Add("@Email", usuario.Email);
            parameters.Add("@Nome", usuario.Nome);
            parameters.Add("@Senha", usuario.Senha);
            
            var con = this.FactoryConnection();
            con.Open();
            con.Query(query, parameters).ToList();
            con.Close();
        }

        public Usuario Alterar(Usuario usuario)
        {
            query = "UPDATE usuarios SET Email = @Email, Nome = @Nome, Senha = @Senha WHERE id = @id";

            var parameters = new DynamicParameters();
            parameters.Add("@Email", usuario.Email);
            parameters.Add("@Nome", usuario.Nome);
            parameters.Add("@Senha", usuario.Senha);
            parameters.Add("@id", usuario.Id);
            
            var con = this.FactoryConnection();
            con.Open();
            con.Query(query, parameters).ToList();
            con.Close();
            return usuario;
        }

        public List<Usuario> Listar(string nome)
        {
            nome = nome ?? String.Empty;

            List<Usuario> usuarios;
            query = "SELECT * FROM usuarios where Nome like '%"+ nome + "%' ";

            var con = this.FactoryConnection();
            con.Open();
            usuarios = con.Query<Usuario>(query).ToList();
            con.Close();

            return usuarios;
        }

        public void Excluir(Usuario usuario)
        {
            query = "DELETE FROM usuarios where id = @id";

            var parameters = new DynamicParameters();
            parameters.Add("@id", usuario.Id);

            var con = this.FactoryConnection();
            con.Open();
            con.Query(query, parameters).ToList();
            con.Close();
        }

        public Usuario ProcurarPorId(int id)
        {
            Usuario usuario;
            query = "SELECT * FROM usuarios where Id = @id ";

            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            var con = this.FactoryConnection();
            con.Open();
            usuario = con.Query<Usuario>(query, parameters).FirstOrDefault();
            con.Close();

            return usuario;
        }

        public Usuario Autenticar(UsuarioLogin usuarioLogin){
        
        Usuario usuario;
        query = "SELECT * FROM usuarios where Email = @email and Senha = @senha";

        var parameters = new DynamicParameters();
        parameters.Add("@email", usuarioLogin.Email);
        parameters.Add("@senha", usuarioLogin.Senha);

        var con = this.FactoryConnection();
        con.Open();
        usuario = con.Query<Usuario>(query, parameters).FirstOrDefault();
        con.Close();

        return usuario;
        }

        public Usuario BuscarPorEmail(string email){
            Usuario usuario;
            query = "SELECT * FROM usuarios where Email = @email ";

            var parameters = new DynamicParameters();
            parameters.Add("@email", email);

            var con = this.FactoryConnection();
            con.Open();
            usuario = con.Query<Usuario>(query, parameters).FirstOrDefault();
            con.Close();

            return usuario;
        }

        public MySqlConnection FactoryConnection(){
            return new MySqlConnection(iConfiguration.GetConnectionString("mysqlConnection"));
        }
    }
}
