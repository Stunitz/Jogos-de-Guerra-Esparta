using System.ComponentModel.DataAnnotations.Schema;

namespace JogosDeGuerraModel
{
    /// <summary>
    /// Esta sera a estrutura dos elementos que compoem um exercito no jogo
    /// </summary>
    public abstract class ElementoDoExercito
    {
        #region Public Properties


        /// <summary>
        /// Este sera o indetificador de um elemento de um exercito
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// A referencia de qual exercito esse elemento pertence
        /// </summary>
        public int ExercitoId { get; set; }

        /// <summary>
        /// A referencia do tabuleiro a qual o elemento foi alocado
        /// </summary>
        public int TabuleiroId { get; set; }

        /// <summary>
        /// O objeto exercito desse elemento
        /// </summary>
        [ForeignKey("ExercitoId")]
        [InverseProperty("Elementos")]
        public Exercito Exercito { get; set; }

        /// <summary>
        /// O objeto tabuleiro desse elemento
        /// </summary>
        [ForeignKey("TabuleiroId")]
        [InverseProperty("ElementosDoExercito")]
        public Tabuleiro Tabuleiro { get; set; }

        /// <summary>
        /// A posicao a qual o elemento se encontra em relacao ao tabuleiro
        /// </summary>
        public Posicao Posicao { get; set; }
        
        /// <summary>
        /// O alcance de movimento que essa unidade pode realizar, cada elemento devera implementar suas particularidades
        /// </summary>
        public abstract int AlcanceMovimento {get; protected set;}

        /// <summary>
        /// O alcance de ataque que essa unidade pode realizar, cada elemento devera implementar suas particularidades 
        /// </summary>
        public abstract int AlcanceAtaque { get; protected set; }

        /// <summary>
        /// O dano de ataque causado por essa unidade, cada elemento devera implementar suas particularidades 
        /// </summary>
        public abstract int Ataque { get; protected set; }

        /// <summary>
        /// Esta sera o indicador de saude de um elemento, cada elemento devera implementar suas particularidades 
        /// </summary>
        public abstract int Saude { get; set; }

        
        #endregion

        #region Public Methods


        /// <summary>
        /// Verifica se um objeto é esse elemento
        /// </summary>
        /// <param name="obj">O a ser verificado</param>
        /// <returns>O resultado da verificação</returns>
        public override bool Equals(object obj)
        {
            if (obj is ElementoDoExercito && this.Id > 0)
            {
                return ((ElementoDoExercito)obj).Id == this.Id;
            }
            return base.Equals(obj);
        }

        /// <summary>
        /// Retorna o HashCode dessa unidade
        /// </summary>
        /// <returns>Um HashCode</returns>
        public override int GetHashCode()
        {
            if (this.Id > 0)
            {
                return this.Id.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }


        #endregion
    }


}
