using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoJogos.Exceptions
{
    public class JogoJaCadastradoExcepetion : Exception
    {
        public JogoJaCadastradoExcepetion(): base("Este jogo foi cadastrado")
        {
        }
    }
}
