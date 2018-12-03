namespace JogosDeGuerraModel
{
    /// <summary>
    /// O modelo do elemento arqueiro de um exercito
    /// </summary>
    public abstract class Arqueiro : ElementoDoExercito
    {
        #region Public Properties


        /// <summary>
        /// O alcance de movimento que um arqueiro pode realizar
        /// </summary>
        public override int AlcanceMovimento { get; protected set; } = 1;

        /// <summary>
        /// O alcance de ataque que um arqueiro pode realizar
        /// </summary>
        public override int AlcanceAtaque { get; protected set; } = 3;

        /// <summary>
        /// O dano de ataque realizado por um arqueiro
        /// </summary>
        public override int Ataque { get; protected set; } = 10;

        /// <summary>
        /// A saude de um arqueiro
        /// </summary>
        public override int Saude { get; set; } = 75;

        /// <summary>
        /// O caminho de onde a imagem que representara um arqueiro no servidor
        /// </summary>
        public abstract override string UriImagem { get; protected set; }


        #endregion
    }
}
