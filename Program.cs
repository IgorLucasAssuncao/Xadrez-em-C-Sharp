using System;
using Tabuleiro;

namespace Xadrez
{
    class Principal
    {
        public static void Main()
        {
            tabuleiro tabuleiro = new tabuleiro(8, 8);

            tabuleiro.ColocarPeca(new Torre(Cor.Preta, tabuleiro), new Posicao(0, 0));
            tabuleiro.ColocarPeca(new Torre(Cor.Preta, tabuleiro), new Posicao(1, 3));

            Tela.ImprimirTabuleiro(tabuleiro);
            Console.Clear();

        }
    }
}