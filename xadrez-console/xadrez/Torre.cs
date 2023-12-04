using tabuleiro;

namespace xadrez
{
    //Criando a peça Torre do xadrez herdando a superclasse Peca
    class Torre : Peca
    {
        public Torre(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
        }

        public override string ToString()
        {
            return "T";
        }
    }
}
