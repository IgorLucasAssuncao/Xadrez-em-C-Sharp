using System;
using Tabuleiro;

namespace Xadrez
{
    class Principal
    {
        public static void Main()
        {
            tabuleiro tabuleiro = new tabuleiro(8, 8);
           Tela.ImprimirTabuleiro(tabuleiro);   

        }
    }
}