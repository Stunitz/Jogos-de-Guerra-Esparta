using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogosDeGuerraModel
{
    class FactoryExercitoEgipcio : AbstractFactoryExercito
    {
        public override Arqueiro CriarArqueiro()
        {
            return new ArqueiroEgipicio();
        }

        public override Cavaleiro CriarCavalaria()
        {
            return new CavaleiroEgipicia();
        }

        public override Guerreiro CriarGuerreiro()
        {
            return new GuerreiroEgipicio();
        }
    }
}
