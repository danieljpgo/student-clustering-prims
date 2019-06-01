using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TIGrafos
{
    class Grafo
    {
        private List<Vertice> listaVertices;

        // Construtor
        public Grafo(List<Vertice> _listaVertices)
        {
            ListaVertices = _listaVertices;
        }

        // Get/Set
        internal List<Vertice> ListaVertices
        {
            get => listaVertices;
            set => listaVertices = value;
        }

        public static Grafo LeitorArquivoDissimilaridade(string urlArquivo)
        {
            StreamReader arquivo = new StreamReader(urlArquivo);
            // Ler todo o arquivo e salvar em uma string
            string matriz = arquivo.ReadToEnd();
            // Substituir os \r e separar cada linha da matriz com \n
            string[] linhasMatriz = matriz.Replace('\r', ' ').Trim().Split('\n');
            // Lista de Vertices do Grafo
            List<Vertice> listaVertices = new List<Vertice>();

            // Linha da Matriz
            for (int i = 0; i < linhasMatriz.Length; i++)
            {
                List<int> ListaAreaPequisa = new List<int>();
                List<Aresta> listaArestas = new List<Aresta>();
                List<Aluno> listaAlunos = new List<Aluno>();
                // Remover os espaços iniciais e finais da String antes de dividir em pesos
                string[] pesos = linhasMatriz[i].Trim().Split(' ');
                // Contador para saber pegar o valor da coluna sem está ligado com o indice do loop
                int contPosicaoColuna = 0;

                // Coluna da Matriz
                for (int x = 0; x < linhasMatriz.Length; x++)
                {
                    // Peso representado na Matriz
                    if (x >= i)
                    {
                        // Não represetar um loop de aresta
                        // x + 1 ou i + 1 ocorre devido aos loops que começam com 0 e o identificador começa 1
                        if (x > i)
                        {
                            Aresta aresta = new Aresta();
                            aresta.Origem = (i + 1);
                            aresta.Destino = (x + 1);
                            aresta.Peso = int.Parse(pesos[contPosicaoColuna]);

                            listaArestas.Add(aresta);
                        }
                        contPosicaoColuna++;
                    }
                    // Peso não representado na Matriz
                    else
                    {
                        Aresta aresta = new Aresta();
                        aresta.Origem = (i + 1);
                        aresta.Destino = (x + 1);
                        aresta.Peso = listaVertices[x].ListaArestas[i - 1].Peso;

                        listaArestas.Add(aresta);
                    }
                }


                ListaAreaPequisa.Add(i + 1);
                // Adicionar toda as Arestas encontradas em um novo Vertice.
                Vertice vertice = new Vertice((i + 1), listaArestas, listaAlunos, ListaAreaPequisa);
                listaVertices.Add(vertice);
            }

            Grafo grafoDissimilaridade = new Grafo(listaVertices);
            return grafoDissimilaridade;
        }

        public static Grafo Prim(Grafo _grafoCompleto)
        {
            List<Vertice> verticesArvoreGenMin = new List<Vertice>();
            // Gerar um indice aleatorio, para iniciar o Prim
            Random random = new Random();
            int inicialRandom = random.Next(0, _grafoCompleto.ListaVertices.Count);

            // Adicionar o Vertice randomico a lista de Vertices da Arvore
            verticesArvoreGenMin.Add(_grafoCompleto.ListaVertices[inicialRandom]);

            // Looping para percorrer todos Vertices
            for (int i = 0; i < (_grafoCompleto.ListaVertices.Count); i++)
            {
                // Loop para remover todas as arestas já percorridas e para limpar toda as arestas do ultimo Vertice consequentemente
                for (int x = 0; x < verticesArvoreGenMin.Count; x++)
                {
                    verticesArvoreGenMin[i].ListaArestas.RemoveAll((aresta) => aresta.Destino == verticesArvoreGenMin[x].Identificador);
                }

                // Condição para o ultimo Vertice não entrar
                if (i < _grafoCompleto.ListaVertices.Count - 1)
                {
                    // Encontrar a Aresta de menor peso no Vertice atual da Arvore Geradora Minima
                    Aresta arestaMenorPeso = verticesArvoreGenMin[i].ListaArestas.Aggregate((primeiraAresta, segundaAresta) =>
                    {
                        return primeiraAresta.Peso < segundaAresta.Peso ? primeiraAresta : segundaAresta;
                    });

                    // Encontrar o Vertice que corresponde a aresta de menor peso encontra 
                    Vertice proximoVertice = _grafoCompleto.ListaVertices.Find((vertice) =>
                    {
                        return vertice.Identificador == arestaMenorPeso.Destino;
                    });

                    // Remover todas as Aresta que não sejam a menor do Vertice atual da Arvore Geradora Minima
                    verticesArvoreGenMin[i].ListaArestas.RemoveAll((aresta) => aresta.Destino != proximoVertice.Identificador);
                    verticesArvoreGenMin[i].ListaArestas[0].Direcao = 1;

                    // Adicionar novo Vertice encontrado a Arvore
                    verticesArvoreGenMin.Add(proximoVertice);
                }
            }

            Grafo arvoreGeradoraMin = new Grafo(verticesArvoreGenMin);
            return arvoreGeradoraMin;
        }

        public static Grafo LeitorArquivoAlunos(Grafo _arvoreGeradoraMin, string urlArquivo)
        {
            StreamReader arquivo = new StreamReader(urlArquivo);
            // Ler todo o arquivo e salvar em uma string
            string lista = arquivo.ReadToEnd();
            // Substituir os \r e separar cada linha da lista com \n
            string[] vetorAlunos = lista.Replace('\r', ' ').Trim().Split('\n');

            List<Aluno> listaAlunos = new List<Aluno>();

            // Loop para preencher a Lista de Alunos usando o Vetor de Alunos
            foreach(string alunoInfo in vetorAlunos)
            {
                string[] dadosAluno = alunoInfo.Trim().Split(' ');
                Aluno aluno = new Aluno();
                aluno.CodigoAluno = int.Parse(dadosAluno[0]);
                aluno.AreaPesquisa = int.Parse(dadosAluno[1]);
                listaAlunos.Add(aluno);
            }

            // Loop para encontrar os Alunos de cada AreaPesquisa e inserir no Vertice respectivo
            _arvoreGeradoraMin.ListaVertices.ForEach((vertice) =>
            {
                vertice.ListaAlunos = listaAlunos.FindAll((aluno) => aluno.AreaPesquisa == vertice.Identificador);
            });

            return _arvoreGeradoraMin;

        }

        public static Grafo GeracaoCluster(int numeroGrupos, Grafo _arvoreGeradoraMin)
        {
            // Verifica se o numero digitado é menor que o numero de vertice do grafo da arvore geradora minima
            if (numeroGrupos < _arvoreGeradoraMin.ListaVertices.Count)
            {
                while (numeroGrupos < _arvoreGeradoraMin.ListaVertices.Count)
                {
                    // Encontrar a aresta com o menor peso
                    Vertice verticeRemovivel = _arvoreGeradoraMin.ListaVertices.Aggregate((primeiroVertice, segundoVertice) =>
                    {
                        // Verifica se já percorreu toda a lista, para impedir que retorne em error
                        if (segundoVertice.ListaArestas.Count > 0)
                        {
                            return primeiroVertice.ListaArestas[0].Peso < segundoVertice.ListaArestas[0].Peso ? primeiroVertice : segundoVertice;
                        } else
                        {
                            return primeiroVertice;
                        }
                    });

                    // Encontrar o index do Vertice que será removido
                    int indexRemover = _arvoreGeradoraMin.ListaVertices.FindIndex((vertice) => vertice.Identificador == verticeRemovivel.ListaArestas[0].Origem);

                    // Caso o Index seja mairo que 0, irei unir as informações no vertice anterior ao encontrado que será removido
                    if (indexRemover > 0)
                    {
                        // Somar o Peso da Aresta anterior com o peso da Aresta que será removido
                        _arvoreGeradoraMin.ListaVertices[indexRemover - 1].ListaArestas[0].Peso = _arvoreGeradoraMin.ListaVertices[indexRemover - 1].ListaArestas[0].Peso + verticeRemovivel.ListaArestas[0].Peso;
                        // Juntar a Lista de Areas de Pesquisa
                        _arvoreGeradoraMin.ListaVertices[indexRemover - 1].ListaAreaPequisa.Concat(_arvoreGeradoraMin.ListaVertices[indexRemover].ListaAreaPequisa);
                        // Juntar a Lista de Alunos
                        _arvoreGeradoraMin.ListaVertices[indexRemover - 1].ListaAlunos.Concat(_arvoreGeradoraMin.ListaVertices[indexRemover].ListaAlunos);
                        // Apontar a aresta para o proximo Vertice, retirando o apontamento no Vertice que será removido
                        _arvoreGeradoraMin.ListaVertices[indexRemover - 1].ListaArestas[0].Destino = verticeRemovivel.ListaArestas[0].Destino;

                        // Remover o Vertice, apos juntar todas as informações
                        _arvoreGeradoraMin.ListaVertices.Remove(verticeRemovivel);
                    }
                    // Caso o Index seja igual 0, irei unir as informações ao vertice de baixo ao que foi encontrado, que será removido
                    else
                    {
                        // Somar o Peso da proximo Aresta com o peso da Aresta que será removido
                        _arvoreGeradoraMin.ListaVertices[indexRemover + 1].ListaArestas[0].Peso = _arvoreGeradoraMin.ListaVertices[indexRemover + 1].ListaArestas[0].Peso + verticeRemovivel.ListaArestas[0].Peso;
                        // Juntar a Lista de Areas de Pesquisa
                        _arvoreGeradoraMin.ListaVertices[indexRemover + 1].ListaAreaPequisa.Concat(_arvoreGeradoraMin.ListaVertices[indexRemover].ListaAreaPequisa);
                        // Juntar a Lista de Alunos
                        _arvoreGeradoraMin.ListaVertices[indexRemover + 1].ListaAlunos.Concat(_arvoreGeradoraMin.ListaVertices[indexRemover].ListaAlunos);

                        // Remover o Vertice, apos juntar todas as informações
                        _arvoreGeradoraMin.ListaVertices.Remove(verticeRemovivel);
                    }
                    Console.Write("----------------------------------\n");
                    _arvoreGeradoraMin.ListaVertices.ForEach((vertice) =>
                    {
                        Console.Write("( ");
                        vertice.ListaAreaPequisa.ForEach((valor) =>
                        {
                            Console.Write("{0}-", valor);
                        });
                        Console.Write(" ) = Informações:\n");

                        if (vertice.ListaArestas.Count > 0)
                        {
                            Console.Write(" |  |    Codigo Aluno    :");
                            vertice.ListaAlunos.ForEach((aluno) =>
                            {
                                Console.Write("\t{0}", aluno.CodigoAluno);
                            });
                            Console.Write("\n |{0}|    Area de Pesquisa:", vertice.ListaArestas[0].Peso);
                            vertice.ListaAlunos.ForEach((aluno) =>
                            {
                                Console.Write("\t{0}", aluno.AreaPesquisa);
                            });
                            Console.WriteLine("\n  ||   ");
                        }
                        else
                        {
                            Console.Write("         Codigo Aluno    :");
                            vertice.ListaAlunos.ForEach((aluno) =>
                            {
                                Console.Write("\t{0}", aluno.CodigoAluno);
                            });
                            Console.Write("\n         Area de Pesquisa:");
                            vertice.ListaAlunos.ForEach((aluno) =>
                            {
                                Console.Write("\t{0}\n", aluno.AreaPesquisa);
                            });
                        }

                    });

                    Console.Write("Digite algo para continuar");
                    Console.ReadKey();
                }
                return _arvoreGeradoraMin;
            } else
            {
                Console.WriteLine("O numero de grupos {0}, é maior ou igual que o número de áreas de pesquisa {1}.", numeroGrupos, _arvoreGeradoraMin.ListaVertices.Count);
                return _arvoreGeradoraMin;
            }
        }
    }
}



// Console.WriteLine(primeiroVertice.ListaArestas.Count);
// Console.WriteLine("---------");
// Console.WriteLine(segundoVertice.ListaArestas.Count);