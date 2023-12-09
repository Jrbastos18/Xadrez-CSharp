namespace tabuleiro
{
    class Posicao
    {
        public int linha { get; set; }
        public int coluna { get; set; }

        //Construtor
        public Posicao(int linha, int coluna)
        {
            this.linha = linha;
            this.coluna = coluna;
        }

        public void definirValores(int linha, int coluna) //Método para definir valores da posição
        {
            this.linha = linha;
            this.coluna = coluna;
        }

        //método override ToString para transformar as propriedades da classe em string para imprimir na tela
        public override string ToString() 
        {
            return linha
                + ", "
                + coluna;
        }
    }
}
