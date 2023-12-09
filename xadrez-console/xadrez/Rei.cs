using tabuleiro;

namespace xadrez
{
    //Criando a peça Rei do xadrez herdando a superclasse Peca
    class Rei : Peca
    {
        public Rei(Tabuleiro tab, Cor cor) : base(tab, cor)
        {                 
        }

        //Métodos
        public override string ToString() //Método para converter classe em string
        {
            return "R";
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

            //Posição Acima 
            pos.definirValores(posicao.linha - 1, posicao.coluna); //Instanciando o método definirValores da classe Posicao para definir movimento possível 
            if(tab.posicaoValida(pos) && podeMover(pos)) //Estrutura condicional para verificar o método se a posição é válida e se o método podeMover for verdadeiro
            {
                mat[pos.linha, pos.coluna] = true; //Caso as condições sejam verdadeiras, irá definir que a matriz booleana na posição a ser movida é verdadeira e pode ser movida para ela
            }
            //Posição Nordeste
            pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            //Posição Direita
            pos.definirValores(posicao.linha, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            //Posição Sudeste
            pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            //Posição Abaixo
            pos.definirValores(posicao.linha + 1, posicao.coluna);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            //Posição Sudoeste
            pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            //Posição Esquerda
            pos.definirValores(posicao.linha, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            //Posição Noroeste
            pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            return mat; //Retornando a matriz booleana
        }
    }
}
