using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIGrafos
{
    class Aresta
    {
        
        private int direcao = 0; /* 0 = Não posssui direção / 1 = esquerda para direita / -1 = direita para esquerda */
        private int peso;
        private int origem;
        private int destino;

        // Get/Set

        public Aresta() { }

        public int Direcao
        {
            get => direcao;
            set => direcao = value;
        }

        public int Peso
        {
            get => peso;
            set => peso = value;
        }

        public int Origem
        {
            get => origem;
            set => origem = value;
        }

        public int Destino
        {
            get => destino;
            set => destino = value;
        }
    }
}
