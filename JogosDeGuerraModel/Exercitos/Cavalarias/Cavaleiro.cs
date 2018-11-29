namespace JogosDeGuerraModel
{
    /// <summary>
    /// O modelo do elemento cavaleiro de um exercito
    /// </summary>
    public abstract class Cavaleiro : ElementoDoExercito
    {
        #region Public Properties


        /// <summary>
        /// O alcance de movimento que um cavaleiro pode realizar
        /// </summary>
        public override int AlcanceMovimento { get; protected set; } = 3;

        /// <summary>
        /// O alcance de ataque que um cavaleiro pode realizar
        /// </summary>
        public override int AlcanceAtaque { get; protected set; } = 1;

        /// <summary>
        /// O dano de ataque realizado por um cavaleiro
        /// </summary>
        public override int Ataque { get; protected set; } = 25;

        /// <summary>
        /// A saude de um cavaleiro
        /// </summary>
        public override int Saude { get; set; } = 75;

        /// <summary>
        /// O caminho de onde a imagem que representara um cavaleiro no servidor
        /// </summary>
        public abstract string UriImagem { get; protected set; }


        #endregion
    }
}
