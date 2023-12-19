using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Tabuleiro;

namespace Xadrez
{
    internal class PartidaDeXadrez
    {
        public tabuleiro tab { get; private set;}
        private int Turno { get; set; }
        private Cor JogadorAtual { get; set; }
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
