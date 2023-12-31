﻿using Tabuleiro;
using System;
namespace Xadrez
{
    internal class Tela
    {
        public static void ImprimirPartida(PartidaDeXadrez partida) //usado para imprimir o tabuleiro e as peças capturadas
        {
            Console.Clear();
            ImprimirTabuleiro(partida.tab);
            Console.WriteLine();
            Console.WriteLine("Peças capturadas: ");
            Console.Write("Brancas: ");
            ImprimirConjunto(partida.pecasCapturadas(Cor.Branca));
            Console.WriteLine();

            ConsoleColor consoleColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.Write("Pretas: ");
            ImprimirConjunto(partida.pecasCapturadas(Cor.Preta));
            Console.WriteLine();

            Console.ForegroundColor = consoleColor;

            Console.WriteLine("Turno: " + partida.Turno);
            Console.WriteLine("Aguardando jogada: " + partida.JogadorAtual);

            Console.WriteLine();
            if (partida.Xeque)
            {
                Console.WriteLine("XEQUE!");
            }
            /*  if (!partida.Terminada)
              {
                  Console.WriteLine("Aguardando jogada: " + partida.JogadorAtual);
                  if (partida.Xeque)
                  {
                      Console.WriteLine("XEQUE!");
                  }
              }
              else
              {
                  Console.WriteLine("XEQUEMATE!");
                  Console.WriteLine("Vencedor: " + partida.JogadorAtual);
              }
            */
        }   
        public static void ImprimirPecasCapturadas(PartidaDeXadrez partida)
        {
            Console.Write("Brancas: ");
            ImprimirConjunto(partida.pecasCapturadas(Cor.Branca));
            Console.WriteLine();
            Console.Write("Pretas: ");
            ConsoleColor aux = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Yellow;
            ImprimirConjunto(partida.pecasCapturadas(Cor.Preta));
            Console.ForegroundColor = aux;
            Console.WriteLine();

        }
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
                    Console.Write(" ");
                }
            }
        }
        public static void ImprimirTabuleiro(tabuleiro tab, bool[,] posicoesPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int i = 0; i < tab.Linha; i++)
            {
                Console.BackgroundColor = fundoOriginal;
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

        public static void ImprimirConjunto(HashSet<Peca> conjunto) //usado para imprimir as peças capturadas
        {
            Console.Write("[");
            foreach (Peca x in conjunto)
            {
                Console.Write(x + " ");
            }
            Console.Write("]");
            Console.WriteLine();
        }   
    }
}
