using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TIGrafos
{
    class Program
    {
        static void Main(string[] args)
        {
            Grafo grafoDissimilaridade = Grafo.leitorArquivoDissimilaridade("Matriz_Dissimilaridade.txt");
            Console.ReadKey();
        }
    }
}
