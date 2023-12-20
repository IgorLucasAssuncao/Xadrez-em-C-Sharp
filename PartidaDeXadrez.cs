using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Tabuleiro;

namespace Xadrez
{
    internal class PartidaDeXadrez
    {
        public tabuleiro tab { get; private set; }
        public int Turno { get; protected set; }

        public Cor JogadorAtual { get; protected set; }
        public bool Terminada { get; set; }

        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;

        public bool Xeque = false;

        public PartidaDeXadrez()
        {
            tab = new tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            ColocarPecas();
        }
        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            
            Peca pecaCapturada = ExecutaMovimento(origem, destino);
            if(estaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }
            if(estaEmXeque(adversaria(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }

            if (XequeMate(adversaria(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                MudaJogador();
            } 
        }
        private void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.RetirarPeca(destino);
            p.DecrementarQtdMovimentos();
            if(pecaCapturada != null)
            {
                tab.ColocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.ColocarPeca(p, origem);
        }


        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            if (tab.ValidarPosicao(origem) == false || tab.ValidarPosicao(destino) == false)
            {
                throw new TabuleiroException("Posição de origem inválida ou destino, inválida!");
            }
            Peca? p = tab.RetirarPeca(origem);
            p.IncrementarQtdMovimentos();
            Peca? pecaCapturada = tab.RetirarPeca(destino);
            tab.ColocarPeca(p, destino);

            if(pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            } 
            return pecaCapturada;
        } 
        private Cor adversaria(Cor cor)
        {
            if(Cor.Branca == cor)
            {
                return Cor.Preta;
            }else
            {
                return Cor.Branca;
            }
        }
        private Peca? rei(Cor cor)
        {
            foreach(Peca x in pecasEmJogo(cor))
            {
                if(x is Rei)
                {
                    return x;
                }
            }
            return null;
        }
        public bool estaEmXeque(Cor cor)
        {
            foreach(Peca x in pecasEmJogo(adversaria(cor)))
            {
                bool[,] mat = x.MovimentosPossiveis();
                Peca? r = rei(cor);
                if (mat[r.Posicao.Linha, r.Posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }
        public bool XequeMate(Cor cor)
        {
            if(!estaEmXeque(cor))
            {
                return false;
            }

            foreach(Peca x in pecasEmJogo(cor))
            {

                bool[,] mat = x.MovimentosPossiveis();
                for(int i = 0; i < tab.Linha; i++)
                {
                    for(int j = 0; j < tab.Coluna; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            bool testeXeque = estaEmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if(!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }

                
            }
            return true;

        }
        public void MudaJogador()
        {
            if (JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }
        public void ValidarPosicaoDeOrigem(Posicao pos)
        {
            if (tab.Peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if (JogadorAtual != tab.Peca(pos).Cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if (tab.Peca(pos).ExisteMovimentosPossiveis() == false)
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!tab.Peca(origem).ValidaDestino(destino))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }
        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            pecas.Add(peca);

        }
        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in capturadas)
            {
                if(x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }
        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }
        private void ColocarPecas()
        {
            colocarNovaPeca('c', 1, new Torre(Cor.Branca, tab));
            colocarNovaPeca('c', 2, new Torre(Cor.Branca, tab));
            colocarNovaPeca('d', 2, new Torre(Cor.Branca, tab));
            colocarNovaPeca('e', 1, new Torre(Cor.Branca, tab));
            colocarNovaPeca('d', 1, new Rei(Cor.Branca, tab));
            colocarNovaPeca('e', 2, new Torre(Cor.Branca, tab));

            colocarNovaPeca('c', 8, new Torre(Cor.Preta, tab));
            colocarNovaPeca('c', 7, new Torre(Cor.Preta, tab));
            colocarNovaPeca('d', 7, new Torre(Cor.Preta, tab));
            colocarNovaPeca('e', 8, new Torre(Cor.Preta, tab));
            colocarNovaPeca('d', 8, new Rei(Cor.Preta, tab));
            colocarNovaPeca('e', 7, new Torre(Cor.Preta, tab));



        }

    }
}
