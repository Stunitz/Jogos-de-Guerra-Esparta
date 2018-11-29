namespace JogosDeGuerraModel
{
    /// <summary>
    /// Modelo do arqueiro persa
    /// </summary>
    class ArqueiroPersa : Arqueiro
    {
        /// <summary>
        /// O caminho de onde a imagem que representara um arqueiro persa no servidor
        /// </summary>
        public override string UriImagem { get; protected set; } = "/Images/arqueiro_persa_1.png";
    }
}
