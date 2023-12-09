using tabuleiro;
using xadrez;


namespace xadrez_console
{
    class Tela
    {
        //Método para imprimir o tabuleiro
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            //Estrutura de repetição dupla com i e j, representando a linha e coluna respectivamente
            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write(8 - i + " "); //Imprimindo da tela cada linha do tabuleiro de forma decrescente
                for (int j = 0; j < tab.colunas; j++)
                {
                    //Estrutura condicional para verificar se a posição da matriz é nula ou contém uma peça
                    if (tab.peca(i,j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        imprimirPeca(tab.peca(i, j));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h");
        }

        public static PosicaoXadrez lerPosicaoXadrez() //Método para ler do teclado a posição do xadrez digitada pelo usuário
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);
        }

        public static void imprimirPeca(Peca peca) //Método estático para mudar cor da peça
        {
            if (peca.cor == Cor.Branca) //Caso seja branca, continuará normal
            {
                Console.Write(peca);
            }
            else //Caso seja preta, ela mudará para a cor amarela
            {
                ConsoleColor aux = Console.ForegroundColor; //Criando uma variável auxiliar para receber a cor original do console (branca)
                Console.ForegroundColor = ConsoleColor.Yellow; //Definindo a cor dor primeiro plano (string) do console para amarelo
                Console.Write(peca); //Imprimindo a peça na cor amarela
                Console.ForegroundColor = aux; //Voltando a cor original branca para o primeiro plano (string)
            }
        }

    }
}
