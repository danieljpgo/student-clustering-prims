using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIGrafos
{
    class Program
    {
        static void Main(string[] args)
        {
            Aresta aresta = new Aresta();
            aresta.Origem = 1;
            aresta.Destino = 2;
            aresta.Direcao = 0;
            aresta.Peso = 60;
            List<Aresta> listaAresta = new List<Aresta>();
            listaAresta.Add(aresta);
            Vertice v1 = new Vertice(1, listaAresta);


            Aresta aresta2 = new Aresta();
            aresta2.Origem = 2;
            aresta2.Destino = 1;
            aresta2.Direcao = 0;
            aresta2.Peso = 60;

            List<Aresta> listaAresta2 = new List<Aresta>();
            listaAresta2.Add(aresta2);

            Vertice v2 = new Vertice(2, listaAresta2);



            List<Vertice> listaVertices = new List<Vertice>();
            listaVertices.Add(v1);
            listaVertices.Add(v2);


            Grafo teste = new Grafo(listaVertices);
            Console.ReadKey();
        }
    }
}
