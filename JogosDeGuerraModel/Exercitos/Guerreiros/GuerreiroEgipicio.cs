namespace JogosDeGuerraModel
{
    /// <summary>
    /// Modelo do guerreiro egipicio
    /// </summary>
    class GuerreiroEgipicio : Guerreiro
    {
        /// <summary>
        /// O caminho de onde a imagem que representara um guerreiro egipicio no servidor
        /// </summary>
        public override string UriImagem { get; protected set; } = "/Images/PNG/EGITO/dir/guerreiro_egipcio_dir.png";
    }
}
