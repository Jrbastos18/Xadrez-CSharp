using System.Runtime.ConstrainedExecution;
using tabuleiro;

namespace xadrez
{
    //Criando a peça Rei do xadrez herdando a superclasse Peca
    class Rei : Peca
    {
        private PartidaDeXadrez partida;

        //Cosntrutor
        public Rei(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor)
        {
            this.partida = partida;
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

        private bool testeTorreParaRoque(Posicao pos) //Método privado que testa se uma torre pode fazer roque
        {
            Peca p = tab.peca(pos);
            return p != null && p is Torre && p.cor == cor && p.qteMovimentos == 0;
            //Retorna verdadeiro se essa peça não for nula, se é uma instância de Torre
            //a cor dessa peça tem quer a mesma cor desse rei e a quantidade de movimentos dessa peça também for zero
        }

        public override bool[,] movimentosPossiveis() //Método da superclasse para verificar movimentos possiveis de cada peça específica
        {
            bool[,] mat = new bool[tab.linhas, tab.colunas]; //Instancia uma matriz booleana do tamanho do tabuleiro

            Posicao pos = new Posicao(0, 0); //Definindo uma posição aleatória

            //Posição Acima 
            pos.definirValores(posicao.linha - 1, posicao.coluna); //Instanciando o método definirValores da classe Posicao para definir movimento possível 
            if (tab.posicaoValida(pos) && podeMover(pos)) //Estrutura condicional para verificar o método se a posição é válida e se o método podeMover for verdadeiro
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

            // #JOGADAESPECIAL Roque
            if (qteMovimentos == 0 && !partida.xeque) //Condição que verifica se o Rei está na condição necessária(se a quantidade de movimentos for zero e a partida não estiver em xeque) para fazer um roque
            {
                // #JogadaEspecial Roque Pequeno
                Posicao posT1 = new Posicao(posicao.linha, posicao.coluna + 3); //Posição da torre
                if (testeTorreParaRoque(posT1)) //Verificando se a posição T1 tenha a Torre e seja elegivel no teste Torre para Roque
                {
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna + 1); //Instanciando as duas posições a direita do Rei
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna + 2);
                    if (tab.peca(p1) == null && tab.peca(p2) == null) //Verifica se as duas casas a direita do rei estejam vagas (nulas)
                    {
                        mat[posicao.linha, posicao.coluna + 2] = true; //Caso as condições sejam verdadeiras, irá definir que a matriz booleana na posição a ser movida é verdadeira e pode ser movida para ela
                    }
                }

                // #JogadaEspecial Roque Grande
                Posicao posT2 = new Posicao(posicao.linha, posicao.coluna - 4); //Posição da torre
                if (testeTorreParaRoque(posT2)) //Verificando se a posição T1 tenha a Torre e seja elegivel no teste Torre para Roque
                {
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna - 1); //Instanciando as três posições a esquerda do Rei
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna - 2);
                    Posicao p3 = new Posicao(posicao.linha, posicao.coluna - 3);
                    if (tab.peca(p1) == null && tab.peca(p2) == null && tab.peca(p3) == null) //Verifica se as duas casas a esquerda do rei estejam vagas (nulas)
                    {
                        mat[posicao.linha, posicao.coluna - 2] = true; //Caso as condições sejam verdadeiras, irá definir que a matriz booleana na posição a ser movida é verdadeira e pode ser movida para ela
                    }
                }
            }


            return mat; //Retornando a matriz booleana
        }
    }
}
