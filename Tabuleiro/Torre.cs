
namespace Tabuleiro
{
    internal class Torre : Peca
    {
        public Torre(Cor cor, tabuleiro tab) : base(cor, tab)
        {
        }
        public override string ToString()
        {
            return "T";
        }
    }
}
