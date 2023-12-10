using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaDeXadrez partida = new PartidaDeXadrez();

                while (!partida.terminada) //Estrutura de repetição enquanto a partida não estiver terminada
                {
                    try
                    {
                        Console.Clear(); //Limpa a tela
                        Tela.ImprimirTabuleiro(partida.tab); //Imprime tabuleiro
                        Console.WriteLine();
                        Console.WriteLine("Turno: " + partida.turno);
                        Console.WriteLine("Aguardando jogada: " + partida.jogadorAtual);

                        Console.WriteLine();
                        Console.Write("Origem: ");
                        Posicao origem = Tela.lerPosicaoXadrez().toPosicao(); //Instanciando o método para ler do teclado a posição de origme do xadrez e transformar para posição de matriz
                        partida.validarPosicaoDeOrigem(origem);

                        bool[,] posicoesPossiveis = partida.tab.peca(origem).movimentosPossiveis(); //Instanciando uma matriz booleana recbendo a partida com o tabuleiro dela, a peça na posição de origem e seus movimentos possíveis 


                        Console.Clear(); //Limpa a tela
                        Tela.ImprimirTabuleiro(partida.tab, posicoesPossiveis); //Imprime tabuleiro com sobrecarga matriz booleana de possições possíveis

                        Console.WriteLine();
                        Console.Write("Destino: ");
                        Posicao destino = Tela.lerPosicaoXadrez().toPosicao(); //Instanciando o método para ler do teclado a posição de destino do xadrez e transformar para posição de matriz
                        partida.validarPosicaoDeDestino(origem, destino); //Validando a posição de destino

                        partida.realizaJogada(origem, destino);
                    }
                    catch (TabuleiroException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }

                Tela.ImprimirTabuleiro(partida.tab);
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();


        }
    }
}