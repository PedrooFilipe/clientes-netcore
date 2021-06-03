using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using clientes_api.Entidades;
using clientes_api.Interfaces;
using clientes_api.Model.Rest;
using clientes_api.Helpers;

namespace usuarios_api.Controllers
{
    [Route("usuarios")]
    public class UsuarioController : ControllerBase
    {
        IUsuarioDAO iUsuarioDAO;

        public UsuarioController(IUsuarioDAO iUsuarioDAO)
        {
            this.iUsuarioDAO = iUsuarioDAO;
        }
        
        [Route("cadastrar"), HttpPost]
        public ActionResult Cadastrar([FromBody] Usuario usuario)
        {
            usuario.Senha = Helper.CalculateMD5Hash(usuario.Senha);
            try
            {
                iUsuarioDAO.Cadastrar(usuario);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest("Erro ao adicionar usuario");            
            }
        }

        [Route("alterar/{id}"), HttpPut]
        public RestResponse<Usuario> Alterar(int id, [FromBody] Usuario usuarioParam)
        {
            Usuario usuario = iUsuarioDAO.ProcurarPorId(id);
            RestResponse<Usuario> response = new RestResponse<Usuario>();

            if(usuario != null)
            {
                usuarioParam.Id = usuario.Id;
                usuarioParam.Senha = Helper.CalculateMD5Hash(usuarioParam.Senha);
                try
                {
                    response.Data = iUsuarioDAO.Alterar(usuarioParam);
                }
                catch (Exception e)
                {
                    response.ResponseMessage = e.Message;
                }
            }else
            {
                response.ResponseMessage = "Usuario não encontrado";
            }

            return response;
        }

        [Route("listar"), HttpGet]
        public RestResponse<List<Usuario>> Listar(string nome, int page = 1)
        {
            List<Usuario> usuarios = iUsuarioDAO.Listar(nome);
            RestResponse<List<Usuario>> response = new RestResponse<List<Usuario>>();

            int resultsPerPage = 5;
            int skip = (page - 1) * resultsPerPage;
            decimal totalCalculation = usuarios.Count() / resultsPerPage;

            int totalPages = int.Parse(Math.Truncate(totalCalculation + 1).ToString());

            response.Data = usuarios.Skip(skip).Take(resultsPerPage).ToList();
            response.Paginator = new Paginator()
            {
                Current = page,
                Total = totalPages
            };

            return response;
        }

        [Route("buscar/{id}"), HttpGet]
        public RestResponse<Usuario> Buscar(int id)
        {
            Usuario usuario = iUsuarioDAO.ProcurarPorId(id);
            RestResponse<Usuario> response = new RestResponse<Usuario>();

            response.Data = usuario;

            return response;
        }

        [Route("excluir/{id}"), HttpDelete]
        public ActionResult Excluir(int id)
        {
            Usuario usuario = iUsuarioDAO.ProcurarPorId(id);

            if (usuario != null)
            {
                try
                {
                    iUsuarioDAO.Excluir(usuario);
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
