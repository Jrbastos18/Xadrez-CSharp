namespace tabuleiro
{
    class Tabuleiro
    {
        public int linhas { get; set; }
        public int colunas { get; set; }
        private Peca[,] pecas; //Declarando a propriedade peça como matriz privada da classe para que não seja alterada fora dela

        public Tabuleiro(int linhas, int colunas)
        {
            this.linhas = linhas;//Definindo linhas e colunas
            this.colunas = colunas;
            pecas = new Peca[linhas, colunas];//Instanciando a matriz de peças para o tamanho de linhas por tamanho de colunas
        }

        //Método para retornar a peça em matriz linha por coluna, já que a propriedade é privada
        public Peca peca(int linha, int coluna)
        {
            return pecas[linha, coluna];
        }

        public Peca peca(Posicao pos) //Melhoria criando uma sobrecarga do método peça, recebendo a posição pos
        {
            return pecas[pos.linha, pos.coluna];
        }

        public bool existePeca(Posicao pos) //Método para testar se existe uma peça em uma dada posição
        {
            validarPosicao(pos); //Chamando o método para validar para não dar nenhum erro 
            return peca(pos) != null; //Retorna se a peça, na pos for diferente de nulo
        }

        public void colocarPeca(Peca p, Posicao pos) //Método para incluir peças no tabuleiro
        {
            if (existePeca(pos))
            {
                throw new TabuleiroException("Já existe uma peça nessa posição!");
            }
            pecas[pos.linha, pos.coluna] = p; //Matriz de peças na posição pos.linha por pos.coluna
            p.posicao = pos; //A peça.posição vai receber a pos.
        }

        public bool posicaoValida(Posicao pos) //Método que testa se a posição é válida ou não
        {
            if(pos.linha < 0 || pos.linha >= linhas || pos.coluna < 0 || pos.coluna >= colunas)
            {
                return false;
            }
            return true;
        }

        public void validarPosicao(Posicao pos) //Método para validar a posição e lançar uma exceção personalizada
        {
            if (!posicaoValida(pos))
            {
                throw new TabuleiroException("Posição inválida!");
            }
        }

    }
}
