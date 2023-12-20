using System;
using Tabuleiro;

namespace Xadrez
{
    class Principal
    {
        public static void Main()
        {
            try
            {
                PartidaDeXadrez partida = new PartidaDeXadrez();
               
                while(partida.Terminada == false)
                {
                    try
                    {
                        
                        Tela.ImprimirPartida(partida);
                        Console.Write("Origem: ");
                        Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();

                        partida.ValidarPosicaoDeOrigem(origem);  //verifica se a posição de origem é válida

                        bool[,] posicoesPossiveis = partida.tab.Peca(origem).MovimentosPossiveis(); //cria uma matriz de booleanos para armazenar as posições possíveis de movimento da peça

                        Console.Clear();
                        Tela.ImprimirTabuleiro(partida.tab, posicoesPossiveis); //imprime o tabuleiro com as posições possíveis de movimento da peça

                        Console.Write("Destino: ");
                        Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();

                        partida.ValidarPosicaoDeDestino(origem, destino);
                        partida.RealizaJogada(origem, destino);
                    }
                    catch(TabuleiroException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
                Console.WriteLine("XequeMate");
                Console.WriteLine(partida.JogadorAtual + " Venceu!");
            }catch(TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
         

        }
    }
}