using Tabuleiro;

namespace Xadrez
{
    internal class PosicaoXadrez //Faz a conversão de uma posição do xadrez para uma posição de matriz
    {
        public char Coluna { get; set; }
        public int Linha { get; set; }
        
        public PosicaoXadrez(char coluna, int linha)
        {
            Coluna = coluna;
            Linha = linha;
        }
        public Posicao ToPosicao()
        {
            return new Posicao(8 - Linha, Coluna - 'a');
        }
        new public string ToString()
        {
            return "" + Coluna + Linha;
        }
    }
}
