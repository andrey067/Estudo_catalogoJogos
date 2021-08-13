using CatalogoJogos.DTO.InputModel;
using CatalogoJogos.DTO.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogoJogos.Service
{
    public interface IJogoService : IDisposable
    {
        Task<IEnumerable<JogoViewModel>> Obter(int pagina, int quantidade);

        Task<JogoViewModel> Obter(Guid idguid);

        Task<JogoViewModel> Insetir(JogoInputModel jogoViewModel);
        Task Atualizar(Guid idjogo, JogoInputModel jogoViewModel);
        Task Atualizar(Guid idjogo, double preco);

        Task Remover(Guid idjogo);

    }    
}
