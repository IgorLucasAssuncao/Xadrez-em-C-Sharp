using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabuleiro
{
    public class tabuleiro
    {
        public int Coluna { get; set; }
        public int Linha { get; set; }
        private Peca[,] Pecas;


        public tabuleiro(int linha, int coluna)
        {
            Coluna = coluna;
            Linha = linha;
            Pecas = new Peca[Linha, Coluna];
        }
        internal Peca Peca(int linha, int coluna)
        {
            return Pecas[linha, coluna];
        }
    }
}
