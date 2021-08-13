using CatalogoJogos.DTO.InputModel;
using CatalogoJogos.DTO.ViewModel;
using CatalogoJogos.Exceptions;
using CatalogoJogos.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoJogos.Controllers.V1
{
    [Route("/api/V1/[controller]")]
    [ApiController]
    public class JogosController : Controller
    {
        private readonly IJogoService _ijogoservice;

        public JogosController(IJogoService ijogoservice)
        {
            _ijogoservice = ijogoservice;
        }

        /// <summary>
        /// Buscar todos os jogos de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possível retornar os jogos sem paginação
        /// </remarks>
        /// <param name="pagina">Indica qual página está sendo consultada. Mínimo 1</param>
        /// <param name="quantidade">Indica a quantidade de reistros por página. Mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de jogos</response>
        /// <response code="204">Caso não haja jogos</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JogoViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var jogos = await _ijogoservice.Obter(pagina, quantidade);

            if (jogos.Count() == 0)
                return NoContent();

            return Ok(jogos);
        }

        [HttpGet("{idJogo:guid}")]
        public async Task<ActionResult<JogoViewModel>> Obter([FromRoute] Guid idJogo)
        {
            var jogo = await _ijogoservice.Obter(idJogo);

            return Ok(jogo);
        }


        [HttpPost]
        public async Task<ActionResult<JogoViewModel>> InserirJogo([FromBody] JogoInputModel jogoInput)
        {
            try
            {
                await _ijogoservice.Insetir(jogoInput);
                return Ok();
            }
            //catch (jogonaocadastradoExeption e)
            catch (Exception e )
            {
                return NotFound()
;
            }
        }

        [HttpPut("{idjogo:guid}")]
        public async Task<ActionResult<List<object>>> AtualizarJogo([FromRoute] Guid idJogo, [FromRoute] JogoInputModel jogo)
        {
            try
            {
                await _ijogoservice.Atualizar(idJogo, jogo);

            }
            catch(Exception e)
            {



            }

            return Ok();
        }

        [HttpPatch("{idjogo:guid/preco/{preco:double}")]
        public async Task<ActionResult<List<object>>> AtualizarJogo(Guid idJogo, object preco)
        {
            return Ok();
        }

        [HttpDelete("{idjogo:guid}")]
        public async Task<ActionResult<List<object>>> DeletarJogo(Guid idJogo)
        {
            try
            {
                await _ijogoservice.Remover(idJogo);

                return Ok();
            }
            catch (JogoNaoCadastradoException ex)
            {
                return NotFound("Não existe este jogo" + ex);
            }
        }


    }
}



