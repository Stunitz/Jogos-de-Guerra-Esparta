using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogosDeGuerraModel
{
    /// <summary>
    /// O modelo do elemento guerreiro de um exercito
    /// </summary>
    public abstract class Guerreiro : ElementoDoExercito
    {
        #region Public Properties


        /// <summary>
        /// O alcance de movimento que um guerreiro pode realizar
        /// </summary>
        public override int AlcanceMovimento { get; protected set; } = 1;

        /// <summary>
        /// O alcance de ataque que um guerreiro pode realizar
        /// </summary>
        public override int AlcanceAtaque { get; protected set; } = 1;

        /// <summary>
        /// O dano de ataque realizado por um guerreiro
        /// </summary>
        public override int Ataque { get; protected set; } = 45;

        /// <summary>
        /// A saude de um guerreiro
        /// </summary>
        public override int Saude { get; set; } = 150;

        /// <summary>
        /// O caminho de onde a imagem que representara um guerreiro no servidor
        /// </summary>
        public abstract override string UriImagem { get; protected set; }


        #endregion
    }
}
