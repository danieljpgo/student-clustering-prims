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
            Console.Write("Para iniciar a geração do Grafo utilizando a Matriz Dissimilaridade, digite alguma tecla:\n");
            Console.ReadKey();
            Grafo grafoCompleto = Grafo.LeitorArquivoDissimilaridade("Matriz_Dissimilaridade.txt");

            Console.Write("Grafo gerado com sucesso, agora digite alguma tecla para gerar a Arvore Geradora Minima:\n");
            Console.ReadKey();
            Grafo arvoreGeradoraMinima = Grafo.Prim(grafoCompleto);


            //Aluno aluno = new Aluno();
            //aluno.AreaPesquisa = 1;
            //aluno.AreaPesquisa = 5;

            //grafoCompleto.ListaVertices[0].ListaAlunos.Add(aluno);

            Console.ReadKey();
        }
    }
}
