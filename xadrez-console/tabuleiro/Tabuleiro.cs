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

    }
}
