namespace tabuleiro
{
    abstract class Peca //A classe tem que ser abstrata pois tem pelo menos um método abstrato
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

        public void incrementarQteMovimentos() //Método para incrementar a contagem da quantidade de movimentos da peça
        {
            qteMovimentos++;
        }

        public abstract bool[,] movimentosPossiveis(); //Método abstrato de matriz booleana para retornar valores verdadeiros para movimentos possiveis e falso onde não for
        //Esse método acima não será utilizado na superclasse Peca, apenas nas subclasses herdadas da superclasse, pois a regra de movimentos depende de cada peça específica

        
    }
}
