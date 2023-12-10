using tabuleiro;

namespace xadrez
{
    //Criando a peça Bispo do xadrez herdando a superclasse Peca
    class Bispo : Peca
    {
        //Construtor
        public Bispo(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
        }

        //Métodos
        public override string ToString() //Método para converter classe em string
        {
            return "B";
        }

        private bool podeMover(Posicao pos) //Método auxiliar para verificar se a peça poderá se movimentar
        {
            Peca p = tab.peca(pos);
            return p == null || p.cor != cor; //Retorna verdade se a posição for nula (sem nenhuma peça nela) ou se a peça que estiver na posição for oposta da peça a ser movimentada
        }

        public override bool[,] movimentosPossiveis() //Método da superclasse para verificar movimentos possiveis de cada peça específica
        {
            bool[,] mat = new bool[tab.linhas, tab.colunas]; //Instancia uma matriz booleana do tamanho do tabuleiro

            Posicao pos = new Posicao(0, 0); //Definindo uma posição aleatória

            //Posição Noroeste
            pos.definirValores(posicao.linha - 1, posicao.coluna - 1); //Instanciando o método definirValores da classe Posicao para definir movimento possível
            while (tab.posicaoValida(pos) && podeMover(pos)) //Estrutura de repetição para marcar a posição enquanto o estiver casa livre ou peça adversária
            {
                mat[pos.linha, pos.coluna] = true; //Caso as condições sejam verdadeiras, irá definir que a matriz booleana na posição a ser movida é verdadeira e pode ser movida para ela
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) //Estrutura de condição para forçar a parada caso bata na peça adversária
                {
                    break;
                }
                pos.definirValores(pos.linha - 1, pos.coluna - 1); //Caso não passe na condição, vai verificando a próxima posição
            }

            //Posição Nordeste
            pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                    break;
                }
                pos.definirValores(pos.linha - 1, pos.coluna + 1);
            }

            //Posição Sudeste
            pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                    break;
                }
                pos.definirValores(pos.linha + 1, pos.coluna + 1);
            }

            //Posição Sudoeste
            pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                    break;
                }
                pos.definirValores(pos.linha + 1, pos.coluna - 1);
            }
            return mat; //Retornando a matriz booleana
        }

    }
}
