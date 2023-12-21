using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Tabuleiro;

namespace Xadrez
{
    internal class PartidaDeXadrez
    {
        public tabuleiro tab { get; private set; } //Tabuleiro que vai ser manipulado
        public int Turno { get; protected set; }

        public Cor JogadorAtual { get; protected set; } //Controle de vez
        public bool Terminada { get; set; } //Fim de partida

        private HashSet<Peca> pecas; //Todas as peças presente no jogo
        private HashSet<Peca> capturadas; //Todas as peças capturadas

        public bool Xeque = false;

        public Peca? VulneravelEnPassant;

        public PartidaDeXadrez()
        {
            tab = new tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            VulneravelEnPassant = null;
            ColocarPecas();
        }



        public void RealizaJogada(Posicao origem, Posicao destino)
        {

            Peca pecaCapturada = ExecutaMovimento(origem, destino);

            //Depois de executar o movimento, o jogador está em xeque? 
            //Se estiver, desfaz o movimento e lanã exceção
            if (estaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            Peca p = tab.Peca(destino);
            //Promoção

            if (p is Peao)
            {
                if (destino.Linha == 0 && p.Cor == Cor.Branca || destino.Linha == 7 && p.Cor == Cor.Preta)
                {
                    p = tab.RetirarPeca(destino);
                    pecas.Remove(p);

                    Peca dama = new Rainha(p.Cor, tab);

                    tab.ColocarPeca(dama, destino);
                    pecas.Add(dama);
                    ;
                }
            }


            //Verifica se o movimento coloca o adversário em xeque
            if (estaEmXeque(adversaria(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }

            //Verifica se o movimento coloca o adversário em xeque mate
            if (XequeMate(adversaria(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                MudaJogador();
            }



            //Jogada especial En Passant
            if (p is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))//2 linhas a mais ou a menos pq pode ser preta ou branca
            {
                VulneravelEnPassant = p;
            }
            else
            {
                VulneravelEnPassant = null;
            }
        }
        private void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca? p = tab.RetirarPeca(destino);
            p.DecrementarQtdMovimentos();
            if (pecaCapturada != null)
            {
                tab.ColocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.ColocarPeca(p, origem);

            //Roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao DestinoT = new Posicao(origem.Linha, origem.Coluna - 1);

                Peca T = tab.RetirarPeca(DestinoT);
                T.DecrementarQtdMovimentos();
                tab.ColocarPeca(T, origemT);
            }

            //Roque 
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao DestinoT = new Posicao(origem.Linha, origem.Coluna + 1);

                Peca T = tab.RetirarPeca(DestinoT);
                T.IncrementarQtdMovimentos();
                tab.ColocarPeca(T, origemT);
            }

            //En Passant

            if (p is Peao)
            {
                if (origem.Coluna == destino.Coluna && pecaCapturada == VulneravelEnPassant)
                {
                    Peca peao = tab.RetirarPeca(destino);
                    Posicao posP;

                    if (peao.Cor == Cor.Branca)
                    {
                        posP = new Posicao(3, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(4, destino.Coluna);
                    }

                    tab.ColocarPeca(peao, posP);
                }
            }
        }


        public Peca ExecutaMovimento(Posicao origem, Posicao destino) //Faz a troca de peças
        {
            if (tab.ValidarPosicao(origem) == false || tab.ValidarPosicao(destino) == false) //Valida se são posições de dentro da tabela
            {
                throw new TabuleiroException("Posição de origem inválida ou destino, inválida!");
            }
            Peca? p = tab.RetirarPeca(origem); //Retira a peça da posição de origem (A peça que vai ser movida)
            p.IncrementarQtdMovimentos(); //Incrementa a quantidade de movimentos da peça
            Peca? pecaCapturada = tab.RetirarPeca(destino); //Retira a peça da posição de destino (A peça que vai ser capturada)
            tab.ColocarPeca(p, destino); //Coloca a peça da origem na posição de destino

            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }

            //Roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao DestinoT = new Posicao(origem.Linha, origem.Coluna - 1);

                Peca T = tab.RetirarPeca(origemT);
                T.IncrementarQtdMovimentos();
                tab.ColocarPeca(T, DestinoT);
            }

            //Roque 
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao DestinoT = new Posicao(origem.Linha, origem.Coluna + 1);

                Peca T = tab.RetirarPeca(origemT);
                T.IncrementarQtdMovimentos();
                tab.ColocarPeca(T, DestinoT);
            }

            //Jogada especial En Passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == null)
                {
                    Posicao Peao;
                    if (p.Cor == Cor.Branca)
                    {
                        if (origem.Coluna - 1 == destino.Coluna)
                        {
                            Peao = new Posicao(origem.Linha, origem.Coluna - 1);
                        }
                        else
                        {
                            Peao = new Posicao(origem.Linha, origem.Coluna + 1);
                        }
                    }
                    else
                    {
                        if (origem.Coluna - 1 == destino.Coluna)
                        {
                            Peao = new Posicao(origem.Linha, origem.Coluna - 1);
                        }
                        else
                        {
                            Peao = new Posicao(origem.Linha, origem.Coluna + 1);
                        }
                    }
                    capturadas.Add(tab.RetirarPeca(Peao));
                }
            }




