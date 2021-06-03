using clientes_api.Interfaces;
using clientes_api.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace clientes_api.DAO
{
    public class ClienteDAO : IClienteDAO
    {
        private IConfiguration iConfiguration;
        string query;
        public ClienteDAO( IConfiguration iConfiguration)
        {
            this.iConfiguration = iConfiguration;
        }

        public void Cadastrar(Cliente cliente)
        {
            query = "INSERT INTO clientes (CpfCnpj, DataCadastro, TipoPessoa, CodigoSap, Nome, WebSite, Email, InscricaoEstadual, Cmc, EmailNfe, NomeRepresentante)" +
                    "VALUES (@CpfCnpj, @DataCadastro, @TipoPessoa, @CodigoSap, @Nome, @WebSite, @Email, @InscricaoEstadual, @Cmc, @EmailNfe, @NomeRepresentante)";

            var parameters = new DynamicParameters();
            parameters.Add("@CpfCnpj", cliente.CpfCnpj);
            parameters.Add("@DataCadastro", cliente.DataCadastro);
            parameters.Add("@TipoPessoa", cliente.TipoPessoa);
            parameters.Add("@CodigoSap", cliente.CodigoSap);
            parameters.Add("@Nome", cliente.Nome);
            parameters.Add("@WebSite", cliente.WebSite);
            parameters.Add("@Email", cliente.Email);
            parameters.Add("@InscricaoEstadual", cliente.InscricaoEstadual);
            parameters.Add("@Cmc", cliente.Cmc);
            parameters.Add("@EmailNfe", cliente.EmailNfe);
            parameters.Add("@NomeRepresentante", cliente.NomeRepresentante);
            
            var con = this.FactoryConnection();
            con.Open();
            con.Query(query, parameters).ToList();
            con.Close();
        }

        public Cliente Alterar(Cliente cliente)
        {
            query = "UPDATE clientes  SET CpfCnpj = @CpfCnpj, DataCadastro = @DataCadastro, TipoPessoa = @TipoPessoa, CodigoSap = @CodigoSap, Nome = @Nome, " +
                    "WebSite = @WebSite, Email = @Email, InscricaoEstadual = @InscricaoEstadual, Cmc = @Cmc, EmailNfe = @EmailNfe, NomeRepresentante = @NomeRepresentante "+
                    "WHERE id = @id";

            var parameters = new DynamicParameters();
            parameters.Add("@CpfCnpj", cliente.CpfCnpj);
            parameters.Add("@DataCadastro", cliente.DataCadastro);
            parameters.Add("@TipoPessoa", cliente.TipoPessoa);
            parameters.Add("@CodigoSap", cliente.CodigoSap);
            parameters.Add("@Nome", cliente.Nome);
            parameters.Add("@WebSite", cliente.WebSite);
            parameters.Add("@Email", cliente.Email);
            parameters.Add("@InscricaoEstadual", cliente.InscricaoEstadual);
            parameters.Add("@Cmc", cliente.Cmc);
            parameters.Add("@EmailNfe", cliente.EmailNfe);
            parameters.Add("@NomeRepresentante", cliente.NomeRepresentante);
            parameters.Add("@id", cliente.Id);
            
            var con = this.FactoryConnection();
            con.Open();
            con.Query(query, parameters).ToList();
            con.Close();
            return cliente;
        }

        public List<Cliente> Listar(string nome)
        {
            nome = nome ?? String.Empty;

            List<Cliente> clientes;
            query = "SELECT * FROM clientes where Nome like '%"+ nome + "%' ";

            var con = this.FactoryConnection();
            con.Open();
            clientes = con.Query<Cliente>(query).ToList();
            con.Close();

            return clientes;
        }

        public void Excluir(Cliente cliente)
        {
            query = "DELETE FROM clientes where id = @clienteId";

            var parameters = new DynamicParameters();
            parameters.Add("@clienteId", cliente.Id);

            var con = this.FactoryConnection();
            con.Open();
            con.Query(query, parameters).ToList();
            con.Close();
        }

        public Cliente ProcurarPorId(int id)
        {
            Cliente cliente;
            query = "SELECT * FROM clientes where Id = @id ";

            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            var con = this.FactoryConnection();
            con.Open();
            cliente = con.Query<Cliente>(query, parameters).FirstOrDefault();
            con.Close();

            return cliente;
        }

        public MySqlConnection FactoryConnection(){
            return new MySqlConnection(iConfiguration.GetConnectionString("mysqlConnection"));
        }

    }
}
