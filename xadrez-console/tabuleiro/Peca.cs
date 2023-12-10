namespace tabuleiro
{
    abstract class Peca //A classe tem que ser abstrata pois tem pelo menos um método abstrato
    {
        public Posicao posicao { get; set; }
        public Cor cor { get; protected set; } //O protected significa que o atributo será acessível pela própria classe e pelas subclasses
        public int qteMovimentos { get; protected set; }
        public Tabuleiro tab { get; protected set; } //Declarando a classe tabuleiro como propriedade da classe peça

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

        public bool existeMovimentosPossiveis() //Método para verificar se existe movimentos possíveis para a peça
        {
            bool[,] mat = movimentosPossiveis(); //Instanciando uma matriz booleana recebendo o método de movimentos possíveis
            for (int i = 0; i < tab.linhas; i++)
            {
                for (int j = 0; j < tab.colunas; j++)
                {
                    if (mat[i, j]) //Caso a matriz na posição ij for verdadeira com movimento possível, retornará true
                    {
                        return true;
                    }
                }
            }
            return false; //Caso saia da estrutura de repetição e não retorne nenhum verdadeiro, o método retornará false
        }

        public bool podeMoverPara(Posicao pos) //Método para saber se a peça pode mover para uma dada posição
        {
            return movimentosPossiveis()[pos.linha, pos.coluna]; //retorna os movimentos possíveis, testando se a a matriz na linha e coluna é verdadeiro
        }

        public abstract bool[,] movimentosPossiveis(); //Método abstrato de matriz booleana para retornar valores verdadeiros para movimentos possiveis e falso onde não for
                                                       //Esse método acima não será utilizado na superclasse Peca, apenas nas subclasses herdadas da superclasse, pois a regra de movimentos depende de cada peça específica
    }
}
