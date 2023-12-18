using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabuleiro
{
    internal class Tabuleiro
    {
        public int Coluna { get; set; }
        public int Linha { get; set; }
        private Peca[,] Pecas;


        public Tabuleiro(int linha, int coluna)
        {
            Coluna = coluna;
            Linha = linha;
            Pecas = new Peca[Linha, Coluna];
        }
    }
}
