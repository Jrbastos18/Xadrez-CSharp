using tabuleiro;

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
                for (int j = 0; j < tab.colunas; j++)
                {
                    //Estrutura condicional para verificar se a posição da matriz é nula ou contém uma peça
                    if (tab.peca(i,j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write(tab.peca(i, j) + " ");
                    }
                }
                Console.WriteLine();
            }
        }

    }
}
