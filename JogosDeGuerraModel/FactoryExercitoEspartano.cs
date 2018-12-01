using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogosDeGuerraModel
{
    class FactoryExercitoEspartano : AbstractFactoryExercito
    {
        public override Arqueiro CriarArqueiro()
        {
            return new ArqueiroEspartano();
        }

        public override Cavaleiro CriarCavalaria()
        {
            return new CavaleiroEspartano();
        }

        public override Guerreiro CriarGuerreiro()
        {
            return new GuerreiroEspartano();
        }
    }
}
