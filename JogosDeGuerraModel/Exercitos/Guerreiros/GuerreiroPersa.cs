using System;

namespace JogosDeGuerraModel
{
    /// <summary>
    /// Modelo do guerreiro persa
    /// </summary>
    class GuerreiroPersa : Guerreiro
    {
        /// <summary>
        /// O caminho de onde a imagem que representara um guerreiro persa no servidor
        /// </summary>
        public override string UriImagem { get; protected set; }

        public GuerreiroPersa()
        {
            UriImagem = $"/Images/guerreiro_persa_{new Random().Next(1, 3)}.png";
        }
    }
}
