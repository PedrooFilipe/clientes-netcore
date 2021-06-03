using clientes_api.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace clientes_api.Interfaces
{
    public interface IClienteDAO
    {
        void Cadastrar(Cliente cliente);

        Cliente Alterar(Cliente cliente);

        List<Cliente> Listar(string nome);

        void Excluir(Cliente cliente);

        Cliente ProcurarPorId(int id);
    }
}
