using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xadrez;

namespace Tabuleiro
{
    internal class Rei : Peca
    {
        PartidaDeXadrez Partida;
        public Rei(Cor cor, tabuleiro tab, PartidaDeXadrez partida) : base(cor, tab)
        {
            Partida = partida;
        }
        private bool PodeMover(Posicao pos)
        {
            Peca p = tab.Peca(pos);
            return p == null || p.Cor != Cor;
        }
        private bool TesteTorreParaRoque(Posicao pos)
        {
            Peca peca = tab.Peca(pos);

            return peca != null && peca.Cor == Cor && peca is Torre && peca.QtdMovimentos == 0;
        }
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[tab.Linha, tab.Coluna];
            Posicao pos = new Posicao(0, 0);

            // #Jogada especial : Roque
            if (QtdMovimentos == 0 && !Partida.Xeque)
            {
                Posicao Tor1 = new Posicao(Posicao.Linha, Posicao.Coluna + 3);
             
            
                if (TesteTorreParaRoque(Tor1))
                {
                    Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna + 2);

                    if (tab.Peca(p1) == null && tab.Peca(p2) == null)
                    {
                  
                        mat[Posicao.Linha, Posicao.Coluna + 2] = true;
                    }
                }
            }
          
            //  #Jogada especial : Roque Grande
            if (QtdMovimentos == 0 && !Partida.Xeque)
            {
       
                Posicao Tor1 = new Posicao(Posicao.Linha, Posicao.Coluna - 4);
                if (TesteTorreParaRoque(Tor1))
                {
                    Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna - 2);
                    Posicao p3 = new Posicao(Posicao.Linha, Posicao.Coluna - 2);

                    if (tab.Peca(p1) == null && tab.Peca(p2) == null && tab.Peca(p3) == null)
                    { 
                        mat[Posicao.Linha, Posicao.Coluna - 2] = true;
                    }
                }
            }

            //acima
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            if (tab.ValidarPosicao(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            //nordeste
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (tab.ValidarPosicao(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            //direita
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            if (tab.ValidarPosicao(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            //sudeste
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (tab.ValidarPosicao(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            //abaixo
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            if (tab.ValidarPosicao(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            //sudoeste
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (tab.ValidarPosicao(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            //esquerda
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            if (tab.ValidarPosicao(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            //noroeste
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (tab.ValidarPosicao(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            

            return mat;
        }

        public override bool ExisteMovimentosPossiveis()
        {
            bool[,] bools = MovimentosPossiveis();

            for (int i = 0; i < tab.Linha; i++)
            {
                for (int j = 0; j < tab.Coluna; j++)
                {
                    if (bools[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override bool ValidaDestino(Posicao pos)
        {
            return MovimentosPossiveis()[pos.Linha, pos.Coluna];
        }

        public override string ToString()
        {
            return "R";
        }

    }
}
