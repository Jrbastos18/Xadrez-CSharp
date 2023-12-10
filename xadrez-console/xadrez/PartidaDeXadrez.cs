using System.Collections.Generic;
using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez
    {
        //Propriedade da partida de Xadrez
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;

        //Construtor da classe com as regras de uma partida de Xadrez
        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8); //Iniciando o tabuleiro no tamanho de uma partida de Xadrez 8 x 8
            turno = 1; //Iniciando com o primeiro turno da partida
            jogadorAtual = Cor.Branca; //Definindo o primeiro jogador como na cor Branca (regra do xadrez)
            terminada = false; //Definindo de início que a partida não está terminada, recebendo false
            pecas = new HashSet<Peca>(); //Instanciando um conjunto de peças
            capturadas = new HashSet<Peca>(); //Instanciando um conjunto de peças capturadas
            colocarPecas(); //Método auxiliar para colocar as peças do xadrez na posição padrão inicial
        }

        //Métodos
        public void executaMovimento(Posicao origem, Posicao destino) //método para executar movimento da peça no tabuleiro com posição de origem para a posição de destino
        {
            Peca p = tab.retirarPeca(origem); //Vai no tabuleiro e retira a peça na posição de origem
            p.incrementarQteMovimentos(); //Incrementa a quantidade de movimentos da peça a ser movida
            Peca pecaCapturada = tab.retirarPeca(destino); //Criando uma peça auxiliar para guardar a peça capturada na qual será retirada da posição destino
            tab.colocarPeca(p, destino); //Insere a peça que estava na origem, na posição destino
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada); //Condição se foi capturada alguma peça, acrescenta a peça no conjunto de capturadas
            }
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
            if (jogadorAtual == Cor.Branca) //Estrutura condicional para verificar a cor do jogador, alterando a cor do jogador atual
            {
                jogadorAtual = Cor.Preta;
            }
            else
            {
                jogadorAtual = Cor.Branca;
            }
        }

        public HashSet<Peca> pecasCapturadas(Cor cor) //Método para fazer um conjunto e retornar peças capturadas para cores específicas
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas) //Para cada (percorre) peca x no conjunto capturadas, verifica se a peça cor é igual a mesma cor do parametro cor e adiciona no conjunto auxiliar
            {
                if(x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux; //Retorna o conjunto auxiliar de peças capturadas
        }

        public HashSet<Peca> pecasEmJogo(Cor cor) //Método para fazer um conjunto e retornar peças que ainda estão em jogo
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas) //Para cada (percorre) peca x no conjunto jogo, verifica se a peça cor é igual a mesma cor do parametro cor e adiciona no conjunto auxiliar
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor)); //Conjunto auxiliar recebe as peças exceto (retirar) as peças que estão no método peças capturadas 
            return aux; //Retorna o conjunto auxiliar de peças em jogo
        }   

        public void colocarNovaPeca(char coluna, int linha, Peca peca) //Método auxiliar para colocar novas peças no tabuleiro
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao()); //Dado uma coluna e uma linha, coloca no tabuleiro essa peça numa nova posição xadrez
            pecas.Add(peca); //Adicionando essa peça no conjunto de peças da partida
        }

        private void colocarPecas() //Método para inserir as peças no tabuleiro
        {
            //Chamando o método auxiliar de ColocarNovaPeça para inserir a peça no tabuleiro
            //Peças brancas
            colocarNovaPeca('c', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('c', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('d', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('e', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Rei(tab, Cor.Branca));

            //Peças pretas
            colocarNovaPeca('c', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('d', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('e', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Rei(tab, Cor.Preta));

        }
    }
}
