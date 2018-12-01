namespace JogosDeGuerraModel
{
    /// <summary>
    /// Modelo do guerreiro egipicio
    /// </summary>
    class GuerreiroEspartano : Guerreiro
    {
        /// <summary>
        /// O caminho de onde a imagem que representara um guerreiro egipicio no servidor
        /// </summary>
        public override string UriImagem { get; protected set; } = "/Images/PNG/ESPARTA/dir/guerreiro_esparta_dir.png";
    }
}
