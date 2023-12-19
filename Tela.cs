using Tabuleiro;
using System;
namespace Xadrez
{
    internal class Tela
    {
        public static void ImprimirTabuleiro(tabuleiro tab)
        {
            for (int i = 0; i < tab.Linha; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.Coluna; j++)
                {
                        imprimirPeca(tab.Peca(i, j));
                    
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        static void imprimirPeca(Peca peca) //usado para imprimir as peças no tabuleiro com cor diferente para cada jogador
        {
            if (peca == null)
            {
                Console.Write("- ");

            }
            else
            {
                if (peca.Cor == Cor.Branca)
                {
                    Console.Write(peca + " ");
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
            }
        }
        public static void ImprimirTabuleiro(tabuleiro tab, bool[,] posicoesPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int i = 0; i < tab.Linha; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.Coluna; j++)
                {

                    if (posicoesPossiveis[i, j])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }

                    imprimirPeca(tab.Peca(i, j));

                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = fundoOriginal;
        }
        public static PosicaoXadrez LerPosicaoXadrez() //usado para ler a posição da peça que o jogador quer mover
        {
            string s = Console.ReadLine() ?? "a1";
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);
        }
    }
}
