using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xadrez;

namespace Tabuleiro
{
    internal class Peao : Peca
    {
        PartidaDeXadrez partida;
        public Peao(Cor cor, tabuleiro tab, PartidaDeXadrez partida) : base(cor, tab)
        {
            this.partida = partida;
        }
        private bool PodeMover(Posicao pos)
        {
            Peca p = tab.Peca(pos);
            return p == null || p.Cor != Cor;
        }
        private bool Livre(Posicao pos)
        {
            return tab.Peca(pos) == null;
        }
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[tab.Linha, tab.Coluna];
            Posicao pos = new Posicao(0, 0);

            if(Cor == Cor.Branca)
            {

                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
                if (tab.ValidarPosicao(pos) && Livre(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
                if (tab.ValidarPosicao(pos) && Livre(pos) && QtdMovimentos == 0)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                if (tab.ValidarPosicao(pos) && PodeMover(pos) && tab.Peca(pos) != null)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                if (tab.ValidarPosicao(pos) && PodeMover(pos) && tab.Peca(pos) != null)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                //En Passant Brancas

                if(Posicao.Linha == 3)
                {
                    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if (tab.ValidarPosicao(esquerda) && PodeMover(esquerda) && tab.Peca(esquerda) == partida.VulneravelEnPassant)
                    {
                        mat[esquerda.Linha - 1, esquerda.Coluna] = true;
                    }
                    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if (tab.ValidarPosicao(direita) && PodeMover(direita) && tab.Peca(direita) == partida.VulneravelEnPassant)
                    {
                        mat[direita.Linha - 1, direita.Coluna] = true;
                    }
                }

            }
            else
            {

                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
                if (tab.ValidarPosicao(pos) && Livre(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
                if (tab.ValidarPosicao(pos) && Livre(pos) && QtdMovimentos == 0)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                if (tab.ValidarPosicao(pos) && PodeMover(pos) && tab.Peca(pos) != null)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                if (tab.ValidarPosicao(pos) && PodeMover(pos) && tab.Peca(pos) != null)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                //En Passant Pretas

                if (Posicao.Linha == 4)
                {
                    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if (tab.ValidarPosicao(esquerda) && PodeMover(esquerda) && tab.Peca(esquerda) == partida.VulneravelEnPassant)
                    {
                        mat[esquerda.Linha - 1, esquerda.Coluna] = true;
                    }

                    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if (tab.ValidarPosicao(direita) && PodeMover(direita) && tab.Peca(direita) == partida.VulneravelEnPassant)
                    {
                        mat[direita.Linha + 1, direita.Coluna] = true;
                    }
                }

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
            return "P";
        }

    }
}
