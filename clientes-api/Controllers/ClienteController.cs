using clientes_api.Interfaces;
using clientes_api.Model;
using clientes_api.Model.Rest;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public ActionResult Alterar(int id, [FromBody] Cliente clienteParam)
        {
            Cliente cliente = iClienteDAO.ProcurarPorId(id);

            if(cliente != null)
            {
                try
                {
                    iClienteDAO.Alterar(clienteParam);
                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest("Erro ao alterar cliente");
                }
            }else
            {
                return BadRequest("Cliente não encontrado");
            }
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

            response.Data = clientes.Skip(skip).Take(10).ToList();
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

    }
}
