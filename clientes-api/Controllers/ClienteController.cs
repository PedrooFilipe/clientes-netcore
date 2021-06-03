using clientes_api.Interfaces;
using clientes_api.Entidades;
using clientes_api.Model.Rest;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace clientes_api.Controllers
{
    [Route("clientes")]
    public class ClienteController : ControllerBase
    {
        IClienteDAO iClienteDAO;

        public ClienteController(IClienteDAO iClienteDAO)
        {
            this.iClienteDAO = iClienteDAO;
        }
        
        [Route("cadastrar"), HttpPost]
        public ActionResult Cadastrar([FromBody] Cliente cliente)
        {
            try
            {
                iClienteDAO.Cadastrar(cliente);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest("Erro ao adicionar cliente");            
            }
        }

        [Route("alterar/{id}"), HttpPut]
        public RestResponse<Cliente> Alterar(int id, [FromBody] Cliente clienteParam)
        {
            Cliente cliente = iClienteDAO.ProcurarPorId(id);
            RestResponse<Cliente> response = new RestResponse<Cliente>();

            if(cliente != null)
            {
                clienteParam.Id = cliente.Id;
                try
                {
                    response.Data = iClienteDAO.Alterar(clienteParam);
                }
                catch (Exception e)
                {
                    response.ResponseMessage = e.Message;
                    //return BadRequest("Erro ao alterar cliente");

                }
            }else
            {
                response.ResponseMessage = "Cliente não encontrado";
            }

            return response;
        }

        [Route("listar"), HttpGet]
        public RestResponse<List<Cliente>> Listar(string nome, int page = 1)
        {
            List<Cliente> clientes = iClienteDAO.Listar(nome);
            RestResponse<List<Cliente>> response = new RestResponse<List<Cliente>>();

            int resultsPerPage = 5;
            int skip = (page - 1) * resultsPerPage;
            decimal totalCalculation = clientes.Count() / resultsPerPage;

            int totalPages = int.Parse(Math.Truncate(totalCalculation + 1).ToString());

            response.Data = clientes.Skip(skip).Take(resultsPerPage).ToList();
            response.Paginator = new Paginator()
            {
                Current = page,
                Total = totalPages
            };

            return response;
        }

        [Route("buscar/{id}"), HttpGet]
        public RestResponse<Cliente> Buscar(int id)
        {
            Cliente cliente = iClienteDAO.ProcurarPorId(id);
            RestResponse<Cliente> response = new RestResponse<Cliente>();

            response.Data = cliente;

            return response;
        }

        [Route("excluir/{id}"), HttpDelete]
        public ActionResult Excluir(int id)
        {
            Cliente cliente = iClienteDAO.ProcurarPorId(id);

            if (cliente != null)
            {
                try
                {
                    iClienteDAO.Excluir(cliente);
                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }else
            {
                return BadRequest();
            }
        }

        [Route("importar"), HttpPost]
        public void ImportarArquivo([FromForm] IFormFile file ){
            Console.WriteLine("aq");
        }

    }//end of class
}//end of namespace
