namespace JogosDeGuerraModel
{
    /// <summary>
    /// Modelo do cavaleiro persa
    /// </summary>
    class CavaleiroPersa : Cavaleiro
    {
        /// <summary>
        /// O caminho de onde a imagem que representara um cavaleiro persa no servidor
        /// </summary>
        public override string UriImagem { get; protected set; } = "/Images/PNG/PERSA/direita/cavaleiro_persa_dir.png";
    }
}
