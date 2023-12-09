using tabuleiro;

namespace xadrez
{
    class PosicaoXadrez
    {
        public char coluna { get; set; }
        public int linha { get; set; }

        //Cosntrutor
        public PosicaoXadrez(char coluna, int linha)
        {
            this.coluna = coluna;
            this.linha = linha;
        }

        public Posicao toPosicao()  //Método para converter a posição do xadrez em uma posição na matriz
        {
            return new Posicao(8 - linha, coluna - 'a');
        }

        //Método override ToString para retornar as informações da classe convertidas em string
        public override string ToString()
        {
            return "" + coluna + linha;
        }

    }
}
