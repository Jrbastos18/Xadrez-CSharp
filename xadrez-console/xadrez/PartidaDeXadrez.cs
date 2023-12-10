using System.Drawing;
using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez
    {
        //Propriedade da partida de Xadrez
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada {  get; private set; }

        //Construtor da classe com as regras de uma partida de Xadrez
        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8); //Iniciando o tabuleiro no tamanho de uma partida de Xadrez 8 x 8
            turno = 1; //Iniciando com o primeiro turno da partida
            jogadorAtual = Cor.Branca; //Definindo o primeiro jogador como na cor Branca (regra do xadrez)
            terminada = false; //Definindo de início que a partida não está terminada, recebendo false
            colocarPecas(); //Método auxiliar para colocar as peças do xadrez na posição padrão inicial
        }

        //Métodos
        public void executaMovimento(Posicao origem, Posicao destino) //método para executar movimento da peça no tabuleiro com posição de origem para a posição de destino
        {
            Peca p = tab.retirarPeca(origem); //Vai no tabuleiro e retira a peça na posição de origem
            p.incrementarQteMovimentos(); //Incrementa a quantidade de movimentos da peça a ser movida
            Peca pecaCapturada = tab.retirarPeca(destino); //Criando uma peça auxiliar para guardar a peça capturada na qual será retirada da posição destino
            tab.colocarPeca(p, destino); //Insere a peça que estava na origem, na posição destino
        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
            executaMovimento(origem, destino);
            turno++;
            mudaJogador();

        }

        public void validarPosicaoDeOrigem(Posicao pos) //Método para validar a posição de origem
        {
            if (tab.peca(pos) == null) //Caso a posição escolhida seja nula, ou seja, não tenha nenhuma peça
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if (jogadorAtual != tab.peca(pos).cor) //Caso a cor dor jogador atual não for igual a peça da posição de origem escolhida
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if (!tab.peca(pos).existeMovimentosPossiveis()) //Caso não exista movimentos possíveis para a peça na posição de origem escolhida
            {
                throw new TabuleiroException("Não há movimentos para a peça de origem escolhida!");
            }

        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino) //Método para validar posição de destino
        {
            if (!tab.peca(origem).podeMoverPara(destino)) //Caso a peça na posição de origem NÃO possa mover para a posição de origem, irá lançar uma exceção
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        private void mudaJogador() //Método para mudar a vez do jogador
        {
            if ( jogadorAtual == Cor.Branca ) //Estrutura condicional para verificar a cor do jogador, alterando a cor do jogador atual
            {
                jogadorAtual = Cor.Preta;
            }
            else
            {
                jogadorAtual = Cor.Branca;
            }
        }

        private void colocarPecas() //Método para inserir as peças no tabuleiro
        {
            //Peças brancas
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('c', 1).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('c', 2).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('d', 2).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('e', 2).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('e', 1).toPosicao());
            tab.colocarPeca(new Rei(tab, Cor.Branca), new PosicaoXadrez('d', 1).toPosicao());

            //Peças pretas
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('c', 7).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('c', 8).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('d', 7).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('e', 7).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('e', 8).toPosicao());
            tab.colocarPeca(new Rei(tab, Cor.Preta), new PosicaoXadrez('d', 8).toPosicao());
        }
    }
}
