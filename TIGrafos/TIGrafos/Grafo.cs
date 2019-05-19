using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIGrafos
{
    class Grafo
    {
        public List<Vertice> listaVertices;

        internal List<Vertice> ListaVertices
        {
            get => listaVertices;
            set => listaVertices = value;
        }
    }
}
