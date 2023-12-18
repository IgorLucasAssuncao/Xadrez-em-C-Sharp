using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabuleiro
{
    internal class Rei : Peca
    {
        public Rei(Cor cor, tabuleiro tab) : base(cor, tab)
        {
        }
        public override string ToString()
        {
            return "R";
        }
    }
}
