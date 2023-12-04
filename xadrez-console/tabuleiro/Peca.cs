namespace tabuleiro
{
    internal class Peca
    {
        public Posicao posicao { get; set; }
        public Cor cor { get; protected set; } //O protected significa que o atributo será acessível pela própria classe e pelas subclasses
        public int qteMovimentos { get; set; }
        public Tabuleiro tab { get; set; } //Declarando a classe tabuleiro como propriedade da classe peça

        //Construtor usando o this para variaveis que iniciam com letra minuscula
        public Peca(Tabuleiro tab, Cor cor)
        {
            this.posicao = null; //Quando inicia a peça, a sua posição é nula
            this.tab = tab;
            this.cor = cor;
            this.qteMovimentos = 0;//Instanciando que a peça inicia com 0 e não precisa estar na sobrecarga
        }


    }
}
