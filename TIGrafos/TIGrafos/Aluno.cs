using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIGrafos
{
    class Aluno
    {
        private int codigoAluno;
        private int areaPesquisa;

        public Aluno() {
            //int _codigoAluno, int _areaPesquisa
            //CodigoAluno = _codigoAluno;
            //AreaPesquisa = _areaPesquisa
        }

        public int CodigoAluno
        {
            get => codigoAluno;
            set => codigoAluno = value;
        }

        public int AreaPesquisa
        {
            get => areaPesquisa;
            set => areaPesquisa = value;
        }
    }
}
