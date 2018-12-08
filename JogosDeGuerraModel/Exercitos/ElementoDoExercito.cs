using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace JogosDeGuerraModel
{
    /// <summary>
    /// Esta sera a estrutura dos elementos que compoem um exercito no jogo
    /// </summary>
    [DataContract(IsReference = true)]
    public abstract class ElementoDoExercito
    {
        #region Public Properties


        /// <summary>
        /// Este sera o indetificador de um elemento de um exercito
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// A referencia de qual exercito esse elemento pertence
        /// </summary>
        [DataMember]
        public int ExercitoId { get; set; }

        /// <summary>
        /// A referencia do tabuleiro a qual o elemento foi alocado
        /// </summary>
        [DataMember]
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
        [DataMember]
        public Posicao Posicao { get; set; }

        /// <summary>
        /// O alcance de movimento que essa unidade pode realizar, cada elemento devera implementar suas particularidades
        /// </summary>
        [DataMember]
        public virtual int AlcanceMovimento {get; protected set;}

        /// <summary>
        /// O alcance de ataque que essa unidade pode realizar, cada elemento devera implementar suas particularidades 
        /// </summary>
        [DataMember]
        public virtual int AlcanceAtaque { get; protected set; }

        /// <summary>
        /// O dano de ataque causado por essa unidade, cada elemento devera implementar suas particularidades 
        /// </summary>
        [DataMember]
        public virtual int Ataque { get; protected set; }

        /// <summary>
        /// Esta sera o indicador de saude de um elemento, cada elemento devera implementar suas particularidades 
        /// </summary>
        [DataMember]
        public virtual int Saude { get; set; }

        /// <summary>
        /// O caminho de onde a imagem no servidor que representara um elemento
        /// </summary>
        [DataMember]
        public virtual string UriImagem { get; protected set; }


        #endregion

        #region Public Methods


        /// <summary>
        /// Verifica se um objeto é esse elemento
        /// </summary>
        /// <param name="obj">O objeto a ser verificado</param>
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

        // private num Tipo { Arqueiro = 1, Cavaleiro = 2, Guerreiro = 3 };
    }

    public class FirebaseElemento
    {
        public int Id { get; private set; }

        public int ExercitoId { get; private set; }

        public int TabuleiroId { get; private set; }

        public int PosicaoLargura { get; set; }

        public int PosicaoAltura { get; set; }

        public int Saude { get; set; }

        public int AlcanceMovimento { get; private set; }

        public int AlcanceAtaque { get; private set; }

        public int Ataque { get; private set; }

        public string UriImagem { get; private set; }


        public FirebaseElemento(ElementoDoExercito peca)
        {
            this.Id = peca.Id;
            this.ExercitoId = peca.ExercitoId;
            this.TabuleiroId = peca.TabuleiroId;
            this.Saude = peca.Saude;
            this.AlcanceMovimento = peca.AlcanceMovimento;
            this.AlcanceAtaque = peca.AlcanceAtaque;
            this.Ataque = peca.Ataque;
            this.UriImagem = peca.UriImagem;
            this.PosicaoAltura = peca.Posicao.Altura;
            this.PosicaoLargura = peca.Posicao.Largura;
        }

        public FirebaseElemento()
        {

        }

        public override string ToString()
        {
            return "Peca";
        }
    }
}
