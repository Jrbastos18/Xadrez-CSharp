using tabuleiro;

namespace xadrez
{
    //Criando a peça Rei do xadrez herdando a superclasse Peca
    class Rei : Peca
    {
        public Rei(Tabuleiro tab, Cor cor) : base(tab, cor)
        {                 
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
