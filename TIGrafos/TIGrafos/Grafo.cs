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
            string[] linhasMatriz = matriz.Replace('\r', ' ').Split('\n');
            // Lista de Vertices do Grafo
            List<Vertice> listaVertices = new List<Vertice>();

            // Linha da Matriz
            for (int i = 0; i < linhasMatriz.Length; i++)
            {
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

                // Adicionar toda as Arestas encontradas em um novo Vertice.
                Vertice vertice = new Vertice((i + 1), listaArestas, listaAlunos);
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
    }
}