using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JogosDeGuerraModel
{
    /// <summary>
    /// Esta sera a estrutura de um usuario no jogo
    /// </summary>
    [DataContract(IsReference = true)]
    public class Usuario
    {
        /// <summary>
        /// Este sera o indetificador de um usuario
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// A lista de batalhas que esse usuario participa
        /// </summary>
        [DataMember]
        public IList<Batalha> Batalhas { get; set; }

        /// <summary>
        /// O nome do usuario no jogo
        /// </summary>
        [DataMember]
        public String Name { get; set; }

        /// <summary>
        /// O email desse usuario
        /// </summary>
        [DataMember]
        public String Email { get; set; }
    }
}
