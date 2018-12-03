using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;

namespace JogosDeGuerraModel
{
    /// <summary>
    /// Esta sera a estrutura de um exercito no jogo
    /// </summary>
    [DataContract(IsReference = true)]
    public class Exercito
    {
        #region Public Properties


        /// <summary>
        /// Este sera o indetificador de um exercito
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// Os elementos que compoem um exercito
        /// </summary>
        [DataMember]
        [InverseProperty("Exercito")]
        public ICollection<ElementoDoExercito> Elementos { get; set; } =
            new HashSet<ElementoDoExercito>();

        /// <summary>
        /// O identificador da batalha que este exercito pertence
        /// </summary>
        [DataMember]
        public int? BatalhaId { get; set; }

        /// <summary>
        /// A batalha que este exercito pertence
        /// </summary>
        [ForeignKey("BatalhaId")]
        public Batalha Batalha { get; set; }

        /// <summary>
        /// O identificador do usuario que este exercito pertence
        /// </summary>
        [DataMember]
        public int UsuarioId { get; set; }

        /// <summary>
        /// O usuario que este exercito pertence
        /// </summary>
        [DataMember]
        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }

        /// <summary>
        /// A nacao que este exercito pertence
        /// </summary>
        [DataMember]
        public AbstractFactoryExercito.Nacao Nacao { get; set; }

        /// <summary>
        /// Os elementos que ainda estao vivos no exercito em um partida
        /// </summary>
        public ICollection<ElementoDoExercito> ElementosVivos
        {
            get
            {
                var resultado = from elemento in Elementos
                                where elemento.Saude > 0
                                select elemento;
                return resultado.ToList();
            }
        }


        #endregion

        #region Public Methods


        /// <summary>
        /// Verifica se um objeto é esse exercito
        /// </summary>
        /// <param name="obj">O objeto a ser verificado</param>
        /// <returns>O resultado da verificação</returns>
        public override bool Equals(object obj)
        {
            if (obj is Exercito)
                return ((Exercito)obj).Id == this.Id;
            return false;
        }

        /// <summary>
        /// Retorna o HashCode desse exercito
        /// </summary>
        /// <returns>Um HashCode</returns>
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }


        #endregion
    }
}