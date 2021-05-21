using clientes_api.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace clientes_api.Interfaces
{
    public interface IClienteDAO
    {
        void Cadastrar(Cliente cliente);

        void Alterar(Cliente cliente);

        List<Cliente> Listar(string nome);

        void Excluir(Cliente cliente);

        Cliente ProcurarPorId(int id);
    }
}
