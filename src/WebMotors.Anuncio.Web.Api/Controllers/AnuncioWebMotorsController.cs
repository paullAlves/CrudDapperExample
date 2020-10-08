using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using WebMotors.Anuncio.Model.Entity;
using WebMotors.Anuncio.Model.Repository.IDao;

namespace WebMotors.Anuncio.Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnuncioWebMotorsController : ControllerBase
    {
        private readonly ILogger<AnuncioWebMotorsController> _logger;
        private readonly IAnuncioWebMotorsDao _anuncioWebMotorsDao;
        public AnuncioWebMotorsController(ILogger<AnuncioWebMotorsController> logger, IAnuncioWebMotorsDao anuncioWebMotorsDao)
        {
            _logger = logger;
            _anuncioWebMotorsDao = anuncioWebMotorsDao;
        }


        [HttpGet]
        [Route("")]
        public async Task<IActionResult> ListAsync()
        {
            try
            {
                var res = await Task.Run(() => _anuncioWebMotorsDao.ObterTodos());                
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id:long}")]
        public async Task<IActionResult> GetItemById(long id)
        {
            try
            {
                var res = await Task.Run(() => _anuncioWebMotorsDao.ObterPorChave(id));              
                return Ok(res);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateItemAsync([FromBody]JObject posted)
        {
            try
            {
                string message = null;
                AnuncioWebMotors anuncio = posted.ToObject<AnuncioWebMotors>();
                var res = await Task.Run(() => this._anuncioWebMotorsDao.Inserir(anuncio, out message));
                if (!string.IsNullOrEmpty(message))
                {
                    throw new Exception(message);
                }

                return Ok(new
                {
                    Id = res,
                    Message = "Registro salvo com sucesso!"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:long}")]
        public async Task<IActionResult> UpdateItemAsync(long id, [FromBody]JObject posted)
        {
            try
            {
                if (id <= 0 || posted == null) throw new Exception("Nenhum anuncio foi informado");

                string message = null;
                AnuncioWebMotors anuncioNew = posted.ToObject<AnuncioWebMotors>();

                AnuncioWebMotors anuncioOld = await Task.Run(() => _anuncioWebMotorsDao.ObterPorChave(id));

                if (anuncioOld == null) throw new Exception("O anuncio informado não está cadastrado.");

                await Task.Run(() => this._anuncioWebMotorsDao.Alterar(anuncioNew, out message));
                if (!string.IsNullOrEmpty(message))
                {
                    throw new Exception(message);
                }
                return Ok(new
                {
                    Id = id,
                    Message = "Registro atualizado com sucesso!"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id:long}")]
        public async Task<IActionResult> DeleteItemAsync(long id)
        {
            try
            {
                if (id <= 0) throw new Exception("Nenhum anuncio foi informado");

                string message = null;

                await Task.Run(() => this._anuncioWebMotorsDao.ExcluirLista(new
                {
                    ID = id
                }, out message));

                await Task.Run(() => this._anuncioWebMotorsDao.Excluir(id, out message));

                if (!string.IsNullOrEmpty(message))
                {
                    throw new Exception(message);
                }

                return Ok(new
                {
                    ID = id,
                    Message = "Registro excluído com sucesso!"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}