            return pecaCapturada;
        }
        private Cor adversaria(Cor cor)
        {
            if (Cor.Branca == cor)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }
        private Peca? rei(Cor cor)
        {
            foreach (Peca x in pecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }
        public bool estaEmXeque(Cor cor) //Cor do oponente que pode estar em xeque 
        {

            foreach (Peca x in pecasEmJogo(adversaria(cor))) //Todas as peças do oponente (se entrada = branca, vai pegar as peças pretas)
            {

                bool[,] mat = x.MovimentosPossiveis(); //Matriz de movimentos possíveis para cada peça

                Peca? r = rei(cor); //Rei da cor de entrada
                if (mat[r.Posicao.Linha, r.Posicao.Coluna]) //identifica se existe movimento possível para o rei
                {
                    // se sim, está em xeque
                    return true;
                }
            }
            //Se não, não está em xeque
            return false;
        }
        public bool XequeMate(Cor cor)
        {
            if (!estaEmXeque(cor))
            {
                return false;
            }

            foreach (Peca x in pecasEmJogo(cor))
            {
                bool[,] mat = x.MovimentosPossiveis();

                for (int i = 0; i < tab.Linha; i++)//Testa se existe movimento possível para tirar do Xeque
                {
                    for (int j = 0; j < tab.Coluna; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            bool testeXeque = estaEmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
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
            if (JogadorAtual != tab.Peca(pos).Cor) //Verificação que só deixar a cor atual jogar
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if (tab.Peca(pos).ExisteMovimentosPossiveis() == false) //Verifica se a peça está sem movimentos
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!tab.Peca(origem).ValidaDestino(destino)) //Na peça de origem, verifica se a posição de destino é válida (Matriz de booleanos)
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
            foreach (Peca x in capturadas)
            {
                if (x.Cor == cor)
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
            colocarNovaPeca('a', 1, new Torre(Cor.Branca, tab));
            colocarNovaPeca('b', 1, new Cavalo(Cor.Branca, tab));
            colocarNovaPeca('c', 1, new Bispo(Cor.Branca, tab));
            colocarNovaPeca('d', 1, new Rainha(Cor.Branca, tab));
            colocarNovaPeca('e', 1, new Rei(Cor.Branca, tab, this));
            colocarNovaPeca('f', 1, new Bispo(Cor.Branca, tab));
            colocarNovaPeca('g', 1, new Cavalo(Cor.Branca, tab));
            colocarNovaPeca('h', 1, new Torre(Cor.Branca, tab));
            colocarNovaPeca('a', 2, new Peao(Cor.Branca, tab, this));
            colocarNovaPeca('b', 2, new Peao(Cor.Branca, tab, this));
            colocarNovaPeca('c', 2, new Peao(Cor.Branca, tab, this));
            colocarNovaPeca('d', 2, new Peao(Cor.Branca, tab, this));
            colocarNovaPeca('e', 2, new Peao(Cor.Branca, tab, this));
            colocarNovaPeca('f', 2, new Peao(Cor.Branca, tab, this));
            colocarNovaPeca('g', 2, new Peao(Cor.Branca, tab, this));
            colocarNovaPeca('h', 2, new Peao(Cor.Branca, tab, this));

            colocarNovaPeca('a', 8, new Torre(Cor.Preta, tab));
            colocarNovaPeca('b', 8, new Cavalo(Cor.Preta, tab));
            colocarNovaPeca('c', 8, new Bispo(Cor.Preta, tab));
            colocarNovaPeca('d', 8, new Rainha(Cor.Preta, tab));
            colocarNovaPeca('e', 8, new Rei(Cor.Preta, tab, this));
            colocarNovaPeca('f', 8, new Bispo(Cor.Preta, tab));
            colocarNovaPeca('g', 8, new Cavalo(Cor.Preta, tab));
            colocarNovaPeca('h', 8, new Torre(Cor.Preta, tab));
            colocarNovaPeca('a', 7, new Peao(Cor.Preta, tab, this));
            colocarNovaPeca('b', 7, new Peao(Cor.Preta, tab, this));
            colocarNovaPeca('c', 7, new Peao(Cor.Preta, tab, this));
            colocarNovaPeca('d', 7, new Peao(Cor.Preta, tab, this));
            colocarNovaPeca('e', 7, new Peao(Cor.Preta, tab, this));
            colocarNovaPeca('f', 7, new Peao(Cor.Preta, tab, this));
            colocarNovaPeca('g', 7, new Peao(Cor.Preta, tab, this));
            colocarNovaPeca('h', 7, new Peao(Cor.Preta, tab, this));




        }

    }
}
