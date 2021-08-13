using CatalogoJogos.DTO.InputModel;
using CatalogoJogos.DTO.ViewModel;
using CatalogoJogos.Entities;
using CatalogoJogos.Exceptions;
using CatalogoJogos.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoJogos.Service
{
    public class JogoService : IJogoService
    {
        private readonly IJogoRepository _jogoRepository;

        public JogoService(IJogoRepository jogoRepository)
        {
            _jogoRepository = jogoRepository;
        }

        public async Task Atualizar(Guid idjogo, JogoInputModel jogo)
        {
            var entidadeJogo = await _jogoRepository.Obter(idjogo);

            if (entidadeJogo is null)
                throw new JogoNaoCadastradoException();

            entidadeJogo.Nome = jogo.Nome;
            entidadeJogo.Produtora = jogo.Produtora;
            entidadeJogo.Preco = jogo.Preco;
            await _jogoRepository.Atualizar(entidadeJogo);
        }

        public async Task Atualizar(Guid idjogo, double preco)
        {
            var entidadeJogo = await _jogoRepository.Obter(idjogo);

            if (entidadeJogo is null)
                throw new JogoNaoCadastradoException();
            entidadeJogo.Preco = preco;

        }

        public async Task<JogoViewModel> Insetir(JogoInputModel jogo)
        {
            var entidade = await _jogoRepository.Obter(jogo.Nome, jogo.Produtora);

            if (entidade.Count() > 0)
                throw new JogoJaCadastradoExcepetion();

            var jogoInsert = new Jogo
            {
                Id = Guid.NewGuid(),
                Nome = jogo.Nome,
                Preco = jogo.Preco,
                Produtora = jogo.Produtora
            };
            await _jogoRepository.Insert(jogoInsert);

            return new JogoViewModel
            {
                Id = jogoInsert.Id,
                Nome = jogoInsert.Nome,
                Produtora = jogoInsert.Produtora,
                Preco = jogoInsert.Preco
            };
        }

        public async Task<IEnumerable<JogoViewModel>> Obter(int pagina, int quantidade)
        {
            var jogos = await _jogoRepository.Obter(pagina, quantidade);

            return jogos.Select(jogo => new JogoViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Preco = jogo.Preco
            }).ToList();
        }

        public async Task<JogoViewModel> Obter(Guid id)
        {
            var jogo = await _jogoRepository.Obter(id);
            if (jogo is null)
                return null;

            return new JogoViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Preco = jogo.Preco
            };
        }

        public async Task Remover(Guid id)
        {
            var jogo = await _jogoRepository.Obter(id);
            if (jogo is null)
                throw new JogoNaoCadastradoException();

            await _jogoRepository.Remover(id);
        }

        public void Dispose()
        {
            _jogoRepository.Dispose();
        }
    }
}
