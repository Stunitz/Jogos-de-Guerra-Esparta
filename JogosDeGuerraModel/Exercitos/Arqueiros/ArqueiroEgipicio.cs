namespace JogosDeGuerraModel
{
    /// <summary>
    /// Modelo do arqueiro egipicio
    /// </summary>
    class ArqueiroEgipicio : Arqueiro
    {
        /// <summary>
        /// O caminho de onde a imagem que representara um arqueiro egipicio no servidor
        /// </summary>
        public override string UriImagem { get; protected set; } = "/Images/arqueiro_egito_1.png";
    }
}
