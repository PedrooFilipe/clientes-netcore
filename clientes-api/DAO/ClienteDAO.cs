using clientes_api.Interfaces;
using clientes_api.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clientes_api.DAO
{
    public class ClienteDAO : IClienteDAO
    {
        private Contexto contexto;
        public ClienteDAO(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public void Cadastrar(Cliente cliente)
        {
            contexto.Clientes.Add(cliente);
            contexto.SaveChanges();
        }

        public void Alterar(Cliente cliente)
        {
            contexto.Entry(cliente).State = EntityState.Modified;
            contexto.SaveChanges();
        }

        public List<Cliente> Listar(string nome)
        {
            nome = nome ?? String.Empty;
            return contexto.Clientes.AsNoTracking().Where(c => c.Nome.Contains(nome)).ToList();
        }

        public void Excluir(Cliente cliente)
        {
            contexto.Clientes.Remove(cliente);
            contexto.SaveChanges();
        }

        public Cliente ProcurarPorId(int id)
        {
            return contexto.Clientes.AsNoTracking().Where(c => c.Id == id).FirstOrDefault();
        }

    }
}
