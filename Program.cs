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
                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.tab);
                    Console.WriteLine();
                    Console.Write("Origem: ");
                    Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
                    Console.Write("Destino: ");
                    Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();
                    partida.ExecutaMovimento(origem, destino);
                }
            }catch(TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
         

        }
    }
}