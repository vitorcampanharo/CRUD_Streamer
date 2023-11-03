using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CRUD.Streamer;

namespace CRUD
{
    public class Titulo
    {
        public string NomeTitulo { get; set; }
        public string Sinopse { get; set; }
        public ClassificacaoEtaria Classificacao { get; set; }
        public Titulo(string nomeTitulo, string sinopse, ClassificacaoEtaria classificacao = ClassificacaoEtaria.Livre) 
        {
            NomeTitulo = nomeTitulo;
            Sinopse = sinopse;
            Classificacao = classificacao;
        }

        public Titulo(string nomeTitulo, ClassificacaoEtaria classificacao = ClassificacaoEtaria.Livre)
        {
            NomeTitulo = nomeTitulo;
            Classificacao = classificacao;
        }
        public override string ToString()
        {
            return $"{NomeTitulo},{Classificacao},{Sinopse}";
        }
    }
}
