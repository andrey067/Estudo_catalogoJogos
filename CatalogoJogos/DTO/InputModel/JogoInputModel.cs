using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoJogos.DTO.InputModel
{
    public class JogoInputModel
    {
        [Required]
        [StringLength(100, MinimumLength =3, ErrorMessage ="O nome do jogo deve ter de 3 a 100 caracteres")]
        public string Nome { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do produto deve ter de 3 a 100 caracteres")]
        public string Produtora { get; set; }

        [Required]
        [Range(1,1000, ErrorMessage = "O preço deve ser maior que 1 real")]
        public double Preco { get; set; }

    }
}
