namespace JogosDeGuerraModel
{
    /// <summary>
    /// Modelo do guerreiro indiano
    /// </summary>
    class GuerreiroIndiano : Guerreiro
    {
        /// <summary>
        /// O caminho de onde a imagem que representara um guerreiro indiano no servidor
        /// </summary>
        public override string UriImagem { get; protected set; } = "/Images/guerreiro_indiano_1.png";
    }
}
