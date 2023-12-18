using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabuleiro
{
    public class tabuleiro
    {
        public int Coluna { get; set; }
        public int Linha { get; set; }
        private Peca[,] Pecas;


        public tabuleiro(int linha, int coluna)
        {
            Coluna = coluna;
            Linha = linha;
            Pecas = new Peca[Linha, Coluna];
        }

        #region Funções de Peças (Retornam as peças) PS: Utilizadas por métodos de outras classes
        internal Peca Peca(int linha, int coluna)
        {
            return Pecas[linha, coluna]; // retorna a peça na posição linha e coluna (Se usada numa String retorna o To String do Objeto)
        }


        internal Peca Peca(Posicao pos)
        {
            return Pecas[pos.Linha, pos.Coluna];
        }
        #endregion

        #region Funções de Validação
        internal bool ValidarPosicao(Posicao pos)
        {
            if (pos.Linha < 0 || pos.Linha >= Linha || pos.Coluna < 0 || pos.Coluna >= Coluna)
            {
                return false;
            }
            return true;
        }
        internal bool ExistePeca(Posicao pos)
        {
            bool val = ValidarPosicao(pos);
            if (val)
            {
                return Peca(pos) != null;
            }else
            {
                throw new TabuleiroException("Posição Inválida!");
            }
            
        }
        #endregion  

        internal void ColocarPeca(Peca peca, Posicao pos)
        {
            if (ExistePeca(pos))
            {
             throw new TabuleiroException("Já existe uma peça nessa posição!");
            }
            Pecas[pos.Linha, pos.Coluna] = peca;
            peca.Posicao = pos;

        }
    }
}
