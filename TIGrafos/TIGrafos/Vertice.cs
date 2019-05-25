using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIGrafos
{
    class Vertice
    {
        private int identificador;
        private List<Aresta> listaArestas;

        // Construtor
        public Vertice(int _identificador, List<Aresta> _listaArestas) {
            Identificador = _identificador;
            ListaArestas = _listaArestas;
        }

        // Get/Set
        public int Identificador
        {
            get => identificador;
            set => identificador = value;
        }

        public List<Aresta> ListaArestas
        {
            get => listaArestas;
            set => listaArestas = value;
        }
    }
}
