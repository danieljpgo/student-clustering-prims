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

            Console.Write("Arvore Geradora Minima gerado com sucesso, agora digite alguma tecla para preencher a Arvore com os Alunos:\n");
            Console.ReadKey();
            Grafo arvore = Grafo.LeitorArquivoAlunos(arvoreGeradoraMinima, "Dados_Aluno_Pesquisa.txt");





            Console.ReadKey();
        }
    }
}
