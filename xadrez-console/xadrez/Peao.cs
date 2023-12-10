using tabuleiro;

namespace xadrez
{
    //Criando a peça Peão do xadrez herdando a superclasse Peca
    class Peao : Peca
    {
        public Peao(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
        }

        //Métodos
        public override string ToString() //Método para converter classe em string
        {
            return "P";
        }

        private bool existeInimigo(Posicao pos) //Método para verificar se existe inimigo na posição pos
        {
            Peca p = tab.peca(pos);
            return p != null && p.cor != cor; //RQuando a peça quando não é nula e a cor é diferente da cor de peça atual
        }

        private bool livre(Posicao pos) //Método para verificar se a posição está livre
        {
            return tab.peca(pos) == null; //Livre quando a peça é nula
        }

        public override bool[,] movimentosPossiveis() //Método da superclasse para verificar movimentos possiveis de cada peça específica
        {
            bool[,] mat = new bool[tab.linhas, tab.colunas]; //Instancia uma matriz booleana do tamanho do tabuleiro

            Posicao pos = new Posicao(0, 0); //Definindo uma posição aleatória

            if (cor == Cor.Branca) //Se a cor for branca o peão apenas verifica para cima
            {
                pos.definirValores(posicao.linha - 1, posicao.coluna); 
                if (tab.posicaoValida(pos) && livre(pos)) //Se a casa exister livre ele anda 1 para cima
                {
                    mat[pos.linha, pos.coluna] = true; //Caso as condições sejam verdadeiras, irá definir que a matriz booleana na posição a ser movida é verdadeira e pode ser movida para ela
                }
                pos.definirValores(posicao.linha - 2, posicao.coluna);
                if (tab.posicaoValida(pos) && livre(pos) && qteMovimentos == 0) //Se a casa exister livre e quantidade de movimentos for 0 (o primeiro movimento do peão), ele anda 2 casas para cima
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha - 1, posicao.coluna - 1); //Caso exista um inimigo na diagonal esquerda ele pode andar e capturar o inimigo nessa posição
                if (tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha - 1, posicao.coluna + 1); //Caso exista um inimigo na diagonal direita ele pode andar e capturar o inimigo nessa posição
                if (tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
            }
            else //Se a cor for preta o peão apenas verifica para baixo
            {
                pos.definirValores(posicao.linha + 1, posicao.coluna);
                if (tab.posicaoValida(pos) && livre(pos)) //Se a casa exister livre ele anda 1 para cima
                {
                    mat[pos.linha, pos.coluna] = true; //Caso as condições sejam verdadeiras, irá definir que a matriz booleana na posição a ser movida é verdadeira e pode ser movida para ela
                }
                pos.definirValores(posicao.linha + 2, posicao.coluna);
                if (tab.posicaoValida(pos) && livre(pos) && qteMovimentos == 0) //Se a casa exister livre e quantidade de movimentos for 0 (o primeiro movimento do peão), ele anda 2 casas para cima
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha + 1, posicao.coluna - 1); //Caso exista um inimigo na diagonal esquerda ele pode andar e capturar o inimigo nessa posição
                if (tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha + 1, posicao.coluna + 1); //Caso exista um inimigo na diagonal direita ele pode andar e capturar o inimigo nessa posição
                if (tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
            }

            return mat; //Retornando a matriz booleana
        }
    }
}
