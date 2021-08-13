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

                return Ok();
            }
            catch (JogoNaoCadastradoException ex)
            {
                return NotFound("Não existe este jogo");
            }
        }

        [HttpPatch("{idJogo:guid}/preco/{preco:double}")]
        public async Task<ActionResult<List<object>>> AtualizarJogo(Guid idJogo, double preco)
        {
            try
            {
                await _ijogoservice.Atualizar(idJogo, preco);

                return Ok();
            }
            catch (JogoNaoCadastradoException ex)
            {
                return NotFound("Não existe este jogo");
            }
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



