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
        private HashSet<Peca> pecas; //Conjunto de peças no jogo
        private HashSet<Peca> capturadas; //Conjunto de peças capturadas
        public bool xeque { get; private set; }

        //Construtor da classe com as regras de uma partida de Xadrez
        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8); //Iniciando o tabuleiro no tamanho de uma partida de Xadrez 8 x 8
            turno = 1; //Iniciando com o primeiro turno da partida
            jogadorAtual = Cor.Branca; //Definindo o primeiro jogador como na cor Branca (regra do xadrez)
            terminada = false; //Definindo de início que a partida não está terminada, recebendo false
            xeque = false; //Iniciando a propriedade xeque valendo falso
            pecas = new HashSet<Peca>(); //Instanciando um conjunto de peças
            capturadas = new HashSet<Peca>(); //Instanciando um conjunto de peças capturadas
            colocarPecas(); //Método auxiliar para colocar as peças do xadrez na posição padrão inicial
        }

        //Métodos
        public Peca executaMovimento(Posicao origem, Posicao destino) //método para executar movimento da peça no tabuleiro com posição de origem para a posição de destino retornando uma peça capturada
        {
            Peca p = tab.retirarPeca(origem); //Vai no tabuleiro e retira a peça na posição de origem
            p.incrementarQteMovimentos(); //Incrementa a quantidade de movimentos da peça a ser movida
            Peca pecaCapturada = tab.retirarPeca(destino); //Criando uma peça auxiliar para guardar a peça capturada na qual será retirada da posição destino
            tab.colocarPeca(p, destino); //Insere a peça que estava na origem, na posição destino
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada); //Condição se foi capturada alguma peça, acrescenta a peça no conjunto de capturadas
            }
            return pecaCapturada; //Retorna peça capturada
        }

        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada) //Método para desfazer o movimento anterior
        {
            Peca p = tab.retirarPeca(destino); //Tira a peça que estava anteriormente na posição de destino
            p.decrementarQteMovimentos(); //Chama o metódo da Peça para decrementar a quantidade de movimentos
            if (pecaCapturada != null) //Verifica na condicional se a peça capturada for diferente de nulo, então...
            {
                tab.colocarPeca(pecaCapturada, destino); //Coloca a peça capturada na posição de destino
                capturadas.Remove(pecaCapturada); //Remove a peça capturada no conjunto de peças capturadas
            }
            tab.colocarPeca(p, origem); //Pega a peça e coloca de volta na posição de origem, desfazendo o movimento
        }

        public void realizaJogada(Posicao origem, Posicao destino) //Método para relizar jogada
        {
            Peca pecaCapturada = executaMovimento(origem, destino); //Instanciando a pecaCapturada recebendo o executaMovimento
            if (estaEmXeque(jogadorAtual)) //Verifica se o jogador atual está em xeque, então desfaz a jogada, pois o próprio jogador não pode se colocar em xeque
            {
                desfazMovimento(origem, destino, pecaCapturada); //Instanciando o método para desfazer o movimento anterior que se colocou em xeque
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            if (estaEmXeque(adversaria(jogadorAtual))) //Se está em cheque o adversário do jogador atual
            {
                xeque = true; //Xeque recebe true
            }
            else
            {
                xeque = false; //Senão recebe false
            }

            if (testeXequemate(adversaria(jogadorAtual))) //Se o teste de xeque-mate do adversário do jogador atual for verdadeiro
            {
                terminada = true; //A propriedade terminada recebe true e acaba o jogo
            }
            else //Caso não dê false, incrementa o tunro e muda de jogador
            {
                turno++;
                mudaJogador();
            }

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
            if (!tab.peca(origem).movimentoPossivel(destino)) //Caso a peça na posição de origem NÃO possa mover para a posição de origem, irá lançar uma exceção
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
                if (x.cor == cor)
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

        private Cor adversaria(Cor cor) //Método privado para a própria classe para saber quem é a cor adversária de uma cor dada (branca ou preta)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        private Peca rei(Cor cor) //Método privado para devolver a peça rei de uma dada cor
        {
            foreach (Peca x in pecasEmJogo(cor)) //Para cada peça x em um conjunto de peças em jogo(numa dada cor)
            {
                if (x is Rei) //Se a peça x é uma instância (is) da classe Rei (subclasse da superclasse Peca)
                {
                    return x; //Retorna a peça x
                }
            }
            return null; //Se não encontrar nenhum rei (só para rodar, pois sempre tem que ter o rei em jogo antes do xeque-mate), retorna nulo
        }

        public bool estaEmXeque(Cor cor) //Método booleano que verifica se o rei de uma dada cor está em xeque
        {
            Peca R = rei(cor);
            if (R == null)
            {
                throw new TabuleiroException($"Não tem rei da cor {cor} no tabuleiro!");
            }

            foreach (Peca x in pecasEmJogo(adversaria(cor))) //Para cada peça x no conjunto de peças em jogo na cor adversária
            {
                bool[,] mat = x.movimentosPossiveis(); //Pega a matriz de movimentos possíveis e testa na estrutura condicional
                if (mat[R.posicao.linha, R.posicao.coluna]) //Se a matriz na posição do rei estiver verdadeiro para um possivel movimento de alguma peça adversária
                {
                    return true; //Retorna true e significa que o rei desta cor está xeque
                }
            }
            return false; //Caso não está em xeque, retorna false
        }

        public bool testeXequemate(Cor cor) //Método para verificar se o rei de uma dada cor está em xeque-mate
        {
            if (!estaEmXeque(cor))
            {
                return false;
            }
            foreach (Peca x in pecasEmJogo(cor)) //Para cada peça x em peças em jogo de uma dada cor
            {
                bool[,] mat = x.movimentosPossiveis(); //A matriz booleana de movimentos possiveis dessa peça x
                for (int i = 0; i < tab.linhas; i++) //Varrendo a matriz
                {
                    for (int j = 0; j < tab.colunas; j++)
                    {
                        if (mat[i, j]) //Se a matriz booleana na posição i-j for verdadeiro, é uma posição possivel para essa peça x
                        {
                            Posicao origem = x.posicao; //Define a posição da peça atual na origem
                            Posicao destino = new Posicao(i, j); //Instanciando a posição destino na posição i-j
                            Peca pecaCapturada = executaMovimento(origem, destino); //Peça realizada executa movimento dessa peça na posição atual para a nova posição i-j
                            bool testeXeque = estaEmXeque(cor); //Testa se ainda está em xeque
                            desfazMovimento(origem, destino, pecaCapturada); //Desfaz movimento
                            if (!testeXeque) //Se não está mais em xeque, existe um movimento que tira do xeque
                            {
                                return false; //Retorna false
                            }
                        }
                    }
                }
            }
            return true; //Se fez todos os testes acima e não deu false, então retorna true e define que deu xeque-mate

        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca) //Método auxiliar para colocar novas peças no tabuleiro
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao()); //Dado uma coluna e uma linha, coloca no tabuleiro essa peça numa nova posição xadrez
            pecas.Add(peca); //Adicionando essa peça no conjunto de peças da partida
        }

        private void colocarPecas() //Método para instanciar as peças no tabuleiro
        {
            //Chamando o método auxiliar de ColocarNovaPeça para inserir a peça no tabuleiro
            //Peças brancas
            colocarNovaPeca('a', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('b', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('c', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Dama(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Rei(tab, Cor.Branca));
            colocarNovaPeca('f', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('g', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('h', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('a', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('b', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('c', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('d', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('e', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('f', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('g', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('h', 2, new Peao(tab, Cor.Branca));

            //Peças pretas
            colocarNovaPeca('a', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('b', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Dama(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Rei(tab, Cor.Preta));
            colocarNovaPeca('f', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('g', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('h', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('a', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('b', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('c', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('d', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('e', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('f', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('g', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('h', 7, new Peao(tab, Cor.Preta));
        }
    }
}
