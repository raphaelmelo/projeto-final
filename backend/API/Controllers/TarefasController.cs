using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoList.Interfaces;
using TodoList.Models;

namespace TodoList.Controllers
{
    [Route("api/tarefas")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        private readonly IRepositorio _repositorio;
        public TarefasController(IRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        /// <summary>
        /// Lista todas as tarefas da base de dados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var tarefas = _repositorio.GetAllTarefas();
                return Ok(tarefas);
            }
            catch (Exception ex)
            {
                return NotFound(new
                {
                    msg = "Falha ao listar as tarefas",
                    ex.Message
                });
            }

        }
        /// <summary>
        /// Insere uma tarefa na base de dados
        /// </summary>
        /// <remarks>
        /// 
        /// Exemplo de inserção (Não é necessário inserir um Id)
        /// 
        /// 
        ///     {
        ///        "titulo": "Título da tarefa",
        ///        "descricao": "Texto com a descrição da tarefa"
        ///     }
        ///     
        /// </remarks>
        /// <param name="tarefa"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Insert(Tarefa tarefa)
        {
            try
            {
                _repositorio.InsertTarefa(tarefa);
                return Ok(tarefa);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    msg = "Falha ao inserir uma tarefa",
                    ex.Message
                });
            }
        }
        /// <summary>
        /// Remove uma tarefa na base de dados
        /// </summary>
        /// <remarks>
        /// 
        ///     Informe o id da tarefa a ser removida da base de dados
        /// 
        /// </remarks>
        /// <param name="id">Id da tarefa a ser removida</param>
        /// <returns>Retorna um Ok result</returns>
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                _repositorio.DeleteTarefa(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    msg = "Falha ao remover uma tarefa",
                    ex.Message
                });
            }
        }

        /// <summary>
        /// Atualiza uma tarefa na base de dados
        /// </summary>
        /// <remarks>
        /// 
        /// Exemplo de atualização (É necessário inserir um Id)
        /// 
        ///     {
        ///        "id": 1,
        ///        "titulo": "Título atualizado da tarefa ",
        ///        "descricao": "Texto atualizado com a descrição da tarefa"
        ///     }
        ///     
        /// </remarks>
        /// <param name="tarefa">Tarefa a ser atualizada</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put(Tarefa tarefa)
        {
            try
            {
                var retorno = _repositorio.UpdateTarefa(tarefa);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    msg = "Falha ao alterar a tarefa",
                    ex.Message
                });
            }
        }
    }
}
