using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace JogosDeGuerraModel
{
    /// <summary>
    /// Esta sera a estrutura de um tabuleiro no jogo
    /// </summary>
    [DataContract(IsReference = true)]
    public class Tabuleiro
    {
        #region Public Properties

        /// <summary>
        /// Este sera o indetificador de um tabuleiro
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// Esta é a largura em posicoes que o tabuleiro ira ter
        /// </summary>
        [DataMember]
        public int Largura { get; set; }

        /// <summary>
        /// Esta é a altura em posicoes que o tabuleiro ira ter
        /// </summary>
        [DataMember]
        public int Altura { get; set; }


        #endregion

        #region Public Methods

        /// <summary>
        /// Os elementos que estao posiciodados dentro do tabuleiro
        /// </summary>
        [DataMember]
        [InverseProperty("Tabuleiro")]
        public ICollection<ElementoDoExercito> ElementosDoExercito { get; set; }

        /// <summary>
        /// Procura por um elemento em uma posicao dentro do tabuleiro
        /// </summary>
        /// <param name="p">A posicao que contem o elemento</param>
        /// <returns>O elemento na posicao informada</returns>
        public ElementoDoExercito ObterElemento(Posicao p)
        {
            return this.ElementosDoExercito
                .Where(e => e.Posicao.Equals(p) && e.Saude > 0).FirstOrDefault();
        }

        /// <summary>
        /// Procura por uma posicao no tabuleiro atraves de um elemento de um exercito
        /// </summary>
        /// <param name="elemento">O elemento de um exercito</param>
        /// <returns>A posicao dele nesse tabuleiro</returns>
        public Posicao ObterPosicao(ElementoDoExercito elemento)
        {
            return elemento.Posicao;
        }

        /// <summary>
        /// Inicia o jogo com dois exercitos
        /// </summary>
        /// <param name="exercito1">Exercito um</param>
        /// <param name="exercito2">Exercito dois</param>
        public void IniciarJogo(Exercito exercito1, Exercito exercito2)
        {
            for(int i=0; i< this.Largura; i++)
            {
                for( int j=0; j< this.Altura; j++)
                {
                    //Ultima ou primeira fileira?
                    Exercito exercito = (j == 0 || j == 1) ? exercito1 : exercito2;
                    ElementoDoExercito elemento = null;
                    AbstractFactoryExercito factory = 
                        AbstractFactoryExercito.CriarFactoryExercito(exercito.Nacao);

                    if (j==0 || j == this.Altura - 1)
                    {             
                        //Cria arqueiro nas posições pares e Cavaleiros nas posições impáres.
                        elemento= 
                            (i%2==0)?
                            (ElementoDoExercito)factory.CriarArqueiro(): 
                            (ElementoDoExercito)factory.CriarCavalaria();                      
                    }else if(j==1 || j == this.Altura - 2)
                    {
                        //Cria guerreiros
                        elemento = (ElementoDoExercito)factory.CriarGuerreiro();
                    }

                    //Se o elemento tiver sido instanciado criará o elemento no tabuleiro.
                    if (elemento != null)
                    {

                        exercito.Elementos.Add(elemento);
                        elemento.Posicao = new Posicao(i, j);
                        elemento.Tabuleiro = this;
                        // this.Casas.Add(elemento.posicao, elemento);
                    }


                }
            }
        }

        /// <summary>
        /// Move um elemento do tabuleiro para uma nova posicao
        /// </summary>
        /// <param name="movimento"></param>
        internal void MoverElemento(Movimento movimento)
        {
            //this.Casas[ObterPosicao(movimento.Elemento)] = null;
            //this.Casas[movimento.posicao] = movimento.Elemento;
            movimento.Elemento.Posicao = movimento.Posicao;
            this.ElementosDoExercito.ToList().ForEach(x =>
            {
                if (x.Id == movimento.Elemento.Id)
                    x = movimento.Elemento;
            });
        }

        /// <summary>
        /// Move um elemento do tabuleiro para uma nova posicao
        /// </summary>
        /// <param name="movimento"></param>
        internal bool AtacarElemento(ElementoDoExercito agressor, ElementoDoExercito vitima)
        {
            vitima.Saude -= agressor.Ataque;

            return VerificarElementoVivo(vitima);
        }

        internal bool VerificarElementoVivo(ElementoDoExercito vitima)
        {
            bool vitimaMorreu = vitima.Saude <= 0;

            if (vitimaMorreu)
                vitima.Saude = 0;


            this.ElementosDoExercito.ToList().ForEach(x =>
            {
                if (x.Id == vitima.Id)
                    x.Saude = vitima.Saude;
            });

            return vitimaMorreu;
        }

        /// <summary>
        /// Verifica se um objeto é esse tabuleiro
        /// </summary>
        /// <param name="obj">O objeto a ser verificado</param>
        /// <returns>O resultado da verificação</returns>
        public override bool Equals(object obj)
        {
            if (obj is Tabuleiro)
                return ((Tabuleiro)obj).Id == this.Id;
            return false;
        }

        /// <summary>
        /// Retorna o HashCode dessa tabuleiro
        /// </summary>
        /// <returns>Um HashCode</returns>
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }


        #endregion
    }

    public class FirebaseTabuleiro
    {
        public int Id { get; set; }

        public int? TurnoId { get; set; }

        public int Largura { get; set; }

        public int Altura { get; set; }


        public List<FirebaseElemento> Pecas { get; private set; }

        public FirebaseTabuleiro(Tabuleiro tabuleiro, int? turnoId)
        {
            this.Id = tabuleiro.Id;
            this.TurnoId = turnoId;
            this.Largura = tabuleiro.Largura;
            this.Altura = tabuleiro.Altura;
            this.Pecas = tabuleiro.ElementosDoExercito.Select(x => new FirebaseElemento(x)).ToList();
        }

        public FirebaseTabuleiro()
        {

        }

        public override string ToString()
        {
            return "Tabuleiros";
        }
    }
}

