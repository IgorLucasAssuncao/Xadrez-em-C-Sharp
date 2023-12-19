using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Tabuleiro;

namespace Xadrez
{
    internal class PartidaDeXadrez
    {
        public tabuleiro tab { get; private set;}
        public int Turno { get; protected set; }

        public Cor JogadorAtual { get; protected set; }
        public bool Terminada { get; set; }

        public PartidaDeXadrez()
        {
            tab = new tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            ColocarPecas();
        }
        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            if(tab.ValidarPosicao(origem) == false || tab.ValidarPosicao(destino)==false)
            {
                throw new TabuleiroException("Posição de origem inválida ou destino, inválida!");
            }
            Peca? p = tab.RetirarPeca(origem);
            p.IncrementarQtdMovimentos();
            Peca? pecaCapturada = tab.RetirarPeca(destino);
            tab.ColocarPeca(p, destino);
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            ExecutaMovimento(origem, destino);
            Turno++;
            MudaJogador();
        }

        public void MudaJogador()
        {
            if(JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }else
            {
                JogadorAtual = Cor.Branca;
            }
        }
        public void ValidarPosicaoDeOrigem(Posicao pos)
        {
            if(tab.Peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if(JogadorAtual != tab.Peca(pos).Cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if(tab.Peca(pos).ExisteMovimentosPossiveis() == false)
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if(!tab.Peca(origem).ValidaDestino(destino))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }
       
        private void ColocarPecas()
        {
            tab.ColocarPeca(new Torre(Cor.Branca, tab), new PosicaoXadrez('c', 1).ToPosicao());
            tab.ColocarPeca(new Torre(Cor.Branca, tab), new PosicaoXadrez('c', 2).ToPosicao());
            tab.ColocarPeca(new Torre(Cor.Branca, tab), new PosicaoXadrez('d', 2).ToPosicao());
            tab.ColocarPeca(new Torre(Cor.Branca, tab), new PosicaoXadrez('e', 1).ToPosicao());
            tab.ColocarPeca(new Rei(Cor.Branca, tab), new PosicaoXadrez('e', 2).ToPosicao());
        }

    }
}
