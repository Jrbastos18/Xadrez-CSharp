namespace tabuleiro
{
    class Tabuleiro
    {
        public int linhas { get; set; }
        public int colunas { get; set; }
        private Peca[,] pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            this.linhas = linhas;//Definindo linhas e colunas
            this.colunas = colunas;
            pecas = new Peca[linhas, colunas];//Instanciando a matriz de peças para o tamanho de linhas por tamanho de colunas
        }
    }
}
