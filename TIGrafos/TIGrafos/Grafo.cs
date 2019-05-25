using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
