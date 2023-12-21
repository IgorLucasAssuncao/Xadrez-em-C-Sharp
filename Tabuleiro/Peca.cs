using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabuleiro
{
    internal abstract class Peca
    {
        public Posicao? Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QtdMovimentos { get; protected set; }
        public tabuleiro tab { get; set; }

        public Peca( Cor cor, tabuleiro tab)
        {
            Posicao = null;
            Cor = cor;
            QtdMovimentos = 0;
            this.tab = tab;
        }
        public void IncrementarQtdMovimentos()
        {
            QtdMovimentos++;
        }
        public void DecrementarQtdMovimentos()
        {
            QtdMovimentos--;
        }
        public abstract bool[,] MovimentosPossiveis(); //Retorna uma matriz de movimentos possíveis

        public abstract bool ExisteMovimentosPossiveis(); //Verifica na matriz gerada pelo "MovimentosPossiveis" se existe algum movimento possível

        public abstract bool ValidaDestino(Posicao pos); //Valida se o destino é valido consultado a matriz de "MovimentosPossiveis"
       

    }
}
